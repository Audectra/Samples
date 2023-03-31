using Microsoft.Extensions.Logging;
using Prism.Ioc;
using Prism.Modularity;
using SampleApp.Common.Constants;
using SampleApp.Common.Localization;
using SampleApp.Common.Utilities;
using SampleApp.Modules.SampleB.Resources;
using SampleApp.Modules.SampleB.Views;

namespace SampleApp.Modules.SampleB
{
    public class SampleBModule : IModule
    {
        private readonly ILogger _logger = LogManager.GetLogger<SampleBModule>();
        
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            containerProvider.AddLocalizationResource(SampleBLocales.ResourceManager, "SampleB");
            containerProvider.RegisterViewWithRegion<SampleBView>(AppRegions.SampleBRegion);
            
            _logger.LogInformation($"Module '{nameof(SampleBModule)}' has been initialized.");
        }
    }
}
