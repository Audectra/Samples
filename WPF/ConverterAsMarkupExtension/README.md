# Converter as Markup Extension
Technique that tallows you to directly use converters without having to add them to the resources first. 

While this is an incredibly convenient way of working with converters, I will have to point out, that this approach creates more instances of converters than the regular approach. In most scenarios this doesn't make much of a difference, especially when your converters are small (as they should be) anyway. However, keep that in mind, when dealing with "big" converters that take up more resources (for whatever legacy reason), or when your view needs a ton of converters. 

## Contents
### ConverterBase Classes
The abstract class `ValueConverterBase` is a base class for all converters that are going to be used like a markup extension. As such, it inherits from `MarkupExtension` and `IValueConverter`. 

```csharp
public abstract class ValueConverterBase : MarkupExtension, IValueConverter
{
    public override object ProvideValue(IServiceProvider serviceProvider) => this;
    
    // Forward conversion is required. 
    public abstract object? Convert(object value, Type targetType, object parameter, CultureInfo culture);

    // Back conversion is optional. 
    public virtual object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;
}
```

This sample also includes a base class for multi-value converters.

```csharp
public abstract class MultiValueConverterBase : MarkupExtension, IMultiValueConverter
{
    public override object ProvideValue(IServiceProvider serviceProvider) => this;
    
    // Forward conversion is required. 
    public abstract object? Convert(object[] values, Type targetType, object parameter, CultureInfo culture);

    // Back conversion is optional (however normally not supported). 
    public virtual object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}
```

### Sample Converters
There are two converters defined within this sample, whereas their converting functionality is the same. Both convert an incoming string to upper case. However, one of them is a regular converter

```csharp
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
```

and the other a markup based converter

```csharp
public class ToUpperStringMarkupConverter : ValueConverterBase
{
    public override object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string stringValue)
            return value;

        return stringValue.ToUpper();
    }
}
```

### Main Window View
The sample view utilizes both converters to convert a simple string within a TextBlock. 

#### Using the regular converter
The regular converter first needs to be included within the resources. 

```xml
<Grid.Resources>
    <!-- The regular converter needs to be explicitly added to the resources, before it can be used. -->
    <converters:ToUpperStringRegularConverter x:Key="RegularConverter" />
</Grid.Resources>
```

which allows us to reference it as a static resource.

```xml
<!-- For the regular converter, we need to reference it as a resource. -->
<TextBlock Grid.Row="0" Text="{Binding Source=I have been converted the normal way., Converter={StaticResource RegularConverter}}" />
```

#### Using the markup based converter
The usage of the markup based converter is as simple as follows.

```xml
<!-- The markup converter can be used directly without any resource reference. -->
<TextBlock Grid.Row="1" Text="{Binding Source=I have been converted the markup way., Converter={converters:ToUpperStringMarkupConverter}}" />
```
