
namespace Modules.Forms.ControlFapch.ViewModels
{
    using Common.Models;
    using System.Threading.Tasks;
    using Common.ViewModel;

    public class FormControlFapchViewModel : TabViewModel
    {
        public FormControlFapchViewModel()
        {
            TabModel = new TabModel("Контроль ФАПЧ");
        }

        public override string Title { get { return "FormControlFapchViewModel View model title"; } }

        // TODO: Register models with the vmpropmodel codesnippet
        // TODO: Register view model properties with the vmprop or vmpropviewmodeltomodel codesnippets
        // TODO: Register commands with the vmcommand or vmcommandwithcanexecute codesnippets

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            // TODO: subscribe to events here
        }

        protected override async Task CloseAsync()
        {
            // TODO: unsubscribe from events here

            await base.CloseAsync();
        }
    }
}
