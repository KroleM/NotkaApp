﻿using System;
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
using static Azure.Core.HttpHeader;

namespace NotkaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly NotkaDatabaseContext _context;

        public TagController(NotkaDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Tag
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<TagForView>>> GetTag(int userId)
        {
			if (_context.Tag.Where(n => n.UserId == userId) == null)
			{
				return NotFound();
			}
			var tags = await _context.Tag
				.Where(n => n.UserId == userId)
				.ToListAsync();

			return tags
	            .Select(tag => ModelConverters.ConvertToTagForView(tag))
	            .OrderByDescending(t => t.Name)
	            .ToList();
			//return await _context.Tag.Where(t => t.UserId == userId).OrderByDescending(t => t.Name).ToListAsync();
		}

        // GET: api/Tag/5
        [HttpGet("{userId}/{id}")]
        public async Task<ActionResult<TagForView>> GetTag(int userId, int id)
        {
            var tag = await _context.Tag.FindAsync(id);

            if (tag == null)
            {
                return NotFound();
            }
			if (tag.UserId != userId)
			{
				return Forbid();
			}

            return ModelConverters.ConvertToTagForView(tag);

		}

        // PUT: api/Tag/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTag(int id, TagForView tag)
        {
            if (id != tag.Id)
            {
                return BadRequest();
            }

            _context.Entry(tag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagExists(id))
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

        // POST: api/Tag
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TagForView>> PostTag(TagForView tag)
        {
			var tagToAdd = new Tag().CopyProperties(tag);
			_context.Tag.Add(tagToAdd);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetTag", new { id = tag.Id }, tag);
            return Ok(ModelConverters.ConvertToTagForView(tagToAdd));
        }

        // DELETE: api/Tag/5
        [HttpDelete("{userId}/{id}")]
        public async Task<IActionResult> DeleteTag(int userId, int id)
        {
            var tag = await _context.Tag.FindAsync(id);
            if (tag == null)
            {
                return NotFound();
            }
			if (tag.UserId != userId)
			{
				return Forbid();
			}

			_context.Tag.Remove(tag);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TagExists(int id)
        {
            return _context.Tag.Any(e => e.Id == id);
        }
    }
}
