
namespace Modules.Forms.ControlSrch
{
    using ViewModels;
    using Views;
    using Catel.IoC;
    using Common.Constants;
    using Synthesizers;
    using Common.Modularity;


    public class FormControlSrchModule : TabModuleBase
    {
        public FormControlSrchModule() : base(WellKnownModuleNames.FormControlSrchModule)
        {
        }

        #region Overrides of ModuleBase<IServiceLocator>

        protected override void OnInitialized()
        {
            InitTabRegion(WellKnownRegionNames.TabRegion, typeof (FormControlSrchView), typeof (FormControlSrchViewModel));
        }

        #endregion
    }
}