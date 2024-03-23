using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.ComponentModel;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;
using NotkaMobile.Views.Notes.Note;

namespace NotkaMobile.ViewModels.NoteVM
{
	public partial class NoteDetailsViewModel : AItemDetailsViewModel<NoteForView, NoteParameters>
	{
		public NoteDetailsViewModel(NoteDataStore dataStore) 
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
		ICollection<TagForView> _tagsForView = new List<TagForView>();
		[ObservableProperty]
		ImageSource? _photoSource;

		public override void LoadProperties(NoteForView item)
		{
			Name = item.Name;
			Description = item.Description;
			CreatedDate = item.CreatedDate;		//.ToLocalTime();?
			ModifiedDate = item.ModifiedDate;
			TagsForView = item.TagsForView;
			PhotoSource = LoadPhoto(item.Picture);
		}
		protected async override void OnEdit()
		{
			await Shell.Current.GoToAsync($"{nameof(NoteEditPage)}?{nameof(NoteEditViewModel.ItemId)}={ItemId}");
		}
		private ImageSource LoadPhoto(Picture? picture)
		{
			if (picture == null) return null;

			return ImageSource.FromStream(() => new MemoryStream(picture.BitPicture));
		}
	}
}
