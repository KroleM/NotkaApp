using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.ComponentModel;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.Services.Abstract;
using NotkaMobile.ViewModels.Abstract;
using System.Collections.ObjectModel;

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
		DateTime _createdDate;
		[ObservableProperty]
		DateTime _modifiedDate;
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
			CreatedDate = item.CreatedDate.LocalDateTime;
			ModifiedDate = item.ModifiedDate.LocalDateTime;
			StockExchangeShortName = item.StockExchangeShortName;
			CurrencyShortName = item.CurrencyShortName;

			NotesForView.Clear();
			foreach (var stock in item.NotesForViews)
			{
				NotesForView.Add(stock);
			}
		}
	}
}
