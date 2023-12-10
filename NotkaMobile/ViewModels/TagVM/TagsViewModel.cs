using NotkaMobile.Service.Reference;
using NotkaMobile.ViewModels.Abstract;
using NotkaMobile.Views.Notes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotkaMobile.ViewModels.TagVM
{
    public class TagsViewModel : AListViewModel<Tag>
	{
		public TagsViewModel() : base("Tagi")
		{
		}

		public override async void GoToAddPage()
		{
			await Shell.Current.GoToAsync(nameof(NewTagPage));
		}

		public override async void OnItemSelected(Tag item)
		{
			if (item == null)
			{
				return;
			}
			//await Shell.Current.GoToAsync($"{nameof(NoteDetailsPage)}?{nameof(NoteDetailsViewModel.ItemId)}={item.Id}");
		}
	}
}
