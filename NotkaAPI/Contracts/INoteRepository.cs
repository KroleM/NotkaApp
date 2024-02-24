using ApiSharedClasses.QueryParameters;
using NotkaAPI.Helpers;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Contracts
{
	public interface INoteRepository	// : IRepositoryBase<NoteForView>
	{
		Task<PagedList<NoteForView>> GetNotes(int userId, NoteParameters noteParameters);
		Task<NoteForView> GetNoteById(int userId, int id);
		Task<NoteForView> CreateNote(NoteForView note);
		Task UpdateNote(int id, NoteForView note);
		Task DeleteNote(int userId, int id);
	}
}
