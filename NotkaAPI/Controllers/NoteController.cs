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
		private readonly NotkaDatabaseContext _context;
		private readonly IRepositoryWrapper _repository;

		public NoteController(NotkaDatabaseContext context, IRepositoryWrapper repository)
		{
			_context = context;
			_repository = repository;
		}

		// GET: api/Note
		[HttpGet("{userId}")]
		public async Task<ActionResult<IEnumerable<NoteForView>>> GetNote(int userId, [FromQuery] NoteParameters noteParameters) //FIXME PagedList? [FromQuery]??
		{
			if (!noteParameters.ValidTimeRange)
			{
				return BadRequest("Max date cannot be less than min date");
			}
			
			var notes = await _repository.Note.GetNotes(userId, noteParameters);

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
		[HttpGet("{userId}/{id}")]
		public async Task<ActionResult<NoteForView>> GetNote(int userId, int id)
		{
			//var note = await _context.Note.FindAsync(id);
			if (!await _context.Note.AnyAsync(n => n.Id == id))
			{
				return NotFound();
			}
			var note = await _context.Note
				.Include(note => note.NoteTags)
				.ThenInclude(notetag => notetag.Tag)
				.Include(note => note.Picture)
				.SingleOrDefaultAsync(note => note.Id == id);
			if (note.UserId != userId)
			{
				return Forbid();
			}

			return ModelConverters.ConvertToNoteForView(note);
		}

		// PUT: api/Note/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutNote(int id, NoteForView note)
		{
			if (id != note.Id)
			{
				return BadRequest();
			}

			var noteToAdd = new Note().CopyProperties(note);
			_context.Entry(noteToAdd).State = EntityState.Modified;

			try
			{
				using (var dbContextTransaction = _context.Database.BeginTransaction())
				{
					//NoteTags
					_context.NoteTag.RemoveRange(await _context.NoteTag.Where(nt => nt.NoteId == noteToAdd.Id).ToArrayAsync());
					if (!note.TagsForView.IsNullOrEmpty())
					{
						foreach (var tagForView in note.TagsForView)
						{
							var tag = new Tag().CopyProperties(tagForView);
							if (tag.Id == 0)
							{
								_context.Tag.Add(tag);
								await _context.SaveChangesAsync();
							}
							_context.NoteTag.Add(new NoteTag
							{
								Id = 0,
								IsActive = true,
								NoteId = noteToAdd.Id,
								TagId = tag.Id,
							});
						}
					}
					//Picture
					var picture = await _context.Picture.SingleOrDefaultAsync(p => p.NoteId == noteToAdd.Id);
					if (noteToAdd.Picture?.Id != picture?.Id)	//execute only if Picture has changed
					{
						if (picture != null)
							_context.Picture.Remove(picture);
						await _context.SaveChangesAsync(); //to pozwala zapisać zdjęcie
						if (noteToAdd.Picture != null)
						{
							await _context.Picture.AddAsync(noteToAdd.Picture);
						}
					}

					await _context.SaveChangesAsync();
					dbContextTransaction.Commit();
				}
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!NoteExists(id))
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

		// POST: api/Note
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<NoteForView>> PostNote(NoteForView note)
		{
			if (note == null) return Forbid();

			var noteToAdd = new Note().CopyProperties(note);

			using (var dbContextTransaction = _context.Database.BeginTransaction())
			{
				//This goes first, because later noteToAdd.Id can be used
				_context.Note.Add(noteToAdd);
				await _context.SaveChangesAsync();

				if (!note.TagsForView.IsNullOrEmpty())
				{
					foreach (var tagForView in note.TagsForView)
					{
						var tag = new Tag().CopyProperties(tagForView);
						if (tagForView.Id == 0)
						{
							_context.Tag.Add(tag);
							await _context.SaveChangesAsync();
						}

						_context.NoteTag.Add(new NoteTag
						{
							Id = 0,
							IsActive = true,
							NoteId = noteToAdd.Id,
							TagId = tag.Id,
						});
						await _context.SaveChangesAsync();
					}
				}
				dbContextTransaction.Commit();
			}

			var uploadedNote = await _context.Note
				.Include(note => note.NoteTags)
				.ThenInclude(notetag => notetag.Tag)
				.Include(note => note.Picture)
				.SingleOrDefaultAsync(note => note.Id == noteToAdd.Id);
			//await _context.Entry(uploadedNote).ReloadAsync();
			return Ok(ModelConverters.ConvertToNoteForView(uploadedNote));
		}

		// DELETE: api/Note/5
		[HttpDelete("{userId}/{id}")]
		public async Task<IActionResult> DeleteNote(int userId, int id)
		{
			var note = await _context.Note.FindAsync(id);   //Include.Picture? - when note is deleted, the picture is deleted too
			if (note == null)
			{
				return NotFound();
			}
			if (note.UserId != userId)
			{
				return Forbid();
			}

			_context.Note.Remove(note);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool NoteExists(int id)
		{
			//return _context.Note.Any(e => e.Id == id);
			return (_context.Note?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
