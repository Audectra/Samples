using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace SampleApp.Localization;

public class LocalizationService : ILocalizationService
{
    private const char SplitCharacter = '.';
    private static readonly Lazy<LocalizationService> LazyLocalizationService = new(() => new LocalizationService());
    private readonly object _lockResources = new();
    private readonly IDictionary<string, ResourceManager> _resources;
    private CultureInfo _currentCulture;
    
    public static ILocalizationService Current => LazyLocalizationService.Value;
    
    public event PropertyChangedEventHandler? PropertyChanged;
    
    public string this[string resourceKey] => Localize(resourceKey);

    public CultureInfo CurrentCulture
    {
        get => _currentCulture;
        set
        {
            if (_currentCulture.Name != value.Name)
            {
                _currentCulture = value;
                
                // Propagate this change to all localization bindings. 
                OnPropertyChanged(string.Empty);
            }
        }
    }

    internal LocalizationService()
    {
        _resources = new Dictionary<string, ResourceManager>();
        _currentCulture = CultureInfo.CurrentUICulture;
    }

    public string Localize(string resourceKey)
    {
        var splitKey = SplitResourceKey(resourceKey);
        if (splitKey is null)
            return resourceKey;

        return DoLocalize(splitKey.Value.BaseName, splitKey.Value.SubKey) ?? resourceKey;
    }

    public string Localize(string resourceKey, params object[] objects)
    {
        string formatString = Localize(resourceKey);
        return string.Format(formatString, objects);
    }

    public void AddResourceManager(ResourceManager resourceManager, string baseName)
    {
        lock (_lockResources)
            _resources.Add(baseName, resourceManager);

        // Propagate this change to all localization bindings. 
        OnPropertyChanged(string.Empty);
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

     private void OnPropertyChanged([CallerMemberName] string? propertyName = null) => 
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
