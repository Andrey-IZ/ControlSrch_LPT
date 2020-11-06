
namespace Modules.Forms.ControlFapch
{
    using Common.Constants;
    using Common.Modularity;

    public class FormControlFapchModule : TabModuleBase
    {
        public FormControlFapchModule() : base(WellKnownModuleNames.FormControlFapchModule)
        {
        }

        #region Overrides of ModuleBase<IServiceLocator>

        protected override void OnInitialized()
        {
            InitTabRegion(WellKnownRegionNames.TabRegion, typeof (Views.FormControlFapchView),
                typeof (ViewModels.FormControlFapchViewModel));
        }

        #endregion
    }
}