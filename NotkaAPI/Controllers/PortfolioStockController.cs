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
    public class PortfolioStockController : ControllerBase
    {
        private readonly NotkaDatabaseContext _context;

        public PortfolioStockController(NotkaDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/PortfolioStock
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PortfolioStock>>> GetPortfolioStock()
        {
            return await _context.PortfolioStock.ToListAsync();
        }

        // GET: api/PortfolioStock/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PortfolioStock>> GetPortfolioStock(int id)
        {
            var portfolioStock = await _context.PortfolioStock.FindAsync(id);

            if (portfolioStock == null)
            {
                return NotFound();
            }

            return portfolioStock;
        }

        // PUT: api/PortfolioStock/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPortfolioStock(int id, PortfolioStock portfolioStock)
        {
            if (id != portfolioStock.Id)
            {
                return BadRequest();
            }

            _context.Entry(portfolioStock).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PortfolioStockExists(id))
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

        // POST: api/PortfolioStock
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PortfolioStock>> PostPortfolioStock(PortfolioStock portfolioStock)
        {
            _context.PortfolioStock.Add(portfolioStock);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPortfolioStock", new { id = portfolioStock.Id }, portfolioStock);
        }

        // DELETE: api/PortfolioStock/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePortfolioStock(int id)
        {
            var portfolioStock = await _context.PortfolioStock.FindAsync(id);
            if (portfolioStock == null)
            {
                return NotFound();
            }

            _context.PortfolioStock.Remove(portfolioStock);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PortfolioStockExists(int id)
        {
            return _context.PortfolioStock.Any(e => e.Id == id);
        }
    }
}
