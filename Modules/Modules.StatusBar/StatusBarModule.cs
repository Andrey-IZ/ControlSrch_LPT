
namespace Modules.StatusBar
{
    using Catel.Modules;
    using Common.Constants;
    using Views;

    public class StatusBarModule:ModuleBase
    {
        public StatusBarModule() : base(WellKnownModuleNames.StatusBarModule)
        {
        }

        #region Overrides of ModuleBase<IServiceLocator>

        protected override void OnInitialized()
        {
            RegionManager.RegisterViewWithRegion(WellKnownRegionNames.StatusBarRegion, 
                typeof (StatusBarView));
        }

        #endregion
    }
}