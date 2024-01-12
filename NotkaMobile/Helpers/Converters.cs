using System.Globalization;

namespace NotkaMobile.Helpers
{
	//FIXME no use
	public class ObjectToVisibilityConverter : IValueConverter
	{
		public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			if (value != null)
			{
				return Visibility.Visible;
			}
			else { return Visibility.Collapsed; }
		}

		public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
	
}
