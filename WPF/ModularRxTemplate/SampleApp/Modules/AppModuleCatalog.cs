using Prism.Modularity;
using SampleApp.Modules.Core;
using SampleApp.Modules.SampleA;
using SampleApp.Modules.SampleB;

namespace SampleApp.Modules;

public class AppModuleCatalog : ModuleCatalogBase
{
    public AppModuleCatalog()
    {
        this.AddModule<CoreModule>(nameof(CoreModule), InitializationMode.WhenAvailable);
        this.AddModule<SampleAModule>(nameof(SampleAModule), InitializationMode.WhenAvailable, nameof(CoreModule));
        this.AddModule<SampleBModule>(nameof(SampleBModule), InitializationMode.WhenAvailable, nameof(CoreModule));
    }
}
