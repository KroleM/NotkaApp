using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.ComponentModel;
using NotkaDesktop.Service.Reference;
using NotkaDesktop.Services;
using NotkaDesktop.ViewModels.Abstract;

namespace NotkaDesktop.ViewModels
{
	public partial class NewCurrencyViewModel : ANewViewModel<CurrencyForView, CurrencyParameters>
	{
		public NewCurrencyViewModel(CurrencyDataStore dataStore) 
			: base("Nowa waluta", dataStore)
		{
		}

		[ObservableProperty]
		string _name = string.Empty;

		[ObservableProperty]
		string _description = string.Empty;

		[ObservableProperty]
		bool _isActive = true;

		[ObservableProperty]
		string _shortName = string.Empty;

		public override CurrencyForView SetItem()
		{
			return new CurrencyForView
			{
				Id = 0,
				IsActive = this.IsActive,
				Name = this.Name,
				Description = this.Description,
				CreatedDate = DateTimeOffset.Now,
				ModifiedDate = DateTimeOffset.Now,
				ShortName = this.ShortName,
			};
		}

		public override bool ValidateSave()
		{
			return !string.IsNullOrWhiteSpace(Name)
				&& !string.IsNullOrWhiteSpace(ShortName)
				&& ShortName.Length < 6;
		}
	}
}
