using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace SampleApp.Converters;

public abstract class ValueConverterBase : MarkupExtension, IValueConverter
{
    public override object ProvideValue(IServiceProvider serviceProvider) => this;
    
    // Forward conversion is required. 
    public abstract object? Convert(object value, Type targetType, object parameter, CultureInfo culture);

    // Back conversion is optional. 
    public virtual object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;
}
