using DompifyAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DompifyAPI.Application.Interfaces
{
    public interface IRoleUseCase
    {
        Task<IEnumerable<Role>> GetRolesAsync();
        Task<Role?> GetRoleByIdAsync(int id);
        Task<Role?> CreateRoleAsync(Role role);
        Task<Role?> UpdateRoleAsync(Role role);
        Task<bool> DeleteRoleAsync(int id);
    }
}
