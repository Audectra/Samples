using System.Windows;
using System.Windows.Media;

namespace SampleApp.Common.Utilities;

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

    public static T? FindParent<T>(this DependencyObject childObject)
        where T : DependencyObject
    {
        var currentObject = childObject;

        // Iterate the visual tree through the parent relationships, until we find the object we are looking for.
        do
        {
            currentObject = VisualTreeHelper.GetParent(currentObject);

            // If there are no parents left, then return null.
            if (currentObject is null)
                return null;

        } while (currentObject is not T);

        return (T)currentObject;
    }
}
