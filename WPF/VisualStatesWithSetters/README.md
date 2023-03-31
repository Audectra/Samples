# Visual States with Setters
Technique that allows you to use dynamic content or bindings in visual states of custom controls. 

Currently you can only work with story boards to create animations for visual states in WPF. Within these animations, you are required to use static values in XAML. However you can create on-the-fly animations in code-behind to solve this limitation. It can become frustrating very fast working with animations though, because there are not animations available for every scenario, unless you take the workaround route via animation with object key frames.

If you cannot relate with the above paragraph, don't worry, you don't have to. This technique and sample is here for you, to spare you from that mess. 

This sample will not explain how custom controls or visual states work in WPF, only the technique described above. 

## Contents
### Styled Visual States and Group
The key elements in this technique are the `StyledVisualState` and `StyledVisualStateGroup` classes.

The `StyledVisualState` class is quite straight forward, it inherits from `VisualState` and allows the consume to define a collection of `Setter` as content of the visual state. 

```csharp
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
```

The real magic happens in the `StyledVisualStateGroup` class, which inherits from `VisualStateGroup` and reacts to state changes by applying the setters of the corresponding `StyledVisualState` when it has been activated. It also allows to set a collection of visual states as content on the consuming side. 

```csharp
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

    // Omitted for brevity, see full version in sample.
}
```

### Consuming in Sample Control
This is how to consume the styled visual states within control (see `Themes/SampleControl.xaml`).

```xml
<VisualStateManager.VisualStateGroups>
    <!-- Handle visual states for selection. -->
    <controls:StyledVisualStateGroup Name="SelectionStates">
        <!-- Handle selected visual state. -->
        <controls:StyledVisualState Name="NotSelected">
            <Setter TargetName="PART_Border" Property="BorderBrush" Value="{DynamicResource BorderBrush}" />
            <Setter TargetName="PART_TextBlock" Property="TextBlock.Text" Value="Click me." />
        </controls:StyledVisualState>
        
        <!-- Handle not selected visual state. -->
        <controls:StyledVisualState Name="Selected">
            <Setter TargetName="PART_Border" Property="BorderBrush" Value="{DynamicResource HighlightBrush}" />
            <Setter TargetName="PART_TextBlock" Property="TextBlock.Text" Value="Yes! Click me again." />
        </controls:StyledVisualState>
    </controls:StyledVisualStateGroup>
</VisualStateManager.VisualStateGroups>

<!-- Rest is omitted for brevity, see full version in sample. -->
```

As you can see, this is a more straightforward and readable approach than the regular way using story boards. 
