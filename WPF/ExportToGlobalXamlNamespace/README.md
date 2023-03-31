# Export To Global XAML Namespace
Technique that allows you to export custom controls, converters, behaviors and more to a global or custom XAML namespace.

This technique is especially useful, if you have a collection of controls, converters or behaviors that you use across your whole application, because it makes them more accessible on the consuming end. Remember, that you can export to any custom namespace as well, if you prefer. 

## Contents
### SampleApp.Common
Contains a user control and converter that are exported to the global namespace with these assembly attributes within the `Properties/AssemblyInfo.cs` file.

```csharp
// Export all relevant namespaces within this assembly to a global (or custom) namespace. 
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "SampleApp.Common.Controls", AssemblyName = "SampleApp.Common")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "SampleApp.Common.Converters", AssemblyName = "SampleApp.Common")]
```

In this sample, the namespaces `SampleApp.Common.Controls` and `SampleApp.Common.Converters` are both exported to the global WPF specific XAML namespace `http://schemas.microsoft.com/winfx/2006/xaml/presentation`. Since this specific namespace is included within every WPF XAML file, our converters and controls become globally available. 

### SampleApp
References SampleApp.Common and uses the globally exposed control and converter as example. 
