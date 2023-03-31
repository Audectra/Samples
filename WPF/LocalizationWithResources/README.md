# Localization with Resources
Technique that allows you to dynamically localize your application with content from resources. 

It allows to add multiple resource managers, for example from different application modules, to the localization service, where each resource manager has a unique base name identifier. 

Localization resources are referenced via a combined resource key, which consists of two parts, the resource manager base name, and the sub resource key within the corresponding resource. 

    CombinedResourceKey = BaseName.SubResourceKey

Within this sample, the separator between the base key and the sub key is a decimal point. You can adjust that with the `SplitCharacter` constant within the `LocalizationService` class. 

## Contents

### Localization Service
The main task of the `LocalizationService` class is to resolve the combined resource keys to a previously added resource manager and then query the resource with the sub resource key and currently configured culture. 

You can add resource manager to the localization service like this.

```csharp
localizationService.AddResourceManager(SampleAppLocales.ResourceManager, "SampleApp");
```

Additionally, the localization service implements the `INotifyPropertyChanged` interface, in order to propagate any changes to localization bindings (see below) when a new resource manager has been added, or the current culture has been changed. 

There is also a fallback mechanism, which returns the combined resource key, in case either the resource manager with the base name has not been found, or it does not contain any resource with the provided sub resource key. 

### Localize Markup Extension
The markup extension `LocalizeExtension` makes the localization service accessible within XAML and provides dynamic bindings where applicable. These bindings resolve the provided combined resource key via the localization service and propagate any updates from there as well. 

The consumer side within XAML looks like this.

```xml
<!-- The Localize extension will create a binding for localized string, which updates every time the current culture of the localization service is updated. -->
<TextBlock Grid.Row="1" Text="{loc:Localize SampleApp.HelloWorld}" />
```

### Main Window View
This sample shows a localized text and a combobox, which allows you to switch the language and see how the text gets updated accordingly. 
