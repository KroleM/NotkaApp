﻿using ApiSharedClasses.QueryParameters;
using Microsoft.AspNetCore.Mvc;
using NotkaAPI.Contracts;
using NotkaAPI.Helpers;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
		private readonly IRepositoryWrapper _repository;

		public UserController(IRepositoryWrapper repository)
		{
			_repository = repository;
		}

		// GET: api/User
		[HttpGet("{userId}", Name = "UserGETAll")]
        public async Task<ActionResult<PagedList<UserForView>>> GetUser(int userId, [FromQuery] UserParameters userParameters)
        {
			PagedList<UserForView> users;
			try
			{
				users = await _repository.User.GetUsers(userId, userParameters);
			}
			catch (NotFoundException)
			{
				return NotFound();
			}
			catch
			{
				return BadRequest();
			}

            return users;
        }

		// GET: api/User/5
		[HttpGet("{userId:int}/{id}", Name = "UserGET")]	//{userId:int} - route constraint
		public async Task<ActionResult<UserForView>> GetUser(int userId, int id)
        {
			try
			{
				return await _repository.User.GetUserById(userId, id);
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

		[HttpGet("{email}/{hash}", Name = "UserGETWithAuth")]
		public async Task<ActionResult<UserForView>> GetUserWithAuth(string email, string hash)
		{
			try
			{
				return await _repository.User.GetUserWithAuth(email, hash);
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

		// PUT: api/User/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}", Name = "UserPUT")]
        public async Task<IActionResult> PutUser(int id, UserForView user)
        {
			if (id != user.Id)
			{
				return BadRequest();
			}

			try
			{
				await _repository.User.UpdateUser(id, user);
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

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost(Name = "UserPOST")]
        public async Task<ActionResult<UserForView>> PostUser(UserForView user)
        {
			if (user == null) return Forbid();

			UserForView uploadedUser;
			try
			{
				uploadedUser = await _repository.User.CreateUser(user);
			}
			catch (ConflictException)
			{
				return Conflict();
			}
			catch
			{
				return BadRequest();
			}

			return Ok(uploadedUser);
		}

        // DELETE: api/User/5
        [HttpDelete("{userId}/{id}", Name = "UserDELETE")]
        public async Task<IActionResult> DeleteUser(int userId, int id)
        {
			try
			{
				await _repository.User.DeleteUser(userId, id);
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
