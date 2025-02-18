using DompifyAPI.Application.Helpers;
using DompifyAPI.Application.Interfaces;
using DompifyAPI.Domain.Entities;
using DompifyAPI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using MimeKit;
using MailKit.Net.Smtp;

namespace DompifyAPI.Application.UseCases
{
    public partial class AuthUseCase(IAuthRepository authRepository, IOTPRepository OTPRepository) : IAuthUseCase
    {
        private readonly IAuthRepository _authRepository = authRepository;
        private readonly IOTPRepository _OTPRepository = OTPRepository;
        private const int OTP_LENGTH = 5;
        private const int OTP_EXPIRATION = 1;
        private const string SECRET_KEY = "my_super_deep_salt_secret_key_12345678!";

        [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
        private static partial Regex EmailRegex();

        public void ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email) || !EmailRegex().IsMatch(email))
                throw new ArgumentException("Invalid email format.");
        }

        public void ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 6)
                throw new ArgumentException("Password must be at least 6 characters long.");
        }

        public void ValidateConfirmPassword(string password, string confirmPassword)
        {
            if (password != confirmPassword)
                throw new ArgumentException("Passwords do not match.");
        }

        public async Task<User?> LoginUserAsync(string email, string password)
        {
            ValidateEmail(email);
            ValidatePassword(password);

            var user = await _authRepository.GetUserByEmailAsync(email)
                       ?? throw new ArgumentException("User not found.");

            return user;
        }

        public async Task RegisterAsync(string email, string password, string confirmPassword)
        {
            ValidateEmail(email);
            ValidatePassword(password);
            ValidateConfirmPassword(password, confirmPassword);

            if (await _authRepository.IsEmailRegisteredAsync(email))
                throw new ArgumentException("Email already registered.");

            string randomString;
            bool isUsernameUnique = false;

            do
            {
                randomString = Generate.RandomString(8);
                var existingUsername = await _authRepository.GetUserByUsernameAsync(randomString);
                if (existingUsername == null)
                {
                    isUsernameUnique = true;
                }
            }
            while (!isUsernameUnique);

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User
            {
                Username = randomString,
                Email = email,
                Password = hashedPassword,
                RoleId = 1,
                IsActive = true,
                CreatedAt = DateTime.Now.ToUniversalTime(),
                CreatedBy = "SYSTEM",
                UpdatedAt = DateTime.Now.ToUniversalTime(),
                UpdatedBy = "SYSTEM"
            };

            await _authRepository.RegisterUserAsync(user);
        }

        public async Task<User?> LoginAsync(string email, string password)
        {
            ValidateEmail(email);
            ValidatePassword(password);

            var user = await _authRepository.GetUserByEmailAsync(email)
                       ?? throw new ArgumentException("User not found.");

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
                throw new ArgumentException("Login failed, email or password is incorrect.");

            return user;
        }

        public async Task CreateNewPasswordAsync(string email, string password, string confirmPassword)
        {
            ValidateEmail(email);
            ValidatePassword(password);
            ValidateConfirmPassword(password, confirmPassword);

            var user = await _authRepository.GetUserByEmailAsync(email)
                       ?? throw new ArgumentException("User not found.");

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            user.Password = hashedPassword;
            user.UpdatedAt = DateTime.Now.ToUniversalTime();
            user.UpdatedBy = user.Username;

            await _authRepository.UpdateUserAsync(user);
        }

        public string GenerateOTPToken(string otpCode)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                    new Claim("otpCode", otpCode)
                };

            var token = new JwtSecurityToken(
                issuer: "http://localhost:5000",
                audience: "http://localhost:5000",
                claims: claims,
                expires: DateTime.Now.ToUniversalTime().AddMinutes(OTP_EXPIRATION),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> CheckOTPAsync(string email)
        {
            string otpCode = Generate.RandomNumber(OTP_LENGTH);
            string otpToken = GenerateOTPToken(otpCode); // Encrypted OTP
            var user = await _authRepository.GetUserByEmailAsync(email)
                        ?? throw new ArgumentException("User not found");
            int userId = user.Id;
            DateTime expirationTime = DateTime.UtcNow.AddMinutes(OTP_EXPIRATION);

            await _OTPRepository.SaveOrUpdateOTPAsync(userId, otpToken, expirationTime);

            return otpCode;
        }

        public async Task<bool> SendOtpEmailAsync(string email)
        {
            string otpCode = await CheckOTPAsync(email);

            if (string.IsNullOrEmpty(otpCode))
                return false;

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Protentik", "protentik@gmail.com"));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = "Your OTP Code";

            message.Body = new TextPart("plain")
            {
                Text = $"Your OTP code is: {otpCode}"
            };

            var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync("protentik@gmail.com", "rgcyqjoelcgkomvx");
            await client.SendAsync(message);
            await client.DisconnectAsync(true);

            return true;
        }

        public async Task<Data?> ValidateOtpTokenAsync(string email, string otpCode)
        {
            var user = await _authRepository.GetUserByEmailAsync(email)
                        ?? throw new ArgumentException("User not found");
            int userId = user.Id;
            var otpEntry = await _OTPRepository.GetOTPByUserIdAsync(userId);
            if (otpEntry == null || otpEntry.Token == null || otpEntry.ExpirationTime <= DateTime.Now.ToUniversalTime())
                throw new ArgumentException("OTP not found or expired.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "http://localhost:5000",
                ValidAudience = "http://localhost:5000",
                IssuerSigningKey = key
            };

            ClaimsPrincipal principal = tokenHandler.ValidateToken(otpEntry.Token, validationParameters, out SecurityToken validatedToken);

            var otpClaim = principal.Claims.FirstOrDefault(c => c.Type == "otpCode")?.Value;
            if (otpClaim == null)
                return null;

            if(otpClaim != otpCode)
                return null;

            return new Data { Token = otpEntry.Token };
        }
    }
}
