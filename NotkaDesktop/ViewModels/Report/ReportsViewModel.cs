using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NotkaDesktop.Helpers;
using NotkaDesktop.Services;
using NotkaDesktop.ViewModels.Abstract;
using System.Collections.ObjectModel;

namespace NotkaDesktop.ViewModels
{
	public partial class ReportsViewModel : BaseViewModel
	{
		#region Constructor
		public ReportsViewModel(ReportStocksDataStore reportStocksDataStore, ReportUsersDataStore reportUsersDataStore)
		{
			Title = "Raporty";
			_reportStocksDataStore = reportStocksDataStore;
			_reportUsersDataStore = reportUsersDataStore;
			ReportTypesDictionary.Add(ReportType.None, "Brak");
			ReportTypesDictionary.Add(ReportType.MostPopularStocks, "Najpopularniejsze spółki");
			ReportTypesDictionary.Add(ReportType.UserStatistics, "Statystyki użytkowników");
		}
		#endregion
		#region Properties
		private ReportStocksDataStore _reportStocksDataStore;
		private ReportUsersDataStore _reportUsersDataStore;
		public Dictionary<ReportType, string> ReportTypesDictionary { get; } = new Dictionary<ReportType, string>();

		//[ObservableProperty]
		private ReportType _selectedReportType;
		public ReportType SelectedReportType
		{
			get => _selectedReportType;
			set
			{
				if (_selectedReportType == value) return;
				_selectedReportType = value;
				OnPropertyChanged();
				GenerateReportCommand.NotifyCanExecuteChanged();
			}
		}

		[ObservableProperty]
		private BaseViewModel? _contentViewModel;
		#endregion
		#region Commands

		[RelayCommand(CanExecute = nameof(CanGenerate))]
		private async Task GenerateReport()
		{
			ContentViewModel = SelectedReportType switch
			{
				ReportType.MostPopularStocks => new StocksReportViewModel(_reportStocksDataStore),
				ReportType.UserStatistics => new UsersReportViewModel(_reportUsersDataStore)
			};
			//ContentViewModel = new StocksReportViewModel(_reportStocksDataStore);
		}

		private bool CanGenerate()
		{
			if (SelectedReportType == ReportType.None)
			{
				return false;
			}
			return true;
		}
		#endregion
	}
}
