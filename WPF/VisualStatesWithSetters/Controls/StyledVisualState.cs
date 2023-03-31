using System.Windows;
using System.Windows.Markup;

namespace SampleApp.Controls;

[RuntimeNameProperty("Name")]
[ContentProperty("Setters")]
public class StyledVisualState : VisualState
{
    #region Dependency Properties

    public static readonly DependencyProperty SettersProperty = DependencyProperty.Register(nameof(Setters),
        typeof(SetterBaseCollection),
        typeof(StyledVisualState));

    public SetterBaseCollection Setters
    {
        get => (SetterBaseCollection)GetValue(SettersProperty);
        set => SetValue(SettersProperty, value);
    }

    #endregion

    public StyledVisualState()
    {
        Setters = new SetterBaseCollection();
    }
}
