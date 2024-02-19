using ApiSharedClasses.QueryParameters;
using NotkaAPI.Helpers;
using NotkaAPI.ViewModels;

namespace NotkaAPI.Contracts
{
	public interface INoteRepository	// : IRepositoryBase<NoteForView>
	{
		Task<PagedList<NoteForView>> GetNotes(int userId, NoteParameters noteParameters);
		NoteForView GetNoteById(int userId, int id);
		void CreateNote(NoteForView note);
		void UpdateNote(int id, NoteForView note);
		void DeleteNote(int userId, int id);
	}
}
