using System.Globalization;

namespace NotkaMobile.Helpers
{
	public class BoolToHasPicture : IValueConverter
	{
		public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			switch (value as bool?)
			{
				case true:
					return "Tak";
				case false:
					return "Nie";
			}
			return "Dowolnie";
		}

		public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			switch (value as string)
			{
				case "Tak":
					return true;
				case "Nie":
					return false;
			}
			return null;
		}
	}

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
