using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.ComponentModel;
using NotkaDesktop.Service.Reference;
using NotkaDesktop.Services;
using NotkaDesktop.ViewModels.Abstract;

namespace NotkaDesktop.ViewModels
{
	public partial class NewRoleViewModel : ANewViewModel<RoleForView, RoleParameters>
	{
		public NewRoleViewModel(RoleDataStore dataStore) 
			: base("Nowa rola", dataStore)
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

		public override RoleForView SetItem()
		{
			return new RoleForView
			{
				Id = 0,
				IsActive = this.IsActive,
				Name = this.Name,
				Description = this.Description,
				CreatedDate = DateTimeOffset.Now,
				ModifiedDate = DateTimeOffset.Now,
			};
		}

		public override bool ValidateSave()
		{
			return !string.IsNullOrEmpty(Name);
		}
	}
}
