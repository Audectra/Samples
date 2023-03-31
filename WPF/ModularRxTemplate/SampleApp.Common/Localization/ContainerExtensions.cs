using System.Resources;
using Prism.Ioc;
using Prism.Regions;

namespace SampleApp.Common.Localization;

public static class ContainerExtensions
{
    public static IContainerProvider AddLocalizationResource(this IContainerProvider container, ResourceManager resourceManager, string resourceBaseName)
    {
        var localizationService = container.Resolve<ILocalizationService>();

        localizationService.AddResourceManager(resourceManager, resourceBaseName);

        return container;
    }

    public static IContainerProvider RegisterViewWithRegion<T>(this IContainerProvider container, string regionName)
    {
        var regionManager = container.Resolve<IRegionManager>();
        regionManager.RegisterViewWithRegion<T>(regionName);

        return container;
    }
}
