using Microsoft.Extensions.Logging;
using Prism.Ioc;
using Prism.Modularity;
using SampleApp.Common.Constants;
using SampleApp.Common.Localization;
using SampleApp.Common.Utilities;
using SampleApp.Modules.SampleA.Resources;
using SampleApp.Modules.SampleA.Views;

namespace SampleApp.Modules.SampleA
{
    public class SampleAModule : IModule
    {
        private readonly ILogger _logger = LogManager.GetLogger<SampleAModule>();
        
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            containerProvider.AddLocalizationResource(SampleALocales.ResourceManager, "SampleA");
            containerProvider.RegisterViewWithRegion<SampleAView>(AppRegions.SampleARegion);
            
            _logger.LogInformation($"Module '{nameof(SampleAModule)}' has been initialized.");
        }
    }
}
