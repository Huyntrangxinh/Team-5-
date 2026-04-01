using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace Campus.Converters;


public class StatusToColorConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value == null)
			return Colors.Gray;

		var status = value.ToString();

		return status switch
		{
			"Upcoming" => Colors.Green,
			"Cancelled" => Colors.Red,
			_ => Colors.Gray
		};
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}