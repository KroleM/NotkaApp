using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace NotkaDesktop.Helpers
{
	public class StringToVisibilityConverter : IValueConverter
	{
		public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			if (value is not string) return Visibility.Collapsed;
			if ((string)value != string.Empty)
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
