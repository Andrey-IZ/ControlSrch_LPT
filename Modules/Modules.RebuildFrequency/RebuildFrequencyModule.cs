
namespace Modules.RebuildFrequency
{
    using Catel.Modules;
    using Common.Constants;
    using Views;

    public class RebuildFrequencyModule : ModuleBase
    {
        public RebuildFrequencyModule() : base(WellKnownModuleNames.RebuildFrequencyModule)
        {
        }

        #region Overrides of ModuleBase<IServiceLocator>

        protected override void OnInitialized()
        {
            RegionManager.RegisterViewWithRegion(WellKnownRegionNames.RebuildFrequencyRegion,
                typeof (RebuildFrequencyView));
        }

        #endregion
    }
}