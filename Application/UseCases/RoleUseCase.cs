using DompifyAPI.Application.Interfaces;
using DompifyAPI.Domain.Entities;
using DompifyAPI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DompifyAPI.Application.UseCases
{
    public class RoleUseCase(IRoleRepository roleRepository) : IRoleUseCase
    {
        private readonly IRoleRepository _roleRepository = roleRepository;

        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            return await _roleRepository.GetRoles();
        }

        public async Task<Role?> GetRoleByIdAsync(int id)
        {
            return await _roleRepository.GetRoleById(id);
        }

        public async Task<Role?> CreateRoleAsync(Role role)
        {
            if (string.IsNullOrEmpty(role.Name))
                throw new ArgumentException("Role name cannot be empty.");

            var existingRole = await _roleRepository.GetRoleByName(role.Name);
            if (existingRole != null)
                throw new ArgumentException("Role name already exists.");

            role.CreatedAt = DateTime.UtcNow;
            role.CreatedBy = "SYSTEM";

            return await _roleRepository.CreateRole(role);
        }

        public async Task<Role?> UpdateRoleAsync(Role role)
        {
            var existingRole = await _roleRepository.GetRoleById(role.Id);
            if (existingRole == null) 
                return null;

            var existingRoleByName = await _roleRepository.GetRoleByName(role.Name!);
            if (existingRoleByName != null)
                throw new ArgumentException("Role name already exists.");

            existingRole.Name = role.Name ?? existingRole.Name;
            existingRole.Description = role.Description ?? existingRole.Description;
            existingRole.IsActive = role.IsActive ?? existingRole.IsActive;
            existingRole.UpdatedAt = DateTime.UtcNow;
            existingRole.UpdatedBy = "SYSTEM";

            return await _roleRepository.UpdateRole(existingRole);
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            var role = await _roleRepository.GetRoleById(id);
            if (role == null)
                return false;

            return await _roleRepository.DeleteRole(role);
        }
    }
}
