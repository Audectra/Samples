using System.Globalization;
using System.Windows;
using SampleApp.Localization;
using SampleApp.Resources;

namespace SampleApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // We can set the default UI culture of the application.
            CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en");
            
            // Add the localization resources to the localization service.
            var localizationService = LocalizationService.Current;
            localizationService.AddResourceManager(SampleAppLocales.ResourceManager, "SampleApp");
            
            base.OnStartup(e);
        }
    }
}