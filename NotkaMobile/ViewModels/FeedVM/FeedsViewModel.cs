using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.Input;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;
using NotkaMobile.Views;
using NotkaMobile.Views.Feed;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace NotkaMobile.ViewModels.FeedVM
{
	public partial class FeedsViewModel : AListViewModel<FeedForView, FeedParameters>
	{
		public FeedsViewModel(FeedDataStore dataStore) 
			: base("Aktualności", dataStore)
		{
			ExecuteLoadItemsCommand();
			CreateFeedWithImageList();
		}
		public ObservableCollection<FeedWithImageViewModel> ItemsWithImage { get; } = new();

		private FeedWithImageViewModel? _newSelectedItem;
		public FeedWithImageViewModel? NewSelectedItem
		{
			get => _newSelectedItem;
			set
			{
				SetProperty(ref _newSelectedItem, value);
				if (value == null) SelectedItem = null;
				else SelectedItem = new FeedForView();
			}
		}

		public override Task GoToAddPage()
		{
			throw new NotImplementedException();
		}
		private ImageSource LoadPhoto(Picture? picture)
		{
			if (picture == null || picture.BitPicture == null)
				return null;

			return ImageSource.FromStream(() => new MemoryStream(picture.BitPicture));
		}
		public override async Task OnItemSelected(FeedForView? item)
		{
			if (item == null)
			{
				return;
			}
			await Shell.Current.GoToAsync($"{nameof(FeedDetailsPage)}?{nameof(FeedDetailsViewModel.ItemId)}={item.Id}");
		}
		[RelayCommand]
		private async Task FeedSelected(FeedWithImageViewModel? item)
		{
			if (item == null)
			{
				return;
			}
			await Shell.Current.GoToAsync($"{nameof(FeedDetailsPage)}?{nameof(FeedDetailsViewModel.ItemId)}={item.Id}");
		}

		[RelayCommand]
		private async Task GoToSettings()
		{
			var userId = Preferences.Default.Get("userId", 0);
			await Shell.Current.GoToAsync($"{nameof(SettingsPage)}?{nameof(SettingsViewModel.ItemId)}={userId}");
		}

		private void CreateFeedWithImageList()
		{
			ItemsWithImage.Clear();
			foreach (var feedForView in Items)
			{
				ItemsWithImage.Add(new FeedWithImageViewModel
				{
					Id = feedForView.Id,
					IsActive = feedForView.IsActive,
					CreatedDate = feedForView.CreatedDate,
					ModifiedDate = feedForView.ModifiedDate,
					Name = feedForView.Name,
					Description = feedForView.Description,
					PhotoSource = LoadPhoto(feedForView.Picture),
				});
			}
		}

		[RelayCommand]
		private async Task LoadMoreItemsWithImage()
		{
			try
			{
				await Task.Delay(10);   //this prevents strange ObservableCollection synchronization error
				if (DataStore.PageParameters.HasNext && Items.Count > 0)
				{
					DataStore.Params.PageNumber++;
					Debug.WriteLine("Page number: {0}", DataStore.Params.PageNumber);
					var items = await DataStore.GetItemsAsync(true);
					foreach (var feedForView in items)
					{
						ItemsWithImage.Add(new FeedWithImageViewModel
						{
							Id = feedForView.Id,
							IsActive = feedForView.IsActive,
							CreatedDate = feedForView.CreatedDate,
							ModifiedDate = feedForView.ModifiedDate,
							Name = feedForView.Name,
							Description = feedForView.Description,
							PhotoSource = LoadPhoto(feedForView.Picture),
						});
					}
					Debug.WriteLine("Items count = {0}", Items.Count);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
		}
	}
}
