using System;
using System.Globalization;

namespace SampleApp.Converters;

public class ToUpperStringMarkupConverter : ValueConverterBase
{
    public override object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string stringValue)
            return value;

        return stringValue.ToUpper();
    }
}
