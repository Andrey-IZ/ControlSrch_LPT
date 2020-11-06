
namespace Modules.RangeReproductionF
{
    using Catel.Modules;
    using Common.Constants;
    using Views;

    public class RangeReproductionFModule: ModuleBase
    {
        public RangeReproductionFModule() : base(WellKnownModuleNames.RangeReproductionFModule)
        {
        }

        #region Overrides of ModuleBase<IServiceLocator>

        protected override void OnInitialized()
        {
            RegionManager.RegisterViewWithRegion(WellKnownRegionNames.RangeReproductionFRegion, typeof (RangeReproductionFView));
        }

        #endregion
    }
}