using DompifyAPI.Domain.Entities;
using DompifyAPI.Domain.Interfaces;
using DompifyAPI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DompifyAPI.Infrastructure.Repositories
{
    public partial class RoleRepository(ApplicationDbContext context) : IRoleRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<IEnumerable<Role>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role?> GetRoleById(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task<Role?> GetRoleByName(string name)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Name == name);
        }

        public async Task<Role?> CreateRole(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            return role;
        }

        public async Task<Role?> UpdateRole(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();

            return role;
        }

        public async Task<bool> DeleteRole(Role role)
        {
            _context.Roles.Remove(role);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
