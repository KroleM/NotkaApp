using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NotkaMobile.Service.Reference;
using NotkaMobile.ViewModels.Abstract;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Task = System.Threading.Tasks.Task;

namespace NotkaMobile.ViewModels.NoteVM
{
	public partial class NewNoteViewModel : ANewViewModel<Note>
	{
		public NewNoteViewModel() 
			: base("Nowa notatka")
		{
		}
		#region Fields & Properties
		public ObservableCollection<Tag> Tags { get; } = new();

		[ObservableProperty]
		string _noteTitle = string.Empty;

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
				Name = this.NoteTitle,
				Description = this.Text,
			};
		}
		public override bool ValidateSave()
		{
			return !string.IsNullOrEmpty(Title);
		}
		[RelayCommand]
		private async Task Appearing()	//LoadTags
		{
			IsBusy = true;
			try
			{
				Tags.Clear();
				var items = await DataStore.GetItemsAsync(true);
				foreach (var item in items)
				{
					Tags.Add(item);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}
