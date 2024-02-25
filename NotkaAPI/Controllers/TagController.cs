using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiSharedClasses.QueryParameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NotkaAPI.Contracts;
using NotkaAPI.Data;
using NotkaAPI.Helpers;
using NotkaAPI.Models.BusinessLogic;
using NotkaAPI.Models.Notes;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly NotkaDatabaseContext _context;
		private readonly IRepositoryWrapper _repository;

		public TagController(NotkaDatabaseContext context, IRepositoryWrapper repository)
        {
            _context = context;
			_repository = repository;
		}

        // GET: api/Tag
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<TagForView>>> GetTag(int userId, [FromQuery] TagParameters tagParameters)
        {
			PagedList<TagForView> tags;
			try
			{
				tags = await _repository.Tag.GetTags(userId, tagParameters);
			}
			catch (NotFoundException)
			{
				return NotFound();
			}
			catch
			{
				//FIXME general exception might also produce NotFound()
				return BadRequest();
			}

			var metadata = new
			{
				tags.TotalCount,
				tags.PageSize,
				tags.CurrentPage,
				tags.TotalPages,
				tags.HasNext,
				tags.HasPrevious
			};

			Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

			return tags;
		}

		// GET: api/Tag/1/5
		//[HttpGet]   //np. api/Tag?userId=1&id=2
		[HttpGet("{userId}/{id}")]
		public async Task<ActionResult<TagForView>> GetTag(int userId, int id)
        {
			if (!await _context.Tag.AnyAsync(n => n.Id == id))
			{
				return NotFound();
			}
            var tag = await _context.Tag
                .Include(tag => tag.NoteTags)
                .ThenInclude(notetag => notetag.Note)
				.ThenInclude(note => note.NoteTags)
				.ThenInclude(notetag => notetag.Tag)
				.SingleOrDefaultAsync(tag => tag.Id == id);
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
            //Przemienić TagForView na Tag ??
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
			if (!await _context.Tag.AnyAsync(n => n.Id == id))
			{
				return NotFound();
			}
            //Proper relationship child (dependent) has to be included (loaded) in order to cascade-delete.
			var tag = await _context.Tag
                .Include(tag => tag.NoteTags)
                .SingleOrDefaultAsync(tag => tag.Id == id);

			if (tag.UserId != userId)
			{
				return Forbid();
			}

			//_context.Tag.Remove(tag);
			_context.Remove(tag);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TagExists(int id)
        {
            return _context.Tag.Any(e => e.Id == id);
        }
    }
}
