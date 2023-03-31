using System;
using System.Globalization;

namespace SampleApp.Common.Converters;

public class StringFormatMultiConverter : MultiValueConverterBase
{
    public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        // We assume that the first string is the format string and the other objects are arguments. 
        if (values.Length == 0 || values[0] is not string formatString)
            return values;

        return string.Format(formatString, values[1..]);
    }
}
