using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace SampleApp.Common.Converters;

public abstract class ValueConverterBase : MarkupExtension, IValueConverter
{
    public override object ProvideValue(IServiceProvider serviceProvider) => this;

    public abstract object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture);

    public virtual object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => Binding.DoNothing;
}
