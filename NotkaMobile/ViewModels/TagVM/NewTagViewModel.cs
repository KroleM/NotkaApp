using CommunityToolkit.Mvvm.ComponentModel;
using NotkaMobile.Service.Reference;
using NotkaMobile.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotkaMobile.ViewModels.TagVM
{
	public partial class NewTagViewModel : ANewViewModel<Tag>
	{
		public NewTagViewModel() 
			: base("Nowy tag")
		{
		}

		[ObservableProperty]
		string _tagName = string.Empty;
		[ObservableProperty]
		string _description = string.Empty;
		public override Tag SetItem()
		{
			return new Tag
			{
				Id = 0,
				IsActive = true,
				Name = this.TagName,
				Description = this.Description,
				CreatedDate = DateTime.Now,
				ModifiedDate = DateTime.Now,
				UserId = Preferences.Default.Get("userId", 0),
			};
		}

		public override bool ValidateSave()
		{
			return !string.IsNullOrEmpty(TagName);
		}
	}
}
