using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotkaAPI.Data;
using NotkaAPI.Models.Notes;

namespace NotkaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly NotkaDatabaseContext _context;

        public ListController(NotkaDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/List
        [HttpGet]
        public async Task<ActionResult<IEnumerable<List>>> GetList()
        {
            return await _context.List.ToListAsync();
        }

        // GET: api/List/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List>> GetList(int id)
        {
            var list = await _context.List.FindAsync(id);

            if (list == null)
            {
                return NotFound();
            }

            return list;
        }

        // PUT: api/List/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutList(int id, List list)
        {
            if (id != list.Id)
            {
                return BadRequest();
            }

            _context.Entry(list).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListExists(id))
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

        // POST: api/List
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<List>> PostList(List list)
        {
            _context.List.Add(list);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetList", new { id = list.Id }, list);
        }

        // DELETE: api/List/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteList(int id)
        {
            var list = await _context.List.FindAsync(id);
            if (list == null)
            {
                return NotFound();
            }

            _context.List.Remove(list);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ListExists(int id)
        {
            return _context.List.Any(e => e.Id == id);
        }
    }
}
