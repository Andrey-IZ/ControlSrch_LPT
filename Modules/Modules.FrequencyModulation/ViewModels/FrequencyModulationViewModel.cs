
namespace Modules.FrequencyModulation.ViewModels
{
    using Catel.MVVM;
    using Catel;
    using Catel.Data;
    using Common.Models.Interfaces;
    using System.Threading.Tasks;
    using Common.Services.Interfaces;
    

    public class FrequencyModulationViewModel : ViewModelBase
    {
        private readonly IRemoteControlService _remoteControlService;

        /// <exception cref="System.ArgumentNullException">The <paramref name="frequencyModulationRepository"/> is <c>null</c>.</exception>
        /// <exception cref="System.ArgumentNullException">The <paramref name="remoteControlService"/> is <c>null</c>.</exception>
        public FrequencyModulationViewModel(IFrequencyModulationRepository frequencyModulationRepository,
            IRemoteControlService remoteControlService)
        {
            Argument.IsNotNull(() => remoteControlService);
            Argument.IsNotNull(() => frequencyModulationRepository);

            _remoteControlService = remoteControlService;
            Modulation = frequencyModulationRepository.GetFrequencyModulation();

            IncreaseAmplitudeMseqCommand = new Command(OnIncreaseAmplitudeMseqCommandExecute);
            DecreaseAmplitudeMseqCommand = new Command(OnDecreaseAmplitudeMseqCommandExecute);

            IsAnimChangedValue = true;
            IsAnimChangedValueAmplMod = true;
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

        #region Commands
        /// <summary>
            /// Gets the IncreaseAmplitudeMseqCommand command.   
            /// </summary>
        public Command IncreaseAmplitudeMseqCommand { get; private set; }

        /// <summary>
        /// Method to invoke when the IncreaseAmplitudeMseqCommand command is executed.
        /// </summary>
        private void OnIncreaseAmplitudeMseqCommandExecute()
        {
            var value = Modulation.AmplitudeCodeMseq * 2 + 1;
            if (value <= Modulation.MaxAmplitudeCodeMseq && value >= Modulation.MinAmplitudeCodeMseq)
                AmplitudeCodeMseq = value;
        }

        /// <summary>
            /// Gets the DecreaseAmplitudeMseqCommand command.
            /// </summary>
        public Command DecreaseAmplitudeMseqCommand { get; private set; }

        /// <summary>
        /// Method to invoke when the DecreaseAmplitudeMseqCommand command is executed.
        /// </summary>
        private void OnDecreaseAmplitudeMseqCommandExecute()
        {
            var value = ((Modulation.AmplitudeCodeMseq +1) / 2) - 1 ;
            if (value <= Modulation.MaxAmplitudeCodeMseq && value >= Modulation.MinAmplitudeCodeMseq)
                AmplitudeCodeMseq = value;
        }
        #endregion

        #region Property
        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [Model]
        [Catel.Fody.Expose("MinAmplitudeCode")]
        [Catel.Fody.Expose("MaxAmplitudeCode")]
        [Catel.Fody.Expose("MinAmplitudeCodeMseq")]
        [Catel.Fody.Expose("MaxAmplitudeCodeMseq")]
        [Catel.Fody.Expose("MinorDeltaAmplitudeCode")]
        public IFrequencyModulation Modulation
        {
            get { return GetValue<IFrequencyModulation>(ModulationProperty); }
            private set { SetValue(ModulationProperty, value); }
        }

        /// <summary>
        /// Register the Modulation property so it is known in the class.
        /// </summary>
        public static readonly PropertyData ModulationProperty = RegisterProperty("Modulation", typeof(IFrequencyModulation));

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [ViewModelToModel("Modulation")]
        public uint AmplitudeCode
        {
            get { return GetValue<uint>(AmplitudeCodeProperty); }
            set
            {
                SetValue(AmplitudeCodeProperty, value);
                _remoteControlService.SetStartWork();
                IsStartAnimChangedValue = false;
            }
        }

        /// <summary>
        /// Register the AmplitudeCode property so it is known in the class.
        /// </summary>
        public static readonly PropertyData AmplitudeCodeProperty = RegisterProperty("AmplitudeCode", typeof(uint));

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [ViewModelToModel("Modulation")]
        public bool IsOnUk1
        {
            get { return GetValue<bool>(IsOnUk1Property); }
            set
            {
                SetValue(IsOnUk1Property, value);
                _remoteControlService.SetStartWork();
            }
        }

        /// <summary>
        /// Register the IsOnUk1 property so it is known in the class.
        /// </summary>
        public static readonly PropertyData IsOnUk1Property = RegisterProperty("IsOnUk1", typeof(bool));
        
        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [ViewModelToModel("Modulation")]
        public bool IsOnUk2
        {
            get { return GetValue<bool>(IsOnUk2Property); }
            set
            {
                SetValue(IsOnUk2Property, value);
                _remoteControlService.SetStartWork();
            }
        }

        /// <summary>
        /// Register the IsOnUk1 property so it is known in the class.
        /// </summary>
        public static readonly PropertyData IsOnUk2Property = RegisterProperty("IsOnUk2", typeof(bool));

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [ViewModelToModel("Modulation")]
        public bool IsNoise
        {
            get { return GetValue<bool>(IsNoiesProperty); }
            set
            {
                SetValue(IsNoiesProperty, value);
                _remoteControlService.SetStartWork();
            }
        }

        /// <summary>
        /// Register the IsNoise property so it is known in the class.
        /// </summary>
        public static readonly PropertyData IsNoiesProperty = RegisterProperty("IsNoise", typeof(bool));

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [ViewModelToModel("Modulation")]
        public uint AmplitudeCodeMseq
        {
            get { return GetValue<uint>(AmplitudeCodeMseqProperty); }
            set
            {
                SetValue(AmplitudeCodeMseqProperty, value);
                IsStartAnimChangedValueAmplMod = false;
                _remoteControlService.SetStartWork();
            }
        }

        /// <summary>
        /// Register the AmplitudeCodeMseq property so it is known in the class.
        /// </summary>
        public static readonly PropertyData AmplitudeCodeMseqProperty = RegisterProperty("AmplitudeCodeMseq", typeof(uint));

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [ViewModelToModel("Modulation")]
        public bool IsMseq
        {
            get { return GetValue<bool>(IsMseqProperty); }
            set
            {
                SetValue(IsMseqProperty, value);
                _remoteControlService.SetStartWork();
            }
        }

        /// <summary>
        /// Register the IsMseq property so it is known in the class.
        /// </summary>
        public static readonly PropertyData IsMseqProperty = RegisterProperty("IsMseq", typeof(bool));

        #region Animation of Control
        public static readonly PropertyData IsAnimChangedValueProperty = RegisterProperty<FrequencyModulationViewModel, bool>(model => model.IsAnimChangedValue, default(bool));

        public bool IsAnimChangedValue
        {
            get { return GetValue<bool>(IsAnimChangedValueProperty); }
            set { SetValue(IsAnimChangedValueProperty, value); }
        }
        
        public static readonly PropertyData IsStartAnimChangedValueProperty = RegisterProperty<FrequencyModulationViewModel, bool>(model => model.IsStartAnimChangedValue, default(bool));

        public bool IsStartAnimChangedValue
        {
            get { return GetValue<bool>(IsStartAnimChangedValueProperty); }
            set { SetValue(IsStartAnimChangedValueProperty, value); }
        }

        public static readonly PropertyData IsAnimChangedValueAmplModProperty = RegisterProperty<FrequencyModulationViewModel, bool>(model => model.IsAnimChangedValueAmplMod, default(bool));

        public bool IsAnimChangedValueAmplMod
        {
            get { return GetValue<bool>(IsAnimChangedValueAmplModProperty); }
            set { SetValue(IsAnimChangedValueAmplModProperty, value); }
        }


        public static readonly PropertyData IsStartAnimChangedValueAmplModProperty = RegisterProperty<FrequencyModulationViewModel, bool>(model => model.IsStartAnimChangedValueAmplMod, default(bool));

        public bool IsStartAnimChangedValueAmplMod
        {
            get { return GetValue<bool>(IsStartAnimChangedValueAmplModProperty); }
            set { SetValue(IsStartAnimChangedValueAmplModProperty, value); }
        }
        #endregion

        #endregion
    }
}
