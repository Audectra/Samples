using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SampleApp.Controls;

[TemplatePart(Name = "PART_Border", Type = typeof(Border))]
[TemplatePart(Name = "PART_TextBlock", Type = typeof(TextBlock))]
[TemplateVisualState(GroupName = SelectionStatesGroup, Name = SelectedState)]
[TemplateVisualState(GroupName = SelectionStatesGroup, Name = NotSelectedState)]
public class SampleControl : Control
{
    public const string SelectionStatesGroup = "SelectionStates";
    public const string SelectedState = "Selected";
    public const string NotSelectedState = "NotSelected";

    private bool _isSelected;

    static SampleControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(SampleControl), new FrameworkPropertyMetadata(typeof(SampleControl)));
    }

    public override void OnApplyTemplate()
    {
        // Lets default to not selected state. 
        SetSelectionState(false);
        
        base.OnApplyTemplate();
    }

    protected override void OnMouseUp(MouseButtonEventArgs e)
    {
        SetSelectionState(!_isSelected);
        base.OnMouseUp(e);
    }

    private void SetSelectionState(bool selected)
    {
        _isSelected = selected;
        VisualStateManager.GoToState(this, selected ? SelectedState : NotSelectedState, true);
    }
}
