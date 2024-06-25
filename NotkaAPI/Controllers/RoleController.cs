using ApiSharedClasses.QueryParameters;
using Microsoft.AspNetCore.Mvc;
using NotkaAPI.Contracts;
using NotkaAPI.Helpers;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
		private readonly IRepositoryWrapper _repository;

		public RoleController(IRepositoryWrapper repository)
        {
			_repository = repository;
		}

        // GET: api/Role
		[HttpGet("{userId}", Name = "RoleGETAll")]
		public async Task<ActionResult<PagedList<RoleForView>>> GetRole(int userId, [FromQuery] RoleParameters roleParameters)
		{
			PagedList<RoleForView> roles;
			try
			{
				roles = await _repository.Role.GetRoles(userId, roleParameters);
			}
			catch (NotFoundException)
			{
				return NotFound();
			}
			catch
			{
				return BadRequest();
			}

			return roles;
		}

        // GET: api/Role/5
		[HttpGet("{userId}/{id}", Name = "RoleGET")]
		public async Task<ActionResult<RoleForView>> GetRole(int userId, int id)
		{
			try
			{
				return await _repository.Role.GetRoleById(userId, id);
			}
			catch (NotFoundException)
			{
				return NotFound();
			}
			catch (UnauthorizedException)
			{
				return Unauthorized();
			}
			catch
			{
				return BadRequest();
			}
		}

        // PUT: api/Role/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}", Name = "RolePUT")]
		public async Task<IActionResult> PutRole(int id, RoleForView role)
        {
			if (id != role.Id)
			{
				return BadRequest();
			}

			try
			{
				await _repository.Role.UpdateRole(id, role);
			}
			catch (NotFoundException)
			{
				return NotFound();
			}
			catch
			{
				return BadRequest();
			}

			return NoContent();
		}

		// POST: api/Role
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost(Name = "RolePOST")]
		public async Task<ActionResult<RoleForView>> PostRole(RoleForView role)
        {
			if (role == null) return Forbid();

			RoleForView uploadedRole;
			try
			{
				uploadedRole = await _repository.Role.CreateRole(role);
			}
			catch
			{
				return BadRequest();
			}

			return Ok(uploadedRole);
		}

		// DELETE: api/Role/5
		[HttpDelete("{userId}/{id}", Name = "RoleDELETE")]
		public async Task<IActionResult> DeleteRole(int userId, int id)
		{
			try
			{
				await _repository.Role.DeleteRole(userId, id);
			}
			catch (NotFoundException)
			{
				return NotFound();
			}
			catch (ForbidException)
			{
				return Forbid();
			}

			return NoContent();
		}
    }
}
