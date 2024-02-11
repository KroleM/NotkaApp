using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;
using NotkaMobile.ViewModels.NoteVM;
using NotkaMobile.Views.Notes.Note;
using NotkaMobile.Views.Notes.Tag;

namespace NotkaMobile.ViewModels.TagVM
{
	public partial class TagDetailsViewModel : AItemDetailsViewModel<TagForView, TagParameters>
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
			NotesForView = item.NotesForView;	//dopisywanie w foreach?
		}
		protected async override void OnEdit()
		{
			await Shell.Current.GoToAsync($"{nameof(TagEditPage)}?{nameof(TagEditViewModel.ItemId)}={ItemId}");
		}
		[RelayCommand]
		async System.Threading.Tasks.Task NoteTapped(NoteForView item)
		{
			if (item == null)
			{
				return;
			}
			await Shell.Current.GoToAsync($"{nameof(NoteDetailsPage)}?{nameof(NoteDetailsViewModel.ItemId)}={item.Id}");
		}
	}
}
