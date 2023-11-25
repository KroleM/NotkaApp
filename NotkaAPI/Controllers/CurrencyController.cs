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
    public class CurrencyController : ControllerBase
    {
        private readonly NotkaDatabaseContext _context;

        public CurrencyController(NotkaDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Currency
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Currency>>> GetCurrency()
        {
            return await _context.Currency.ToListAsync();
        }

        // GET: api/Currency/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Currency>> GetCurrency(int id)
        {
            var currency = await _context.Currency.FindAsync(id);

            if (currency == null)
            {
                return NotFound();
            }

            return currency;
        }

        // PUT: api/Currency/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurrency(int id, Currency currency)
        {
            if (id != currency.Id)
            {
                return BadRequest();
            }

            _context.Entry(currency).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurrencyExists(id))
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

        // POST: api/Currency
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Currency>> PostCurrency(Currency currency)
        {
            _context.Currency.Add(currency);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCurrency", new { id = currency.Id }, currency);
        }

        // DELETE: api/Currency/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurrency(int id)
        {
            var currency = await _context.Currency.FindAsync(id);
            if (currency == null)
            {
                return NotFound();
            }

            _context.Currency.Remove(currency);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CurrencyExists(int id)
        {
            return _context.Currency.Any(e => e.Id == id);
        }
    }
}
