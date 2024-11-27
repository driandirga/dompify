using DompifyAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DompifyAPI.Domain.Interfaces
{
    public interface IOTPRepository
    {
        Task<OTP?> GetOTPByUserIdAsync(int userId);
        Task<string> SaveOrUpdateOTPAsync(int userId, string otpToken, DateTime expirationTime);
    }
}
