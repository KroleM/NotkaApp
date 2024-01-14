using CommunityToolkit.Mvvm.ComponentModel;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.Services.Abstract;
using NotkaMobile.ViewModels.Abstract;

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
		public override void LoadProperties(TagForView item)
		{
			Name = item.Name;
			Description = item.Description;
			CreatedDate = item.CreatedDate;
			ModifiedDate = item.ModifiedDate;
		}
	}
}
