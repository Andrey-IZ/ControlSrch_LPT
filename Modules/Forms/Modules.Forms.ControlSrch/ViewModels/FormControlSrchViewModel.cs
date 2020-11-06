using Common.Models;
using Common.Models.Interfaces;

namespace Modules.Forms.ControlSrch.ViewModels
{
    using Catel.MVVM;
    using System.Threading.Tasks;
    using Common.ViewModel;

    public class FormControlSrchViewModel : TabViewModel
    {
       public override string Title { get { return "FormControlSrchViewModel View"; } }
        public FormControlSrchViewModel()
        {
            this.TabModel = new TabModel("Контроль СРЧ");
        }

        
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
