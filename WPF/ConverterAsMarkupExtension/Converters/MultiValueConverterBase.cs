using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace SampleApp.Converters;

public abstract class MultiValueConverterBase : MarkupExtension, IMultiValueConverter
{
    public override object ProvideValue(IServiceProvider serviceProvider) => this;
    
    // Forward conversion is required. 
    public abstract object? Convert(object[] values, Type targetType, object parameter, CultureInfo culture);

    // Back conversion is optional (however normally not supported). 
    public virtual object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}
