using DompifyAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DompifyAPI.Application.Interfaces
{
    public interface IAuthUseCase
    {
        void ValidateEmail(string email);
        void ValidatePassword(string confirmPassword);
        void ValidateConfirmPassword(string password, string confirmPassword);
        Task<User?> LoginUserAsync(string email, string password);
        Task RegisterAsync(string email, string password, string confirmPassword);
        Task<User?> LoginAsync(string email, string password);
        Task CreateNewPasswordAsync(string email, string password, string confirmPassword);
        string GenerateOTPToken(string otp);
        Task<bool> SendOtpEmailAsync(string email);
        Task<Data?> ValidateOtpTokenAsync(string email, string otp);
    }
}
