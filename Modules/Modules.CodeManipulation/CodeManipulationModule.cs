
namespace Modules.ManipulationsCode
{
    using Catel.Modules;
    using Common.Constants;
    using Views;


    public class CodeManipulationModule: ModuleBase
    {
        public CodeManipulationModule(): base(WellKnownModuleNames.ManipulationsCodeModule)
        {
        }

        #region Overrides of ModuleBase<IServiceLocator>

        protected override void OnInitialized()
        {
            RegionManager.RegisterViewWithRegion(WellKnownRegionNames.CodeManipulationRegion,
                typeof (CodeManipulationView));
        }

        #endregion
    }
}