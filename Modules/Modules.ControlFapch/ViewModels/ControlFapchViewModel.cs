
namespace Modules.ControlFapch.ViewModels
{
    using Catel.MVVM;
    using System.Threading.Tasks;
    using Catel;
    using Catel.Data;
    using Common.Models.Interfaces;

    public class ControlFapchViewModel : ViewModelBase
    {
        /// <exception cref="System.ArgumentNullException">The <paramref name="controlFapchRepository"/> is <c>null</c>.</exception>
        public ControlFapchViewModel(IControlFapchRepository controlFapchRepository)
        {
            Argument.IsNotNull(() => controlFapchRepository);

            ControlFapchModel = controlFapchRepository.GetControlFapch();
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
        public IControlFapch ControlFapchModel
        {
            get { return GetValue<IControlFapch>(ControlFapchModelProperty); }
            private set { SetValue(ControlFapchModelProperty, value); }
        }

        /// <summary>
        /// Register the ControlFapchModel property so it is known in the class.
        /// </summary>
        public static readonly PropertyData ControlFapchModelProperty = RegisterProperty("ControlFapchModel", typeof(IControlFapch));

        /// <summary>
            /// Gets or sets the property value.
            /// </summary>
        [ViewModelToModel("ControlFapchModel")]
        public bool IsDivisorInputSignal
        {
            get { return GetValue<bool>(IsDivisorInputSignalProperty); }
            set { SetValue(IsDivisorInputSignalProperty, value); }
        }

        /// <summary>
        /// Register the IsDivisorInputSignal property so it is known in the class.
        /// </summary>
        public static readonly PropertyData IsDivisorInputSignalProperty = RegisterProperty("IsDivisorInputSignal", typeof(bool));

        /// <summary>
            /// Gets or sets the property value.
            /// </summary>
        [ViewModelToModel("ControlFapchModel")]
        public bool IsDivisorSupportingFreq
        {
            get { return GetValue<bool>(IsDivisorSupportingFreqProperty); }
            set { SetValue(IsDivisorSupportingFreqProperty, value); }
        }

        /// <summary>
        /// Register the IsDivisorSupportingFreq property so it is known in the class.
        /// </summary>
        public static readonly PropertyData IsDivisorSupportingFreqProperty = RegisterProperty("IsDivisorSupportingFreq", typeof(bool));

        /// <summary>
            /// Gets or sets the property value.
            /// </summary>
        [ViewModelToModel("ControlFapchModel")]
        public bool IsPhaseDetectorAnalog
        {
            get { return GetValue<bool>(IsPhaseDetectorAnalogProperty); }
            set { SetValue(IsPhaseDetectorAnalogProperty, value); }
        }

        /// <summary>
        /// Register the IsPhaseDetectorAnalog property so it is known in the class.
        /// </summary>
        public static readonly PropertyData IsPhaseDetectorAnalogProperty = RegisterProperty("IsPhaseDetectorAnalog", typeof(bool));

        /// <summary>
            /// Gets or sets the property value.
            /// </summary>
        [ViewModelToModel("ControlFapchModel")]
        public bool IsPhaseDetectorDigital
        {
            get { return GetValue<bool>(IsPhaseDetectorDigitalProperty); }
            set { SetValue(IsPhaseDetectorDigitalProperty, value); }
        }

        /// <summary>
        /// Register the IsPhaseDetectorDigital property so it is known in the class.
        /// </summary>
        public static readonly PropertyData IsPhaseDetectorDigitalProperty = RegisterProperty("IsPhaseDetectorDigital", typeof(bool));
        #endregion
    }
}
