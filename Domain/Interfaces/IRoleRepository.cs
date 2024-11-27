using DompifyAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DompifyAPI.Domain.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetRoles();
        Task<Role?> GetRoleById(int id);
        Task<Role?> GetRoleByName(string name);
        Task<Role?> CreateRole(Role role);
        Task<Role?> UpdateRole(Role role);
        Task<bool> DeleteRole(Role role);
    }
}
