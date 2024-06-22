using ApiSharedClasses.QueryParameters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NotkaAPI.Contracts;
using NotkaAPI.Helpers;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
		private readonly IRepositoryWrapper _repository;

		public TagController(IRepositoryWrapper repository)
        {
			_repository = repository;
		}

        // GET: api/Tag
        [HttpGet("{userId}", Name = "TagGETAll")]
        public async Task<ActionResult<PagedList<TagForView>>> GetTag(int userId, [FromQuery] TagParameters tagParameters)
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

			//Below metadata is not used
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
		[HttpGet("{userId}/{id}", Name = "TagGET")]
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
        [HttpPut("{id}", Name = "TagPUT")]
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
        [HttpPost(Name = "TagPOST")]
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

        // DELETE: api/Tag/5/1
        [HttpDelete("{userId}/{id}", Name = "TagDELETE")]
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
    }
}
