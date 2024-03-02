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
			try
			{
				return await _repository.Tag.GetTagById(userId, id);
			}
			catch (NotFoundException)
			{
				return NotFound();
			}
			catch (UnauthorizedException)
			{
				return Unauthorized();
			}
			catch
			{
				return BadRequest();
			}
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

            try
            {
                await _repository.Tag.UpdateTag(id, tag);
			}
			catch (NotFoundException)
			{
				return NotFound();
			}
			catch
			{
				return BadRequest();
			}

			return NoContent();
		}

        // POST: api/Tag
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TagForView>> PostTag(TagForView tag)
        {
			if (tag == null) return Forbid();

			TagForView uploadedTag;
			try
			{
				uploadedTag = await _repository.Tag.CreateTag(tag);
			}
			catch
			{
				return BadRequest();
			}

			return Ok(uploadedTag);
		}

        // DELETE: api/Tag/5
        [HttpDelete("{userId}/{id}")]
        public async Task<IActionResult> DeleteTag(int userId, int id)
        {
			try
			{
				await _repository.Tag.DeleteTag(userId, id);
			}
			catch (NotFoundException)
			{
				return NotFound();
			}
			catch (ForbidException)
			{
				return Forbid();
			}

			return NoContent();
		}

        private bool TagExists(int id)
        {
            return _context.Tag.Any(e => e.Id == id);
        }
    }
}
