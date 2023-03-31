using System;
using System.Globalization;
using System.Windows.Data;

namespace SampleApp.Converters;

public class ToUpperStringRegularConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string stringValue)
            return value;

        return stringValue.ToUpper();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;
}
