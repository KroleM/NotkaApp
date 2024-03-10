using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiSharedClasses.QueryParameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NotkaAPI.Contracts;
using NotkaAPI.Data;
using NotkaAPI.Helpers;
using NotkaAPI.Models.BusinessLogic;
using NotkaAPI.Models.General;
using NotkaAPI.Models.Notes;
using NotkaAPI.Models.Users;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NoteController : ControllerBase
	{
		private readonly IRepositoryWrapper _repository;

		public NoteController(IRepositoryWrapper repository)
		{
			_repository = repository;
		}

		// GET: api/Note
		[HttpGet("{userId}", Name = "NoteGETAll")]
		public async Task<ActionResult<PagedList<NoteForView>>> GetNote(int userId, [FromQuery] NoteParameters noteParameters)
		{
			if (!noteParameters.ValidTimeRange)
			{
				return BadRequest("Max date cannot be less than min date");
			}

			PagedList<NoteForView> notes;
			try
			{
				notes = await _repository.Note.GetNotes(userId, noteParameters);
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
				notes.TotalCount,
				notes.PageSize,
				notes.CurrentPage,
				notes.TotalPages,
				notes.HasNext,
				notes.HasPrevious
			};

			Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

			return notes;
		}

		// GET: api/Note/1/5
		[HttpGet("{userId}/{id}", Name = "NoteGET")]
		public async Task<ActionResult<NoteForView>> GetNote(int userId, int id)
		{
			try
			{
				return await _repository.Note.GetNoteById(userId, id);
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

		// PUT: api/Note/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}", Name = "NotePUT")]
		public async Task<IActionResult> PutNote(int id, NoteForView note)
		{
			if (id != note.Id)
			{
				return BadRequest();
			}

			try
			{
				await _repository.Note.UpdateNote(id, note);
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

		// POST: api/Note
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost(Name = "NotePOST")]
		public async Task<ActionResult<NoteForView>> PostNote(NoteForView note)
		{
			if (note == null) return Forbid();

			NoteForView uploadedNote;
			try
			{
				uploadedNote = await _repository.Note.CreateNote(note);
			}
			catch 
			{
				return BadRequest();
			}

			return Ok(uploadedNote);
		}

		// DELETE: api/Note/5
		[HttpDelete("{userId}/{id}", Name = "NoteDELETE")]
		public async Task<IActionResult> DeleteNote(int userId, int id)
		{
			try
			{
				await _repository.Note.DeleteNote(userId, id);
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

		//private bool NoteExists(int id)
		//{
		//	//return _context.Note.Any(e => e.Id == id);
		//	return (_context.Note?.Any(e => e.Id == id)).GetValueOrDefault();
		//}
	}
}
