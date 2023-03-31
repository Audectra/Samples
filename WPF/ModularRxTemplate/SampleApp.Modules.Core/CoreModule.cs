using System;
using Microsoft.Extensions.Logging;
using Prism.Ioc;
using Prism.Modularity;
using SampleApp.Common.Localization;
using SampleApp.Common.Utilities;
using SampleApp.Modules.Core.Resources;

namespace SampleApp.Modules.Core
{
    public class CoreModule : IModule
    {
        private readonly ILogger _logger = LogManager.GetLogger<CoreModule>();
        
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance(LocalizationService.Current);
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            containerProvider.AddLocalizationResource(CoreModuleLocales.ResourceManager, "Core");
            
            _logger.LogInformation($"Module '{nameof(CoreModule)}' has been initialized.");
        }
    }
}
