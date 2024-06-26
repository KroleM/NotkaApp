using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.ComponentModel;
using NotkaDesktop.Service.Reference;
using NotkaDesktop.Services;
using NotkaDesktop.ViewModels.Abstract;

namespace NotkaDesktop.ViewModels
{
	public partial class CurrencyEditViewModel : AEditViewModel<CurrencyForView, CurrencyParameters>
	{
		public CurrencyEditViewModel(CurrencyDataStore dataStore, int itemId) 
			: base("Edycja waluty", dataStore, itemId)
		{
		}
		#region Fields & Properties
		[ObservableProperty]
		string _name = string.Empty;

		[ObservableProperty]
		string _description = string.Empty;

		[ObservableProperty]
		bool _isActive = true;

		[ObservableProperty]
		string _shortName = string.Empty;
		#endregion
		#region Methods
		public override void LoadProperties()
		{
			Name = Item.Name;
			Description = Item.Description;
			IsActive = Item.IsActive;
			ShortName = Item.ShortName;
		}

		public override CurrencyForView SetItem()
		{
			Item.Name = this.Name;
			Item.Description = this.Description;
			Item.IsActive = this.IsActive;
			Item.ModifiedDate = DateTimeOffset.Now;
			Item.ShortName = this.ShortName;

			return Item;
		}

		public override bool ValidateSave()
		{
			return !string.IsNullOrWhiteSpace(Name)
				&& !string.IsNullOrWhiteSpace(ShortName)
				&& ShortName.Length < 6;
		}
		#endregion
	}
}
