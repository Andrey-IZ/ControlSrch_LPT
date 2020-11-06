
namespace Modules.Forms.FormDebug
{
    using Common.Constants;
    using Views;
    using Common.Modularity;

    public class FormDebugModule: TabModuleBase
    {
        public FormDebugModule() : base(WellKnownModuleNames.FormDebugModule)
        {
        }

        protected override void OnInitialized()
        {
            InitTabRegion(WellKnownRegionNames.TabRegion, typeof(FormDebugView),
                typeof(ViewModels.FormDebugViewModel));
        }
    }
}