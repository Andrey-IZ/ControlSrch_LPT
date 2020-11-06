
namespace Modules.ManipulationsCode.ViewModels
{
    using System.Windows.Input;
    using Catel.Messaging;
    using Common.Communications;
    using System.Collections.Generic;
    using Common.Messages;
    using Catel;
    using Catel.Data;
    using Common.Models.Interfaces;
    using Catel.MVVM;
    using System.Threading.Tasks;

    public class CodeManipulationViewModel : ViewModelBase
    {
        private readonly ICodeManipulationRepository _codeManipulationRepository;
        public readonly IMessageMediator _messageMediator;

        /// <exception cref="System.ArgumentNullException">The <paramref name="codeManipulationRepository"/> is <c>null</c>.</exception>
        /// <exception cref="System.ArgumentNullException">The <paramref name="messageMediator"/> is <c>null</c>.</exception>
        public CodeManipulationViewModel(ICodeManipulationRepository codeManipulationRepository, IMessageMediator messageMediator)
        {
            Argument.IsNotNull(() => messageMediator);
            Argument.IsNotNull(() => codeManipulationRepository);
            _codeManipulationRepository = codeManipulationRepository;
            _messageMediator = messageMediator;
            GunsList = codeManipulationRepository.GetGuns();
            RebuildStep = codeManipulationRepository.RebuildStep;

            IncreaseStepCommand = new Command(OnIncreaseStepCommandExecute);
            DecreaseStepCommand = new Command(OnDecreaseStepCommandExecute);

            IsAnimChangedValue = true;
        }

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            // TODO: subscribe to events here
            _messageMediator.Register<ChangeStepBuilderMessage>(this, ChangeStepBuilder, Commands.ChangeStepRebuild);
        }

        private void ChangeStepBuilder(ChangeStepBuilderMessage argsMessage)
        {
            var sender = argsMessage.Sender;
            if (argsMessage.IsIncrease)
            {
                IncreaseStepCommand.Execute(sender);
                //IncreaseHltCommand.Execute(null);
            }
            else DecreaseStepCommand.Execute(sender);
        }

        protected override async Task CloseAsync()
        {
            // TODO: unsubscribe from events here
            _messageMediator.UnregisterRecipientAndIgnoreTags(this);

            await base.CloseAsync();
        }

        #region Overrides of ViewModelBase
        public override string Title
        {
            get { return "View model: CodeManipulationViewModel"; }
        }
        #endregion

        // TODO: Register models with the vmpropmodel codesnippet
        // TODO: Register view model properties with the vmprop or vmpropviewmodeltomodel codesnippets
        // TODO: Register commands with the vmcommand or vmcommandwithcanexecute codesnippets

        #region Command
        /// <summary>
        /// Gets the IncreaseStepCommand command.
        /// </summary>
        public Command IncreaseStepCommand { get; private set; }

        /// <summary>
        /// Method to invoke when the IncreaseStepCommand command is executed.
        /// </summary>
        private void OnIncreaseStepCommandExecute()
        {
            var value = CurrentStepValue * 2;
            if (value <= RebuildStep.MaxStepValue && value >= RebuildStep.MinStepValue)
            {
                CurrentStepValue = value;
            }
        }

        /// <summary>
        /// Gets the DecreaseStepCommand command.
        /// </summary>
        public Command DecreaseStepCommand { get; private set; }

        /// <summary>
        /// Method to invoke when the DecreaseStepCommand command is executed.
        /// </summary>
        private void OnDecreaseStepCommandExecute()
        {
            var value = CurrentStepValue / 2;
            if (value <= RebuildStep.MaxStepValue && value >= RebuildStep.MinStepValue)
                CurrentStepValue = value;
        }
        #endregion
        #region Property
        /// <summary>Register the GunsList property so it is known in the class.</summary>
        public static readonly PropertyData GunsListProperty = 
            RegisterProperty<CodeManipulationViewModel, IEnumerable<IGun>>(model => model.GunsList);

        public IEnumerable<IGun> GunsList
        {
            get { return GetValue<IEnumerable<IGun>>(GunsListProperty); }
            set { SetValue(GunsListProperty, value); }
        }

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [Model]
        [Catel.Fody.Expose("MaxStepValue")]
        [Catel.Fody.Expose("MinStepValue")]
        [Catel.Fody.Expose("MinorDelta")]
        public IRebuildStep RebuildStep
        {
            get { return GetValue<IRebuildStep>(RebuildStepProperty); }
            private set { SetValue(RebuildStepProperty, value); }
        }

        /// <summary>
        /// Register the RebuildStep property so it is known in the class.
        /// </summary>
        public static readonly PropertyData RebuildStepProperty = RegisterProperty("RebuildStep", typeof(IRebuildStep));

        /// <summary>
        /// Gets or sets the current value of rebuild's step.
        /// </summary>
        [ViewModelToModel("RebuildStep")]
        public decimal CurrentStepValue
        {
            get { return GetValue<decimal>(CurrentStepValueProperty); }
            set
            {
                SetValue(CurrentStepValueProperty, value);
                IsStartAnimChangedValue = false;
            }
        }

        /// <summary>
        /// Register the CurrentStepValue property so it is known in the class.
        /// </summary>
        public static readonly PropertyData CurrentStepValueProperty = RegisterProperty("CurrentStepValue", typeof(decimal), 1m, OnCurrentStepValueChanged);

        private static void OnCurrentStepValueChanged(object sender, AdvancedPropertyChangedEventArgs e)
        {
            var vm = sender as CodeManipulationViewModel;
            if (vm != null)
            {
                var value = (decimal)e.NewValue;
                vm._messageMediator.SendMessage((int)value, Commands.NotifyStepRebuildChanged);
            }
        }

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public ICommand IncreaseHltCommand
        {
            get { return GetValue<ICommand>(IncreaseHltCommandProperty); }
            set { SetValue(IncreaseHltCommandProperty, value); }
        }

        /// <summary>
        /// Register the IncreaseHltCommand property so it is known in the class.
        /// </summary>
        public static readonly PropertyData IncreaseHltCommandProperty = RegisterProperty("IncreaseHltCommand", typeof(ICommand), null);

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public ICommand DecreaseHltCommand
        {
            get { return GetValue<ICommand>(DecreaseHltCommandProperty); }
            set { SetValue(DecreaseHltCommandProperty, value); }
        }

        /// <summary>
        /// Register the DecreaseHltCommand property so it is known in the class.
        /// </summary>
        public static readonly PropertyData DecreaseHltCommandProperty = RegisterProperty("DecreaseHltCommand", typeof(ICommand), null);

        public static readonly PropertyData IsAnimChangedValueProperty = RegisterProperty<GunViewModel, bool>(model => model.IsAnimChangedValue, default(bool));
        public bool IsAnimChangedValue
        {
            get { return GetValue<bool>(IsAnimChangedValueProperty); }
            set { SetValue(IsAnimChangedValueProperty, value); }
        }


        public static readonly PropertyData IsStartAnimChangedValueProperty = RegisterProperty<GunViewModel, bool>(model => model.IsStartAnimChangedValue, default(bool));

        public bool IsStartAnimChangedValue
        {
            get { return GetValue<bool>(IsStartAnimChangedValueProperty); }
            set { SetValue(IsStartAnimChangedValueProperty, value); }
        }

        #endregion
    }
}
