using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using SampleApp.Utilities;

namespace SampleApp.Controls;

[RuntimeNameProperty("Name")]
[ContentProperty("States")]
public class StyledVisualStateGroup : VisualStateGroup
{
    private static readonly ServiceProviders EmptyServiceProvider = new();
    
    public StyledVisualStateGroup()
    {
        CurrentStateChanged += OnCurrentStateChanged;
    }

    private static void OnCurrentStateChanged(object? sender, VisualStateChangedEventArgs e)
    {
        var oldState = e.OldState as StyledVisualState;
        var newState = e.NewState as StyledVisualState;

        if (newState?.Setters is null)
            return;

        foreach (var member in newState.Setters)
        {
            if (member is not Setter setter)
                continue; // EventSetters are not supported.

            // Find the framework element with the target name.
            var targetElement = e.Control.FindChild<FrameworkElement>(setter.TargetName);
            if (targetElement is null)
                return;

            ApplySetterToTarget(setter, targetElement);
        }
    }

    private static void ApplySetterToTarget(Setter setter, FrameworkElement targetElement)
    {
        // Depending on the value type, we need to apply it in a specific way to the target elements property.
        switch (setter.Value)
        {
            // If it is a binding, we forward apply the binding to the target property.
            case BindingBase binding:
                targetElement.SetBinding(setter.Property, binding);
                break;

            // If it is a dynamic or static resource, forward the resource key to the target property.
            case DynamicResourceExtension dynamicResource:
                targetElement.SetResourceReference(setter.Property, dynamicResource.ResourceKey);
                break;

            case StaticResourceExtension staticResource:
                targetElement.SetResourceReference(setter.Property, staticResource.ResourceKey);
                break;

            // For any other markup extension, we can simply call the ProvideValue method.
            case MarkupExtension markupExtension:
                targetElement.SetValue(setter.Property, markupExtension.ProvideValue(EmptyServiceProvider));
                break;

            // If none of the above, it is probably static value and can be directly applied to the target property.
            default:
                targetElement.SetValue(setter.Property, setter.Value);
                break;
        }
    }
}
