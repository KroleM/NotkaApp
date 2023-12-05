using CommunityToolkit.Mvvm.ComponentModel;
using NotkaMobile.Service.Reference;
using NotkaMobile.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NotkaMobile.ViewModels.NoteVM
{
	public partial class NewNoteViewModel : ANewViewModel<Note>
	{
		public NewNoteViewModel()
			: base()
		{
		}
		#region Fields & Properties
		[ObservableProperty]
		string _title = string.Empty;
		[ObservableProperty]
		string _text = string.Empty;
		//FIXME tag?
		#endregion
		public override Note SetItem()
		{
			return new Note
			{
				Id = 0,
				IsActive = true,
				Name = this.Title,
				Description = this.Text,
			};
		}

		public override bool ValidateSave()
		{
			return !string.IsNullOrEmpty(Title);
		}
	}
}
