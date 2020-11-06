
namespace Modules.ControlFapch
{
    using Catel.Modules;
    using Common.Constants;
    using Views;

    public class ControlFapchModule : ModuleBase
    {
        public ControlFapchModule() : base(WellKnownModuleNames.ControlFapchModule)
        {
        }

        #region Overrides of ModuleBase<IServiceLocator>

        protected override void OnInitialized()
        {
            RegionManager.RegisterViewWithRegion(WellKnownRegionNames.ControlFapchRegion, typeof (ControlFapchView));
        }

        #endregion
    }
}