
namespace Modules.CaptureFapch.ViewModels
{
    using Catel.MVVM;
    using System.Threading.Tasks;
    using Catel.Data;
    using Common.Models.Interfaces;
    using Catel;

    public class CaptureFapchViewModel : ViewModelBase
    {
        /// <exception cref="System.ArgumentNullException">The <paramref name="captureFapchRepository"/> is <c>null</c>.</exception>
        public CaptureFapchViewModel(ICaptureFapchRepository captureFapchRepository)
        {
            Argument.IsNotNull(() => captureFapchRepository);

            CaptureFapchModel = captureFapchRepository.GetCaptureFapch();
        }

        public override string Title { get { return "View model title"; } }

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

        #region Property
        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [Model]
        [Catel.Fody.Expose("IsCaptureFapch1")]
        [Catel.Fody.Expose("IsCaptureFapch2")]
        public ICaptureFapch CaptureFapchModel
        {
            get { return GetValue<ICaptureFapch>(CaptureFapchModelProperty); }
            private set { SetValue(CaptureFapchModelProperty, value); }
        }

        /// <summary>
        /// Register the CaptureFapchModel property so it is known in the class.
        /// </summary>
        public static readonly PropertyData CaptureFapchModelProperty = RegisterProperty("CaptureFapchModel", typeof(ICaptureFapch));

        #endregion
    }
}
