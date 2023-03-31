using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Resources;
using Prism.Mvvm;

namespace SampleApp.Common.Localization;

public class LocalizationService : BindableBase, ILocalizationService
{
    private const char SplitCharacter = '.';
    private static readonly Lazy<ILocalizationService> LazyLocalizationService = new(() => new LocalizationService());

    private readonly object _lockResources = new();
    private readonly IDictionary<string, ResourceManager> _resources;

    private CultureInfo _currentCulture;
    public static ILocalizationService Current => LazyLocalizationService.Value;

    public string this[string resourceKey] => Localize(resourceKey);

    public CultureInfo CurrentCulture
    {
        get => _currentCulture;
        set => SetProperty(ref _currentCulture, value, string.Empty);
    }

    internal LocalizationService()
    {
        _resources = new Dictionary<string, ResourceManager>();
        _currentCulture = CultureInfo.CurrentUICulture;
    }

    // Localization without any formatting.
    public string Localize(string resourceKey)
    {
        var splitKey = SplitResourceKey(resourceKey);
        if (splitKey is null)
            return resourceKey;

        return DoLocalize(splitKey.Value.BaseName, splitKey.Value.SubKey) ?? resourceKey;
    }

    // Localization with string formatting.
    public string Localize(string resourceKey, params object[] objects)
    {
        string formatString = Localize(resourceKey);
        return string.Format(formatString, objects);
    }

    public void AddResourceManager(ResourceManager resourceManager, string baseName)
    {
        lock (_lockResources)
            _resources.Add(baseName, resourceManager);

        RaisePropertyChanged(string.Empty);
    }

    private string? DoLocalize(string baseName, string resourceSubKey)
    {
        if (TryGetResourceManagerById(baseName, out var resourceManager))
            return resourceManager.GetString(resourceSubKey, CurrentCulture);

        return null;
    }

    private static (string BaseName, string SubKey)? SplitResourceKey(string resourceKey)
    {
        if (!resourceKey.Contains(SplitCharacter))
            return null;

        string[] splitKey = resourceKey.Split(SplitCharacter);
        return (splitKey[0], splitKey[1]);
    }

    private bool TryGetResourceManagerById(string baseName, [NotNullWhen(true)] out ResourceManager? resourceManager)
    {
        lock (_lockResources)
            return _resources.TryGetValue(baseName, out resourceManager);
    }
}
