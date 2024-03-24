using ApiSharedClasses.QueryParameters;
using CommunityToolkit.Mvvm.ComponentModel;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;

namespace NotkaMobile.ViewModels.TagVM
{
	public partial class TagEditViewModel : AEditViewModel<TagForView, TagParameters>
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
			Item.ModifiedDate = DateTimeOffset.Now;

			return Item;
		}

		public override bool ValidateSave()
		{
			return !string.IsNullOrEmpty(TagName) && TagName.Length < 60;
		}
	}
}
