using DompifyAPI.Application.Helpers;
using DompifyAPI.Domain.Entities;
using DompifyAPI.Domain.Interfaces;
using DompifyAPI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace DompifyAPI.Infrastructure.Repositories
{
    public partial class OTPRepository(ApplicationDbContext context) : IOTPRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<OTP?> GetOTPByUserIdAsync(int userId)
        {
            return await _context.OTPs
                .FirstOrDefaultAsync(o => o.UserId == userId && o.Token != null && o.ExpirationTime > DateTime.UtcNow);
        }

        public async Task<string> SaveOrUpdateOTPAsync(int userId, string otpToken, DateTime expirationTime)
        {
            var existingOTP = await GetOTPByUserIdAsync(userId);

            if (existingOTP != null)
            {
                existingOTP.Token = otpToken;
                existingOTP.ExpirationTime = expirationTime;
            }
            else
            {
                var otp = new OTP
                {
                    UserId = userId,
                    Token = otpToken,
                    ExpirationTime = expirationTime,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = userId.ToString()
                };

                await _context.OTPs.AddAsync(otp);
            }

            await _context.SaveChangesAsync();

            return otpToken;
        }
    }
}
