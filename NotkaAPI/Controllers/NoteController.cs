using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotkaAPI.Data;
using NotkaAPI.Helpers;
using NotkaAPI.Models.BusinessLogic;
using NotkaAPI.Models.Notes;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly NotkaDatabaseContext _context;

        public NoteController(NotkaDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Note
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<NoteForView>>> GetNote(int userId)
        {
            if (_context.Note.Where(n => n.UserId == userId) == null) 
            {
                return NotFound();
            }
            var notes = await _context
                .Note
				.Where(n => n.UserId == userId)
				.Include(note => note.NoteTag)
                .ThenInclude(notetag => notetag.Tag)
                //.Include(note => note.Picture)
                .ToListAsync();

            return notes
                .Select(note => ModelConverters.ConvertToNoteForView(note))
                .OrderByDescending(n => n.ModifiedDate)
				.ToList();

            //return await _context.Note.Where(n => n.UserId == userId).OrderByDescending(n => n.ModifiedDate).ToListAsync();
        }

        // GET: api/Note/1/5
        [HttpGet("{userId}/{id}")]
        public async Task<ActionResult<NoteForView>> GetNote(int userId, int id)
        {
            var note = await _context.Note.FindAsync(id);

            if (note == null)
            {
                return NotFound();
            }
            if (note.UserId != userId)
            {
                return Forbid();
            }

            return ModelConverters.ConvertToNoteForView(note);
        }

        // PUT: api/Note/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNote(int id, NoteForView note)
        {
            if (id != note.Id)
            {
                return BadRequest();
            }

            _context.Entry(note).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteExists(id))
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

        // POST: api/Note
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NoteForView>> PostNote(NoteForView note)
        {
            var noteToAdd = new Note().CopyProperties(note);
            _context.Note.Add(noteToAdd);

            foreach (var tag in note.TagsForView)
            {
                // Jeśli tag ma id=0 to dodać do tabeli Tag; wszystkie tagi skojarzyć z notatką w tabeli NoteTag
            }

            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetNote", new { id = note.Id }, note);
            return Ok(ModelConverters.ConvertToNoteForView(noteToAdd));
        }

        // DELETE: api/Note/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int userId, int id)
        {
            var note = await _context.Note.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }
			if (note.UserId != userId)
			{
				return Forbid();
			}

			_context.Note.Remove(note);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NoteExists(int id)
        {
            //return _context.Note.Any(e => e.Id == id);
			return (_context.Note?.Any(e => e.Id == id)).GetValueOrDefault();
		}
    }
}
