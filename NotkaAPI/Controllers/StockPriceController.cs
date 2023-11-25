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
    public class StockPriceController : ControllerBase
    {
        private readonly NotkaDatabaseContext _context;

        public StockPriceController(NotkaDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/StockPrice
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockPrice>>> GetStockPrice()
        {
            return await _context.StockPrice.ToListAsync();
        }

        // GET: api/StockPrice/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StockPrice>> GetStockPrice(int id)
        {
            var stockPrice = await _context.StockPrice.FindAsync(id);

            if (stockPrice == null)
            {
                return NotFound();
            }

            return stockPrice;
        }

        // PUT: api/StockPrice/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStockPrice(int id, StockPrice stockPrice)
        {
            if (id != stockPrice.Id)
            {
                return BadRequest();
            }

            _context.Entry(stockPrice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockPriceExists(id))
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

        // POST: api/StockPrice
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StockPrice>> PostStockPrice(StockPrice stockPrice)
        {
            _context.StockPrice.Add(stockPrice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStockPrice", new { id = stockPrice.Id }, stockPrice);
        }

        // DELETE: api/StockPrice/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockPrice(int id)
        {
            var stockPrice = await _context.StockPrice.FindAsync(id);
            if (stockPrice == null)
            {
                return NotFound();
            }

            _context.StockPrice.Remove(stockPrice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StockPriceExists(int id)
        {
            return _context.StockPrice.Any(e => e.Id == id);
        }
    }
}
