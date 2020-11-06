
namespace Modules.CaptureFapch
{
    using Catel.Modules;
    using Common.Constants;
    using Views;

    public class CaptureFapchModule : ModuleBase
    {
        public CaptureFapchModule() : base(WellKnownModuleNames.CaptureFapchModule)
        {
        }

        #region Overrides of ModuleBase<IServiceLocator>

        protected override void OnInitialized()
        {
            RegionManager.RegisterViewWithRegion(WellKnownRegionNames.CaptureFapchRegion, 
                typeof (CaptureFapchView));
        }

        #endregion
    }
}