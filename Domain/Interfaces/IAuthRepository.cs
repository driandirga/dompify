using DompifyAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DompifyAPI.Domain.Interfaces
{
    public interface IAuthRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<User?> GetUserByPhoneAsync(string phone);
        Task<bool> IsEmailRegisteredAsync(string email);
        Task<bool> IsUsernameRegisteredAsync(string username);
        Task<bool> IsPhoneRegisteredAsync(string phone);
        Task RegisterUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
    }
}
