﻿using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using NotkaDesktop.Helpers;
using NotkaDesktop.Service.Reference;
using NotkaDesktop.Services;
using NotkaDesktop.ViewModels.Abstract;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NotkaDesktop.ViewModels
{
	public partial class FeedsViewModel : AListViewModel<FeedForView, FeedParameters>
	{
		public FeedsViewModel(FeedDataStore dataStore) 
			: base("Aktualności", dataStore)
		{
			CreateFeedWithImageList();
		}

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
			WeakReferenceMessenger.Default.Send(new ViewRequestMessage(MainWindowView.NewFeed));
			return Task.CompletedTask;
		}

		public override async Task OnDeleteItem()
		{
			if (NewSelectedItem != null)
			{
				await DataStore.DeleteItemAsync(NewSelectedItem.Id);
			}
			WeakReferenceMessenger.Default.Send(new ViewRequestMessage(MainWindowView.Feeds));
		}

		public override Task OnEditItem()
		{
			WeakReferenceMessenger.Default.Send(new ViewRequestMessage(MainWindowView.EditFeed));
			return Task.CompletedTask;
		}

		public ObservableCollection<FeedWithImageViewModel> ItemsWithImage { get; } = new();
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

		private ImageSource? LoadPhoto(Picture? picture)
		{
			if (picture == null || picture.BitPicture == null || picture.BitPicture.Length == 0) return null;
			
			return Helpers.Helpers.LoadPhoto(picture.BitPicture);
		}
	}
}
