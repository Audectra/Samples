using System.ComponentModel;
using System.Globalization;
using System.Resources;

namespace SampleApp.Localization;

public interface ILocalizationService : INotifyPropertyChanged
{
    string this[string resourceKey] { get; }

    CultureInfo CurrentCulture { get; set; }

    string Localize(string resourceKey);
    string Localize(string resourceKey, params object[] objects);
    
    void AddResourceManager(ResourceManager resourceManager, string baseName);
}
