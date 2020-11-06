
namespace Modules.Forms.CaptureFapch
{
    using Common.Constants;
    using Common.Modularity;


    public class FormCaptureFapchModule : TabModuleBase
    {
        public FormCaptureFapchModule() : base(WellKnownModuleNames.FormCaptureFapchModule)
        {
        }

        #region Overrides of ModuleBase<IServiceLocator>

        protected override void OnInitialized()
        {
            InitTabRegion(WellKnownRegionNames.TabRegion, typeof (Views.FormCaptureFapchView),
                typeof (ViewModels.FormCaptureFapchViewModel));
        }

        #endregion
    }
}