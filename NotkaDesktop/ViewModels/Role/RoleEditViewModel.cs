using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.ComponentModel;
using NotkaDesktop.Service.Reference;
using NotkaDesktop.Services;
using NotkaDesktop.ViewModels.Abstract;

namespace NotkaDesktop.ViewModels
{
	public partial class RoleEditViewModel : AEditViewModel<RoleForView, RoleParameters>
	{
		public RoleEditViewModel(RoleDataStore dataStore, int itemId) 
			: base("Edycja roli", dataStore, itemId)
		{
		}
		#region Fields & Properties
		[ObservableProperty]
		string _name = string.Empty;

		[ObservableProperty]
		string _description = string.Empty;

		[ObservableProperty]
		bool _isActive = true;
		#endregion
		#region Methods
		public override void LoadProperties()
		{
			Name = Item.Name;
			Description = Item.Description;
			IsActive = Item.IsActive;
		}

		public override RoleForView SetItem()
		{			
			Item.Name = this.Name;
			Item.Description = this.Description;
			Item.IsActive = this.IsActive;
			Item.ModifiedDate = DateTimeOffset.Now;

			return Item;
		}

		public override bool ValidateSave()
		{
			return !string.IsNullOrEmpty(Name);
		}
		#endregion
	}
}
