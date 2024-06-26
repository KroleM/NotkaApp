using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.Messaging;
using NotkaDesktop.Helpers;
using NotkaDesktop.Service.Reference;
using NotkaDesktop.Services;
using NotkaDesktop.ViewModels.Abstract;

namespace NotkaDesktop.ViewModels
{
	public class CurrenciesViewModel : AListViewModel<CurrencyForView, CurrencyParameters>
	{
		public CurrenciesViewModel(CurrencyDataStore dataStore) 
			: base("Waluty", dataStore)
		{
		}

		public override Task GoToAddPage()
		{
			WeakReferenceMessenger.Default.Send(new ViewRequestMessage(MainWindowView.NewCurrency));
			return Task.CompletedTask;
		}

		public override async Task OnDeleteItem()
		{
			if (SelectedItem != null)
			{
				await DataStore.DeleteItemAsync(SelectedItem.Id);
			}
			WeakReferenceMessenger.Default.Send(new ViewRequestMessage(MainWindowView.Currencies));
		}

		public override Task OnEditItem()
		{
			WeakReferenceMessenger.Default.Send(new ViewRequestMessage(MainWindowView.EditCurrency));
			return Task.CompletedTask;
		}
	}
}
