using System;
using System.Globalization;
using System.Windows.Data;

namespace SampleApp.Common.Converters;

public class ToUpperStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string stringValue)
            return value;

        return stringValue.ToUpper();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;
}
