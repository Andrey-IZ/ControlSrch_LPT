
namespace Modules.Synthesizers.ViewModels
{
    using System;
    using Catel.MVVM;
    using System.Threading.Tasks;
    using Catel.Data;
    using Common.Models.Interfaces;
    using System.Collections.Generic;
    using System.Windows.Input;
    using Catel;
    using Catel.Messaging;
    using Common.Communications;
    using Common.Messages;
    using ManipulationsCode.ViewModels;

    public class SynthesizersViewModel : ViewModelBase
    {
        private readonly IMessageMediator _messageMediator;

        /// <exception cref="System.ArgumentNullException">The <paramref name="messageMediator"/> is <c>null</c>.</exception>
        /// <exception cref="System.ArgumentNullException">The <paramref name="synthesizerRepository"/> is <c>null</c>.</exception>
        public SynthesizersViewModel(ISynthesizerRepository synthesizerRepository, IMessageMediator messageMediator)
        {
            Argument.IsNotNull(() => synthesizerRepository);
            Argument.IsNotNull(() => messageMediator);
            _messageMediator = messageMediator;
            SynthesizersList = synthesizerRepository.GetSynthesizers();
            RebuildStepSyn = synthesizerRepository.RebuildStep;

            IncreaseStepCommand = new Command(OnIncreaseStepCommandExecute);
            DecreaseStepCommand = new Command(OnDecreaseStepCommandExecute);

            IsAnimChangedValue = true;
        }

        public override string Title
        {
            get { return "View model title"; }
        }

        // TODO: Register models with the vmpropmodel codesnippet
        // TODO: Register view model properties with the vmprop or vmpropviewmodeltomodel codesnippets
        // TODO: Register commands with the vmcommand or vmcommandwithcanexecute codesnippets

        /// <summary>Register the SynthesizersList property so it is known in the class.</summary>
        public static readonly PropertyData SynthesizersListProperty =
            RegisterProperty<SynthesizersViewModel, IEnumerable<ISynthesizer>>(
                model => model.SynthesizersList);

        public IEnumerable<ISynthesizer> SynthesizersList
        {
            get { return GetValue<IEnumerable<ISynthesizer>>(SynthesizersListProperty); }
            set { SetValue(SynthesizersListProperty, value); }
        }

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            // TODO: subscribe to events here
            _messageMediator.Register<ChangeStepBuilderMessage>(this, ChangeStepBuilder, Commands.ChangeStepRebuildSyn);
        }

        protected override async Task CloseAsync()
        {
            // TODO: unsubscribe from events here
            _messageMediator.UnregisterRecipientAndIgnoreTags(this);

            await base.CloseAsync();
        }

        private void ChangeStepBuilder(ChangeStepBuilderMessage argsMessage)
        {
            var sender = argsMessage.Sender;

            if (argsMessage.Value == CurrentStepValue)
            {
                if (argsMessage.IsIncrease) IncreaseStepCommand.Execute(sender);
                else DecreaseStepCommand.Execute(sender);
            }
        }

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
            decimal value;
            if (CurrentStepValue < 1) value = CurrentStepValue + 0.1m;
            else value = CurrentStepValue*2;

            if (value <= RebuildStepSyn.MaxStepValue && value >= RebuildStepSyn.MinStepValue)
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
            decimal value;
            if (CurrentStepValue <= 1) value = CurrentStepValue - 0.1m;
            else if (CurrentStepValue/2 <= 1) value = 1;
            else value = Math.Truncate(CurrentStepValue / 2);

            if (value <= RebuildStepSyn.MaxStepValue && value >= RebuildStepSyn.MinStepValue)
                CurrentStepValue = value;
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

        #endregion

        #region Property
        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [Model]
        [Catel.Fody.Expose("MaxStepValue")]
        [Catel.Fody.Expose("MinStepValue")]
        [Catel.Fody.Expose("MinorDelta")]
        public IRebuildStep RebuildStepSyn
        {
            get { return GetValue<IRebuildStep>(RebuildStepProperty); }
            private set { SetValue(RebuildStepProperty, value); }
        }

        /// <summary>
        /// Register the RebuildStepSyn property so it is known in the class.
        /// </summary>
        public static readonly PropertyData RebuildStepProperty = RegisterProperty("RebuildStepSyn", typeof(IRebuildStep));

        /// <summary>
        /// Gets or sets the current value of rebuild's step.
        /// </summary>
        [ViewModelToModel("RebuildStepSyn")]
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
        public static readonly PropertyData CurrentStepValueProperty = RegisterProperty("CurrentStepValue", typeof(decimal), 0.1m, OnCurrentStepValueChanged);

        private static void OnCurrentStepValueChanged(object sender, AdvancedPropertyChangedEventArgs e)
        {
            var vm = sender as SynthesizersViewModel;
            if (vm != null)
            {
                var value = (decimal)e.NewValue;
                if (value != (decimal)e.OldValue)
                    vm._messageMediator.SendMessage(value, Commands.NotifyStepRebuildSynChanged);
            }
        }

       
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
