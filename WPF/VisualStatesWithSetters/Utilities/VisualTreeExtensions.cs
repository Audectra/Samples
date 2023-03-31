using System.Windows;
using System.Windows.Media;

namespace SampleApp.Utilities;

public static class VisualTreeExtensions
{
    public static T? FindChild<T>(this DependencyObject parentObject, string childName)
        where T : FrameworkElement
    {
        int numChildren = VisualTreeHelper.GetChildrenCount(parentObject);

        for (int i = 0; i < numChildren; i++)
        {
            var child = VisualTreeHelper.GetChild(parentObject, i);

            // Check current child.
            if (child is T childElement && childElement.Name == childName)
                return childElement;

            // Check children of child.
            var foundChild = FindChild<T>(child, childName);
            if (foundChild is not null)
                return foundChild;
        }

        return null;
    }
}
