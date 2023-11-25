using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotkaAPI.Data;
using NotkaAPI.Models.Investments;

namespace NotkaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockNoteController : ControllerBase
    {
        private readonly NotkaDatabaseContext _context;

        public StockNoteController(NotkaDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/StockNote
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockNote>>> GetStockNote()
        {
            return await _context.StockNote.ToListAsync();
        }

        // GET: api/StockNote/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StockNote>> GetStockNote(int id)
        {
            var stockNote = await _context.StockNote.FindAsync(id);

            if (stockNote == null)
            {
                return NotFound();
            }

            return stockNote;
        }

        // PUT: api/StockNote/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStockNote(int id, StockNote stockNote)
        {
            if (id != stockNote.Id)
            {
                return BadRequest();
            }

            _context.Entry(stockNote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockNoteExists(id))
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

        // POST: api/StockNote
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StockNote>> PostStockNote(StockNote stockNote)
        {
            _context.StockNote.Add(stockNote);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStockNote", new { id = stockNote.Id }, stockNote);
        }

        // DELETE: api/StockNote/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockNote(int id)
        {
            var stockNote = await _context.StockNote.FindAsync(id);
            if (stockNote == null)
            {
                return NotFound();
            }

            _context.StockNote.Remove(stockNote);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StockNoteExists(int id)
        {
            return _context.StockNote.Any(e => e.Id == id);
        }
    }
}
