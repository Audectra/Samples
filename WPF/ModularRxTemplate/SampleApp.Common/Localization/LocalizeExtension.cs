using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace SampleApp.Common.Localization;

public class LocalizeExtension : MarkupExtension
{
    public string ResourceKey { get; set; }

    // This constructor can be used instead of a binding (within a multi binding, for example).
    public LocalizeExtension()
    {
        ResourceKey = string.Empty;
    }
    
    public LocalizeExtension(string resourceKey)
    {
        ResourceKey = resourceKey;
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        var localizationService = LocalizationService.Current;

        // targetObject is the control that is using the LocalizeExtension
        object? targetObject = (serviceProvider as IProvideValueTarget)?.TargetObject;

        // Is extension used in a control template? Required for template re-binding.
        if (targetObject?.GetType().Name == "SharedDp")
            return targetObject;

        // Is extension used within a binding (as converter parameter, for example)? Return localized string.
        if (targetObject is Binding)
            return localizationService[ResourceKey];

        var binding = new Binding
        {
            Mode = BindingMode.OneWay,
            Path = new PropertyPath($"[{ResourceKey}]"),
            Source = localizationService,
            FallbackValue = ResourceKey,
        };

        return binding.ProvideValue(serviceProvider);
    }
}
