using CommunityToolkit.Mvvm.ComponentModel;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.Services.Abstract;
using NotkaMobile.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotkaMobile.ViewModels.TagVM
{
	public partial class TagEditViewModel : AEditViewModel<TagForView>
	{
		#region Constructor
		public TagEditViewModel(TagDataStore dataStore)
			: base("Edycja taga", dataStore)
		{
		}
		#endregion
		#region Fields & Properties
		[ObservableProperty]
		string _tagName = string.Empty;
		[ObservableProperty]
		string _description = string.Empty;
		#endregion
		public override void LoadProperties()
		{
			TagName = Item.Name;
			Description = Item.Description;
		}

		public override TagForView SetItem()
		{
			Item.IsActive = true;
			Item.Name = this.TagName;
			Item.Description = this.Description;
			Item.ModifiedDate = DateTime.Now;

			return Item;
		}

		public override bool ValidateSave()
		{
			return !string.IsNullOrEmpty(TagName) && TagName.Length < 60;
		}
	}
}
