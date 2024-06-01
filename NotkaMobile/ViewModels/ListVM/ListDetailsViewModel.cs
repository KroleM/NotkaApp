using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.ComponentModel;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;
using NotkaMobile.Views.Notes.List;

namespace NotkaMobile.ViewModels.ListVM
{
	public partial class ListDetailsViewModel : AItemDetailsViewModel<ListForView, ListParameters>
	{
		public ListDetailsViewModel(ListDataStore dataStore)
			: base(dataStore)
		{
		}
		[ObservableProperty]
		string _name;
		[ObservableProperty]
		string _description;
		[ObservableProperty]
		DateTime _createdDate;
		[ObservableProperty]
		DateTime _modifiedDate;
		[ObservableProperty]
		ICollection<TagForView> _tagsForView = new List<TagForView>();
		[ObservableProperty]
		ICollection<ListElementForView> _listElementsForView = new List<ListElementForView>();
		public override void LoadProperties(ListForView item)
		{
			Name = item.Name;
			Description = item.Description;
			CreatedDate = item.CreatedDate.LocalDateTime;
			ModifiedDate = item.ModifiedDate.LocalDateTime;
			TagsForView = item.TagsForView;
			ListElementsForView = item.ListElementsForView;
		}
		protected async override Task OnEdit()
		{
			await Shell.Current.GoToAsync($"{nameof(ListEditPage)}?{nameof(ListEditViewModel.ItemId)}={ItemId}");
		}
	}
}
