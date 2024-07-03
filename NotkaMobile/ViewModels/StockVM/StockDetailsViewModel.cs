using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;
using NotkaMobile.ViewModels.NoteVM;
using NotkaMobile.Views.Notes.Note;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace NotkaMobile.ViewModels.StockVM
{
	public partial class StockDetailsViewModel : AItemDetailsViewModel<StockForView, StockParameters>
	{
		public StockDetailsViewModel(StockDataStore dataStore) 
			: base(dataStore)
		{
		}

		[ObservableProperty]
		string _ticker;
		[ObservableProperty]
		string _name;
		[ObservableProperty]
		string _description;
		[ObservableProperty]
		string _stockExchangeShortName;
		[ObservableProperty]
		string _currencyShortName;
		[ObservableProperty]
		ObservableCollection<NoteForView> _notesForView = new ObservableCollection<NoteForView>();

		public override void LoadProperties(StockForView item)
		{
			Ticker = item.Ticker;
			Name = item.Name;
			Description = item.Description;
			StockExchangeShortName = item.StockExchangeShortName;
			CurrencyShortName = item.CurrencyShortName;

			//NotesForView = item.NotesForViews;
			NotesForView.Clear();
			foreach (var stock in item.NotesForViews)
			{
				NotesForView.Add(stock);
			}
		}

		[RelayCommand]
		async Task AddNote()
		{
			Debug.WriteLine(ItemId);
			await Shell.Current.GoToAsync($"{nameof(NewNotePage)}?{nameof(NewNoteViewModel.StockId)}={ItemId}");
		}

		[RelayCommand]
		async Task NoteTapped(NoteForView item)
		{
			if (item == null)
			{
				return;
			}
			await Shell.Current.GoToAsync($"{nameof(NoteDetailsPage)}?{nameof(NoteDetailsViewModel.ItemId)}={item.Id}");
		}
	}
}
