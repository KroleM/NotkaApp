using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotkaAPI.Data;
using NotkaAPI.Models.Users;

namespace NotkaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleUserController : ControllerBase
    {
        private readonly NotkaDatabaseContext _context;

        public RoleUserController(NotkaDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/RoleUser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleUser>>> GetRoleUser()
        {
            return await _context.RoleUser.ToListAsync();
        }

        // GET: api/RoleUser/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleUser>> GetRoleUser(int id)
        {
            var roleUser = await _context.RoleUser.FindAsync(id);

            if (roleUser == null)
            {
                return NotFound();
            }

            return roleUser;
        }

        // PUT: api/RoleUser/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoleUser(int id, RoleUser roleUser)
        {
            if (id != roleUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(roleUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/RoleUser
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RoleUser>> PostRoleUser(RoleUser roleUser)
        {
            _context.RoleUser.Add(roleUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoleUser", new { id = roleUser.Id }, roleUser);
        }

        // DELETE: api/RoleUser/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoleUser(int id)
        {
            var roleUser = await _context.RoleUser.FindAsync(id);
            if (roleUser == null)
            {
                return NotFound();
            }

            _context.RoleUser.Remove(roleUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoleUserExists(int id)
        {
            return _context.RoleUser.Any(e => e.Id == id);
        }
    }
}
