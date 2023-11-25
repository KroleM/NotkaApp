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
    public class StockExchangeController : ControllerBase
    {
        private readonly NotkaDatabaseContext _context;

        public StockExchangeController(NotkaDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/StockExchange
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockExchange>>> GetStockExchange()
        {
            return await _context.StockExchange.ToListAsync();
        }

        // GET: api/StockExchange/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StockExchange>> GetStockExchange(int id)
        {
            var stockExchange = await _context.StockExchange.FindAsync(id);

            if (stockExchange == null)
            {
                return NotFound();
            }

            return stockExchange;
        }

        // PUT: api/StockExchange/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStockExchange(int id, StockExchange stockExchange)
        {
            if (id != stockExchange.Id)
            {
                return BadRequest();
            }

            _context.Entry(stockExchange).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockExchangeExists(id))
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

        // POST: api/StockExchange
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StockExchange>> PostStockExchange(StockExchange stockExchange)
        {
            _context.StockExchange.Add(stockExchange);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStockExchange", new { id = stockExchange.Id }, stockExchange);
        }

        // DELETE: api/StockExchange/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockExchange(int id)
        {
            var stockExchange = await _context.StockExchange.FindAsync(id);
            if (stockExchange == null)
            {
                return NotFound();
            }

            _context.StockExchange.Remove(stockExchange);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StockExchangeExists(int id)
        {
            return _context.StockExchange.Any(e => e.Id == id);
        }
    }
}
