using ApiSharedClasses.QueryParameters;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotkaAPI.Contracts;
using NotkaAPI.Helpers;
using NotkaAPI.Models.CMS;
using NotkaAPI.Models.Notes;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class FeedController : ControllerBase
    {
		private readonly IRepositoryWrapper _repository;

		public FeedController(IRepositoryWrapper repository)
        {
			_repository = repository;
		}

		// GET: api/Feed
		[HttpGet("{userId}", Name = "FeedGETAll")]
		public async Task<ActionResult<PagedList<FeedForView>>> GetFeed(int userId, [FromQuery] FeedParameters feedParameters)
		{
			PagedList<FeedForView> feeds;
			try
			{
				feeds = await _repository.Feed.GetFeeds(userId, feedParameters);
			}
			catch (NotFoundException)
			{
				return NotFound();
			}
			catch
			{
				return BadRequest();
			}

			return feeds;
		}

		[HttpGet("{userId}/{id}", Name = "FeedGET")]
		public async Task<ActionResult<FeedForView>> GetFeed(int userId, int id)
		{
			try
			{
				return await _repository.Feed.GetFeedById(userId, id);
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

        // PUT: api/Feed/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}", Name = "FeedPUT")]
		public async Task<IActionResult> PutFeed(int id, FeedForView feed)
        {
            if (id != feed.Id)
            {
                return BadRequest();
			}

			try
			{
				await _repository.Feed.UpdateFeed(id, feed);
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

		// POST: api/Feed
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost(Name = "FeedPOST")]
		public async Task<ActionResult<FeedForView>> PostFeed(int userId, FeedForView feed)
        {
			if (feed == null) return Forbid();

			FeedForView uploadedFeed;
			try
			{
				uploadedFeed = await _repository.Feed.CreateFeed(userId, feed);
			}
			catch
			{
				return BadRequest();
			}

			return Ok(uploadedFeed);
		}

		// DELETE: api/Feed/5/1
		[HttpDelete("{userId}/{id}", Name = "FeedDELETE")]
		public async Task<IActionResult> DeleteFeed(int userId, int id)
		{
			try
			{
				await _repository.Feed.DeleteFeed(userId, id);
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
