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
    public class NoteTagController : ControllerBase
    {
        private readonly NotkaDatabaseContext _context;

        public NoteTagController(NotkaDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/NoteTag
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteTag>>> GetNoteTag()
        {
            return await _context.NoteTag.ToListAsync();
        }

        // GET: api/NoteTag/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NoteTag>> GetNoteTag(int id)
        {
            var noteTag = await _context.NoteTag.FindAsync(id);

            if (noteTag == null)
            {
                return NotFound();
            }

            return noteTag;
        }

        // PUT: api/NoteTag/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNoteTag(int id, NoteTag noteTag)
        {
            if (id != noteTag.Id)
            {
                return BadRequest();
            }

            _context.Entry(noteTag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteTagExists(id))
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

        // POST: api/NoteTag
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NoteTag>> PostNoteTag(NoteTag noteTag)
        {
            _context.NoteTag.Add(noteTag);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNoteTag", new { id = noteTag.Id }, noteTag);
        }

        // DELETE: api/NoteTag/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNoteTag(int id)
        {
            var noteTag = await _context.NoteTag.FindAsync(id);
            if (noteTag == null)
            {
                return NotFound();
            }

            _context.NoteTag.Remove(noteTag);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NoteTagExists(int id)
        {
            return _context.NoteTag.Any(e => e.Id == id);
        }
    }
}
