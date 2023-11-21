using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotkaAPI.Data;
using NotkaAPI.Models.General;

namespace NotkaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PictureController : ControllerBase
    {
        private readonly NotkaDatabaseContext _context;

        public PictureController(NotkaDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Picture
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Picture>>> GetPicture()
        {
            return await _context.Picture.ToListAsync();
        }

        // GET: api/Picture/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Picture>> GetPicture(int id)
        {
            var picture = await _context.Picture.FindAsync(id);

            if (picture == null)
            {
                return NotFound();
            }

            return picture;
        }

        // PUT: api/Picture/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPicture(int id, Picture picture)
        {
            if (id != picture.Id)
            {
                return BadRequest();
            }

            _context.Entry(picture).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PictureExists(id))
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

        // POST: api/Picture
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Picture>> PostPicture(Picture picture)
        {
            _context.Picture.Add(picture);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPicture", new { id = picture.Id }, picture);
        }

        // DELETE: api/Picture/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePicture(int id)
        {
            var picture = await _context.Picture.FindAsync(id);
            if (picture == null)
            {
                return NotFound();
            }

            _context.Picture.Remove(picture);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PictureExists(int id)
        {
            return _context.Picture.Any(e => e.Id == id);
        }
    }
}
