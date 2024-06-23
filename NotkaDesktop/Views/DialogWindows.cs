using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;

namespace NotkaDesktop.Views
{
	public abstract class DialogBox : FrameworkElement, INotifyPropertyChanged
	{
		#region INotifyPropertyChanged
		public event PropertyChangedEventHandler? PropertyChanged;
		protected void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion
		//Akcja wywoływana przez komendę i odpowiedzialna za pokazywanie okien dialogowych i reagowanie na wybór użytkownika
		protected Action<object?>? execute = null;
		//Caption nadaje oknu dialogowemu nazwę
		public string Caption { get; set; } = string.Empty;
		protected ICommand? _show;
		public virtual ICommand Show
		{
			get
			{
				if (_show == null) _show = new RelayCommand<object>(execute);
				return _show;
			}
		}
	}

	public abstract class CommandDialogBox : DialogBox
	{
		public override ICommand Show
		{
			get
			{
				if (_show == null) _show = new RelayCommand<object>(
					o =>
					{
						ExecuteCommand(CommandBefore, CommandParameter);
						execute(o);
						ExecuteCommand(CommandAfter, CommandParameter);
					});
				return _show;
			}
		}
		public static DependencyProperty CommandParameterProperty =
			DependencyProperty.Register("CommandParameter", typeof(object),
			typeof(CommandDialogBox));
		public object CommandParameter
		{
			get
			{
				return GetValue(CommandParameterProperty);
			}
			set
			{
				SetValue(CommandParameterProperty, value);
			}
		}
		protected static void ExecuteCommand(ICommand command, object commandParameter)
		{
			if (command != null)
				if (command.CanExecute(commandParameter))
					command.Execute(commandParameter);
		}
		public static DependencyProperty CommandBeforeProperty =
			DependencyProperty.Register("CommandBefore", typeof(ICommand),
			typeof(CommandDialogBox));
		public ICommand CommandBefore
		{
			get
			{
				return (ICommand)GetValue(CommandBeforeProperty);
			}
			set
			{
				SetValue(CommandBeforeProperty, value);
			}
		}
		public static DependencyProperty CommandAfterProperty =
			DependencyProperty.Register("CommandAfter", typeof(ICommand),
			typeof(CommandDialogBox));
		public ICommand CommandAfter
		{
			get
			{
				return (ICommand)GetValue(CommandAfterProperty);
			}
			set
			{
				SetValue(CommandAfterProperty, value);
			}
		}
	}

	public abstract class FileDialogBox : CommandDialogBox
	{
		public bool? FileDialogResult { get; protected set; }
		public string FilePath { get; set; } = string.Empty;
		public string Filter { get; set; } = string.Empty;
		public int FilterIndex { get; set; }
		public string DefaultExt { get; set; } = string.Empty;
		protected Microsoft.Win32.FileDialog fileDialog = null!;
		protected FileDialogBox()
		{
			execute =
			o =>
			{
				fileDialog.Title = Caption;
				fileDialog.Filter = Filter;
				fileDialog.FilterIndex = FilterIndex;
				fileDialog.DefaultExt = DefaultExt;
				string filePath = "";
				if (FilePath != null) filePath = FilePath; else FilePath = "";
				if (o != null) filePath = (string)o;
				if (!string.IsNullOrWhiteSpace(filePath))
				{
					fileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(filePath);
					fileDialog.FileName = System.IO.Path.GetFileName(filePath);
				}
				FileDialogResult = fileDialog.ShowDialog();
				OnPropertyChanged(nameof(FileDialogResult));
				if (FileDialogResult.HasValue && FileDialogResult.Value)
				{
					FilePath = fileDialog.FileName;
					OnPropertyChanged(nameof(FilePath));
					ExecuteCommand(CommandFileOk, FilePath);
				};
			};
		}
		public static DependencyProperty CommandFileOkProperty =
			DependencyProperty.Register("CommandFileOk", typeof(ICommand), typeof(FileDialogBox));
		public ICommand CommandFileOk
		{
			get
			{
				return (ICommand)GetValue(CommandFileOkProperty);
			}
			set
			{
				SetValue(CommandFileOkProperty, value);
			}
		}
	}
	public class OpenFileDialogBox : FileDialogBox
	{
		public OpenFileDialogBox()
		{
			fileDialog = new Microsoft.Win32.OpenFileDialog();
		}
	}
	public class SaveFileDialogBox : FileDialogBox
	{
		public SaveFileDialogBox()
		{
			fileDialog = new Microsoft.Win32.SaveFileDialog();
		}
	}
}
