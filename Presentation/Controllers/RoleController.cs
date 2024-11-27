using DompifyAPI.Application.Interfaces;
using DompifyAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DompifyAPI.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController(IRoleUseCase roleUseCase) : ControllerBase
    {
        private readonly IRoleUseCase _roleUseCase = roleUseCase;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            var roles = await _roleUseCase.GetRolesAsync();

            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRoleById(int id)
        {
            var role = await _roleUseCase.GetRoleByIdAsync(id);
            if (role == null) 
                return NotFound();

            return Ok(role);
        }

        [HttpPost]
        public async Task<ActionResult<Role>> CreateRole([FromBody] Role role)
        {
            var createdRole = await _roleUseCase.CreateRoleAsync(role);

            return CreatedAtAction(nameof(GetRoleById), new { id = createdRole?.Id }, createdRole);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Role>> UpdateRole(int id, [FromBody] Role role)
        {
            role.Id = id;
            var updatedRole = await _roleUseCase.UpdateRoleAsync(role);
            if (updatedRole == null) 
                return NotFound();

            return Ok(updatedRole);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRole(int id)
        {
            var result = await _roleUseCase.DeleteRoleAsync(id);
            if (!result) 
                return NotFound();

            return NoContent();
        }
    }
}
