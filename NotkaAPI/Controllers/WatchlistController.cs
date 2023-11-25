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
    public class WatchlistController : ControllerBase
    {
        private readonly NotkaDatabaseContext _context;

        public WatchlistController(NotkaDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Watchlist
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Watchlist>>> GetWatchlist()
        {
            return await _context.Watchlist.ToListAsync();
        }

        // GET: api/Watchlist/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Watchlist>> GetWatchlist(int id)
        {
            var watchlist = await _context.Watchlist.FindAsync(id);

            if (watchlist == null)
            {
                return NotFound();
            }

            return watchlist;
        }

        // PUT: api/Watchlist/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWatchlist(int id, Watchlist watchlist)
        {
            if (id != watchlist.Id)
            {
                return BadRequest();
            }

            _context.Entry(watchlist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WatchlistExists(id))
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

        // POST: api/Watchlist
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Watchlist>> PostWatchlist(Watchlist watchlist)
        {
            _context.Watchlist.Add(watchlist);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWatchlist", new { id = watchlist.Id }, watchlist);
        }

        // DELETE: api/Watchlist/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWatchlist(int id)
        {
            var watchlist = await _context.Watchlist.FindAsync(id);
            if (watchlist == null)
            {
                return NotFound();
            }

            _context.Watchlist.Remove(watchlist);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WatchlistExists(int id)
        {
            return _context.Watchlist.Any(e => e.Id == id);
        }
    }
}
