using ApiSharedClasses.QueryParameters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.IdentityModel.Tokens;
using NotkaAPI.Contracts;
using NotkaAPI.Data;
using NotkaAPI.Helpers;
using NotkaAPI.Models.BusinessLogic;
using NotkaAPI.Models.Notes;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Repository
{
	public class NoteRepository : RepositoryBase<Note>, INoteRepository
	{
		public NoteRepository(NotkaDatabaseContext repositoryContext) 
			: base(repositoryContext)
		{
		}
		public async Task<PagedList<NoteForView>> GetNotes(int userId, NoteParameters noteParameters)
		{
			if (Context.Note.Where(n => n.UserId == userId) == null)
			{
				throw new NotFoundException();
			}

			var notes = FindByCondition(n => n.UserId == userId
										&& n.CreatedDate >= noteParameters.MinDateOfCreation
										&& n.CreatedDate <= noteParameters.MaxDateOfCreation);

			SearchByPhrase(ref notes, noteParameters.SearchPhrase);

			//TBD Sorting?
			
			var notesWithIncludes = notes.Include(note => note.NoteTags).ThenInclude(notetag => notetag.Tag);
			//.Include(note => note.Picture)

			// Include->Picture powyżej prowadzi do duplikowania danych zdjęcia dla każdego wiersza tabeli (bo wiele NoteTagów dla jednej notatki).
			// Propozycja optymalizacji - Split Queries: https://learn.microsoft.com/en-us/ef/core/querying/single-split-queries

			return await PagedList<NoteForView>.CreateAsync(notesWithIncludes.OrderByDescending(n => n.ModifiedDate)
						.Select(note => ModelConverters.ConvertToNoteForView(note)),
					noteParameters.PageNumber,
					noteParameters.PageSize);
		}

		public async Task<NoteForView> GetNoteById(int userId, int id)
		{
			if (!await Context.Note.AnyAsync(n => n.Id == id))
			{
				throw new NotFoundException();
			}
			var note = await Context.Note
				.Include(note => note.NoteTags)
				.ThenInclude(notetag => notetag.Tag)
				.Include(note => note.Picture)
				.SingleOrDefaultAsync(note => note.Id == id);
			if (note.UserId != userId)
			{
				throw new UnauthorizedException();
			}

			return ModelConverters.ConvertToNoteForView(note);
		}
		public async Task<NoteForView> CreateNote(NoteForView note)
		{
			var noteToAdd = new Note().CopyProperties(note);

			using (var dbContextTransaction = Context.Database.BeginTransaction())
			{
				//This goes first, so that later noteToAdd.Id can be extracted
				//Context.Note.Add(noteToAdd);
				Create(noteToAdd);
				await Context.SaveChangesAsync();

				if (!note.TagsForView.IsNullOrEmpty())
				{
					foreach (var tagForView in note.TagsForView)
					{
						await AddToContextNoteTagAsync(tagForView, noteToAdd.Id);
						await Context.SaveChangesAsync();
					}
				}
				dbContextTransaction.Commit();
			}

			var uploadedNote = await Context.Note
				.Include(note => note.NoteTags)
				.ThenInclude(notetag => notetag.Tag)
				.Include(note => note.Picture)
				.SingleOrDefaultAsync(note => note.Id == noteToAdd.Id);
			//await _context.Entry(uploadedNote).ReloadAsync();
			return ModelConverters.ConvertToNoteForView(uploadedNote);
		}
		public async Task UpdateNote(int id, NoteForView note)
		{
			var noteToAdd = new Note().CopyProperties(note);
			Context.Entry(noteToAdd).State = EntityState.Modified;

			try
			{
				using (var dbContextTransaction = Context.Database.BeginTransaction())
				{
					//NoteTags
					Context.NoteTag.RemoveRange(await Context.NoteTag.Where(nt => nt.NoteId == noteToAdd.Id).ToArrayAsync());
					if (!note.TagsForView.IsNullOrEmpty())
					{
						foreach (var tagForView in note.TagsForView)
						{
							await AddToContextNoteTagAsync(tagForView, noteToAdd.Id);
						}
					}
					//Picture
					var picture = await Context.Picture.SingleOrDefaultAsync(p => p.NoteId == noteToAdd.Id);
					if (noteToAdd.Picture?.Id != picture?.Id)   //execute only if Picture has changed
					{
						if (picture != null)
							Context.Picture.Remove(picture);
						await Context.SaveChangesAsync(); //to pozwala zapisać zdjęcie
						if (noteToAdd.Picture != null)
						{
							await Context.Picture.AddAsync(noteToAdd.Picture);
						}
					}

					await Context.SaveChangesAsync();
					dbContextTransaction.Commit();
				}
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!NoteExists(id))
				{
					throw new NotFoundException();
				}
				else
				{
					throw;
				}
			}
		}
		public async Task DeleteNote(int userId, int id)
		{
			var note = await Context.Note.FindAsync(id);   //Include.Picture? - when note is deleted, the picture is deleted too
			if (note == null)
			{
				throw new NotFoundException();
			}
			if (note.UserId != userId)
			{
				throw new ForbidException();
			}

			Delete(note);
			await Context.SaveChangesAsync();
		}
		private bool NoteExists(int id)
		{
			//return _context.Note.Any(e => e.Id == id);
			return (Context.Note?.Any(e => e.Id == id)).GetValueOrDefault();
		}
		private async Task AddToContextNoteTagAsync(TagForView tagForView, int noteId)
		{
			var tag = new Tag().CopyProperties(tagForView);
			if (tag.Id == 0)
			{
				Context.Tag.Add(tag);
				await Context.SaveChangesAsync();
			}
			Context.NoteTag.Add(new NoteTag
			{
				Id = 0,
				IsActive = true,
				NoteId = noteId,
				TagId = tag.Id,
			});
		}
		private void SearchByPhrase(ref IQueryable<Note> notes, string? searchPhrase)
		{
			if (!notes.Any() || string.IsNullOrWhiteSpace(searchPhrase))
				return;
			notes = notes.Where(n => n.Name.ToLower().Contains(searchPhrase.Trim().ToLower()));
		}
	}
}
