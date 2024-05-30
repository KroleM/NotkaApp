using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiSharedClasses.QueryParameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotkaAPI.Contracts;
using NotkaAPI.Data;
using NotkaAPI.Helpers;
using NotkaAPI.Models.Notes;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {
		private readonly IRepositoryWrapper _repository;

        public ListController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        // GET: api/List
        [HttpGet("{userId}", Name = "ListGETAll")]
		public async Task<ActionResult<PagedList<ListForView>>> GetList(int userId, [FromQuery] ListParameters listParameters)
        {
			PagedList<ListForView> lists;
			try
			{
				lists = await _repository.List.GetLists(userId, listParameters);
			}
			catch (NotFoundException)
			{
				return NotFound();
			}
			catch
			{
				return BadRequest();
			}

			return lists;
		}

		// GET: api/List/5
		[HttpGet("{userId}/{id}", Name = "ListGET")]
		public async Task<ActionResult<ListForView>> GetList(int userId, int id)
        {
			try
			{
				return await _repository.List.GetListById(userId, id);
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

		// PUT: api/List/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}", Name = "ListPUT")]
		public async Task<IActionResult> PutList(int id, ListForView list)
        {
			if (id != list.Id)
			{
				return BadRequest();
			}

			try
			{
				await _repository.List.UpdateList(id, list);
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

        // POST: api/List
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost(Name = "ListPOST")]
		public async Task<ActionResult<ListForView>> PostList(ListForView list)
        {
			if (list == null) return Forbid();

			ListForView uploadedList;
			try
			{
				uploadedList = await _repository.List.CreateList(list);
			}
			catch
			{
				return BadRequest();
			}

			return Ok(uploadedList);
		}

        // DELETE: api/List/5/23
		[HttpDelete("{userId}/{id}", Name = "ListDELETE")]
		public async Task<IActionResult> DeleteList(int userId, int id)
		{
			try
			{
				await _repository.List.DeleteList(userId, id);
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
