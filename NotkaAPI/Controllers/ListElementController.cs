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
    public class ListElementController : ControllerBase
    {
        private readonly NotkaDatabaseContext _context;

        public ListElementController(NotkaDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/ListElement
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListElement>>> GetListElement()
        {
            return await _context.ListElement.ToListAsync();
        }

        // GET: api/ListElement/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ListElement>> GetListElement(int id)
        {
            var listElement = await _context.ListElement.FindAsync(id);

            if (listElement == null)
            {
                return NotFound();
            }

            return listElement;
        }

        // PUT: api/ListElement/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutListElement(int id, ListElement listElement)
        {
            if (id != listElement.Id)
            {
                return BadRequest();
            }

            _context.Entry(listElement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListElementExists(id))
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

        // POST: api/ListElement
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ListElement>> PostListElement(ListElement listElement)
        {
            _context.ListElement.Add(listElement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetListElement", new { id = listElement.Id }, listElement);
        }

        // DELETE: api/ListElement/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteListElement(int id)
        {
            var listElement = await _context.ListElement.FindAsync(id);
            if (listElement == null)
            {
                return NotFound();
            }

            _context.ListElement.Remove(listElement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ListElementExists(int id)
        {
            return _context.ListElement.Any(e => e.Id == id);
        }
    }
}
