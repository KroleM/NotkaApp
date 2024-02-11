using Microsoft.EntityFrameworkCore;
using NotkaAPI.Contracts;
using NotkaAPI.Data;
using NotkaAPI.Helpers;
using NotkaAPI.Models.BusinessLogic;
using NotkaAPI.Models.Notes;
using NotkaAPI.Parameters;
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
				//return NotFound();	//wyrzucić odpowiednie exception;
			}
			var notes1 = FindByCondition(n => n.UserId == userId &&
										n.CreatedDate >= noteParameters.MinDateOfCreation &&
										n.CreatedDate <= noteParameters.MaxDateOfCreation)
						.Include(note => note.NoteTags)
						.ThenInclude(notetag => notetag.Tag);
						//.Include(note => note.Picture)

			// Include->Picture powyżej prowadzi do duplikowania danych zdjęcia dla każdego wiersza tabeli (bo wiele NoteTagów dla jednej notatki).
			// Propozycja optymalizacji - Split Queries: https://learn.microsoft.com/en-us/ef/core/querying/single-split-queries

			//TBD: Searching

			var notes = await Context.Note
				.Where(n => n.UserId == userId)
				.Include(note => note.NoteTags)
				.ThenInclude(notetag => notetag.Tag)
				//.Include(note => note.Picture)
				.ToListAsync();

			// Include->Picture powyżej prowadzi do duplikowania danych zdjęcia dla każdego wiersza tabeli (bo wiele NoteTagów dla jednej notatki).
			// Propozycja optymalizacji - Split Queries: https://learn.microsoft.com/en-us/ef/core/querying/single-split-queries

			return PagedList<NoteForView>.ToPagedList(notes1.Select(note => ModelConverters.ConvertToNoteForView(note))
															.OrderByDescending(n => n.ModifiedDate),
				noteParameters.PageNumber,
				noteParameters.PageSize);
		}
		public NoteForView GetNoteById(int userId, int id)
		{
			throw new NotImplementedException();
		}
		public void CreateNote(NoteForView note)
		{
			//przyjmuje i zwraca NoteForView?
			//Create(note...);
		}
		public void UpdateNote(int id, NoteForView note)
		{
			throw new NotImplementedException();
		}
		public void DeleteNote(int userId, int id)
		{
			throw new NotImplementedException();
		}
	}
}
