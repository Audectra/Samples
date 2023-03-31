﻿using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace SampleApp.Common.Converters;

public abstract class MultiValueConverterBase : MarkupExtension, IMultiValueConverter
{
    public override object ProvideValue(IServiceProvider serviceProvider) => this;

    public abstract object Convert(object[] values, Type targetType, object parameter, CultureInfo culture);
    public virtual object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotSupportedException();
}
