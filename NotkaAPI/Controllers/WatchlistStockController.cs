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
    public class WatchlistStockController : ControllerBase
    {
        private readonly NotkaDatabaseContext _context;

        public WatchlistStockController(NotkaDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/WatchlistStock
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WatchlistStock>>> GetWatchlistStock()
        {
            return await _context.WatchlistStock.ToListAsync();
        }

        // GET: api/WatchlistStock/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WatchlistStock>> GetWatchlistStock(int id)
        {
            var watchlistStock = await _context.WatchlistStock.FindAsync(id);

            if (watchlistStock == null)
            {
                return NotFound();
            }

            return watchlistStock;
        }

        // PUT: api/WatchlistStock/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWatchlistStock(int id, WatchlistStock watchlistStock)
        {
            if (id != watchlistStock.Id)
            {
                return BadRequest();
            }

            _context.Entry(watchlistStock).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WatchlistStockExists(id))
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

        // POST: api/WatchlistStock
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WatchlistStock>> PostWatchlistStock(WatchlistStock watchlistStock)
        {
            _context.WatchlistStock.Add(watchlistStock);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWatchlistStock", new { id = watchlistStock.Id }, watchlistStock);
        }

        // DELETE: api/WatchlistStock/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWatchlistStock(int id)
        {
            var watchlistStock = await _context.WatchlistStock.FindAsync(id);
            if (watchlistStock == null)
            {
                return NotFound();
            }

            _context.WatchlistStock.Remove(watchlistStock);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WatchlistStockExists(int id)
        {
            return _context.WatchlistStock.Any(e => e.Id == id);
        }
    }
}
