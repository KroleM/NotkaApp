using NotkaDesktop.ViewModels.Abstract;
using System.Windows.Input;

namespace NotkaDesktop.ViewModels
{
	public class CommandViewModel : BaseViewModel
	{
		#region Properties
		public string DisplayName { get; set; }
		public ICommand Command { get; set; }
		#endregion
		#region Constructor
		public CommandViewModel(string displayName, ICommand command)
		{
			if (command == null)
				throw new ArgumentNullException("Command");
			this.DisplayName = displayName;
			this.Command = command;
		}
		#endregion
	}
}
