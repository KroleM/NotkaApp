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
    public class ListTagController : ControllerBase
    {
        private readonly NotkaDatabaseContext _context;

        public ListTagController(NotkaDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/ListTag
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListTag>>> GetListTag()
        {
            return await _context.ListTag.ToListAsync();
        }

        // GET: api/ListTag/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ListTag>> GetListTag(int id)
        {
            var listTag = await _context.ListTag.FindAsync(id);

            if (listTag == null)
            {
                return NotFound();
            }

            return listTag;
        }

        // PUT: api/ListTag/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutListTag(int id, ListTag listTag)
        {
            if (id != listTag.Id)
            {
                return BadRequest();
            }

            _context.Entry(listTag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListTagExists(id))
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

        // POST: api/ListTag
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ListTag>> PostListTag(ListTag listTag)
        {
            _context.ListTag.Add(listTag);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetListTag", new { id = listTag.Id }, listTag);
        }

        // DELETE: api/ListTag/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteListTag(int id)
        {
            var listTag = await _context.ListTag.FindAsync(id);
            if (listTag == null)
            {
                return NotFound();
            }

            _context.ListTag.Remove(listTag);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ListTagExists(int id)
        {
            return _context.ListTag.Any(e => e.Id == id);
        }
    }
}
