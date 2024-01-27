using CommunityToolkit.Mvvm.ComponentModel;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;
using NotkaMobile.Views.Notes.Tag;

namespace NotkaMobile.ViewModels.TagVM
{
	public partial class TagDetailsViewModel : AItemDetailsViewModel<TagForView>
	{
		public TagDetailsViewModel(TagDataStore dataStore) 
			: base(dataStore)
		{
		}
		[ObservableProperty]
		string _name;
		[ObservableProperty]
		string _description;
		[ObservableProperty]
		DateTimeOffset _createdDate;
		[ObservableProperty]
		DateTimeOffset _modifiedDate;
		[ObservableProperty]
		ICollection<NoteForView> _notesForView = new List<NoteForView>();
		public override void LoadProperties(TagForView item)
		{
			Name = item.Name;
			Description = item.Description;
			CreatedDate = item.CreatedDate;
			ModifiedDate = item.ModifiedDate;
			NotesForView = item.NotesForView;
		}
		protected async override void OnEdit()
		{
			await Shell.Current.GoToAsync($"{nameof(TagEditPage)}?{nameof(TagEditViewModel.ItemId)}={ItemId}");
		}
	}
}
