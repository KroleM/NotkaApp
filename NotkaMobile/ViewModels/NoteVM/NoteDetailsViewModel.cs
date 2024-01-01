using CommunityToolkit.Mvvm.ComponentModel;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;

namespace NotkaMobile.ViewModels.NoteVM
{
	public partial class NoteDetailsViewModel : AItemDetailsViewModel<NoteForView>
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

		public override void LoadProperties(NoteForView item)
		{
			Name = item.Name;
			Description = item.Description;
			CreatedDate = item.CreatedDate;
			ModifiedDate = item.ModifiedDate;
			TagsForView = item.TagsForView;
		}
	}
}
