
namespace Modules.Synthesizers
{
    using Catel.Modules;
    using Common.Constants;
    using Views;

    public class SynthesizersModule : ModuleBase
    {
        public SynthesizersModule() : base(WellKnownModuleNames.SynthesizersModule)
        {
        }

        #region Overrides of ModuleBase<IServiceLocator>

        protected override void OnInitialized()
        {
            RegionManager.RegisterViewWithRegion(WellKnownRegionNames.SynthesizersRegion,
                typeof (SynthesizersView));
        }

        #endregion
    }
}