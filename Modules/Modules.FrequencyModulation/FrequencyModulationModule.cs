

namespace Modules.FrequencyModulation
{
    using Catel.Modules;
    using Common.Constants;
    using Views;

    public class FrequencyModulationModule: ModuleBase
    {
        public FrequencyModulationModule() : base(WellKnownModuleNames.FrequencyModulationModule)
        {
        }

        #region Overrides of ModuleBase<IServiceLocator>

        protected override void OnInitialized()
        {
            RegionManager.RegisterViewWithRegion(WellKnownRegionNames.FrequencyModulationRegion,
                typeof(FrequencyModulationView));
        }

        #endregion
    }
}
