
namespace Modules.Synthesizers.ViewModels
{
    using Catel;
    using Catel.Data;
    using Catel.Messaging;
    using Catel.MVVM;
    using Common.Communications;
    using Common.Models;
    using Common.Models.Interfaces;
    using System.Threading.Tasks;
    using Common.Services.Interfaces;
    using System;
    using System.Windows.Input;
    using System.Windows.Threading;
    using Common.Messages;


    public class SynViewModel : ViewModelBase
    {
        private readonly IMessageMediator _messageMediator;
        private readonly IRemoteControlService _remoteControlService;
        private readonly object _lockObject = new object();

        /// <exception cref="System.ArgumentNullException">The <paramref name="synthesizer"/> is <c>null</c>.</exception>
        /// <exception cref="System.ArgumentNullException">The <paramref name="messageMediator"/> is <c>null</c>.</exception>
        /// <exception cref="System.ArgumentNullException">The <paramref name="remoteControlService"/> is <c>null</c>.</exception>
        public SynViewModel(ISynthesizer synthesizer, IMessageMediator messageMediator, IRemoteControlService remoteControlService)
        {
            Argument.IsNotNull(() => remoteControlService);
            Argument.IsNotNull(() => synthesizer);
            Argument.IsNotNull(() => messageMediator);

            _messageMediator = messageMediator;
            _remoteControlService = remoteControlService;

            Synthesizer = synthesizer;
            SetAutoEnabled(_remoteControlService.RebuildFreq.IsAuto);

            LeftCommand = new Command(OnLeftCommandExecute);
            RightCommand = new Command(OnRightCommandExecute);

            IncreaseCommand = new Command(OnIncreaseCommandExecute);
            DecreaseCommand = new Command(OnDecreaseCommandExecute);
            _timer = new DispatcherTimer(DispatcherPriority.Background)
            {
                Interval = new TimeSpan(0, 0, 0, 0, 500),
            };
            _timer.Tick += (sender, args) => ProcessAutoChangeValueUpDown();

            IsAnimChangedValue = true;
        }
        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            // TODO: subscribe to events here
            _messageMediator.Register<decimal>(this, deltaMinor => Synthesizer.DeltaMinor = deltaMinor, Commands.NotifyStepRebuildSynChanged);
            _messageMediator.Register<bool>(this, isAutoEnabled => SetAutoEnabled(isAutoEnabled), Commands.ChangeIsAutoRebuildF);
        }

        protected override async Task CloseAsync()
        {
            // TODO: unsubscribe from events here
            _messageMediator.UnregisterRecipientAndIgnoreTags(this);
            StopAutoChange();

            await base.CloseAsync();
        }

        // TODO: Register models with the vmpropmodel codesnippet
        // TODO: Register view model properties with the vmprop or vmpropviewmodeltomodel codesnippets
        // TODO: Register commands with the vmcommand or vmcommandwithcanexecute codesnippets

        #region Commands
        /// <summary>
        /// Gets the LeftCommand command.
        /// </summary>
        public Command LeftCommand { get; private set; }

        /// <summary>
        /// Method to invoke when the LeftCommand command is executed.
        /// </summary>
        private void OnLeftCommandExecute()
        {
            _messageMediator.SendMessage(new ChangeStepBuilderMessage(this, false, Synthesizer.DeltaMinor), Commands.ChangeStepRebuildSyn);
        }

        /// <summary>
        /// Gets the RightCommand command.
        /// </summary>
        public Command RightCommand { get; private set; }

        /// <summary>
        /// Method to invoke when the RightCommand command is executed.
        /// </summary>
        private void OnRightCommandExecute()
        {
            _messageMediator.SendMessage(new ChangeStepBuilderMessage(this, true, Synthesizer.DeltaMinor), Commands.ChangeStepRebuildSyn);
        }

       #endregion

        #region Properties

        /// <summary>Register the Synthesizer property so it is known in the class.</summary>
        public static readonly PropertyData SynthesizerProperty = RegisterProperty<SynViewModel, ISynthesizer>(model => model.Synthesizer);
        [Model]
        [Catel.Fody.Expose("Header")]
        [Catel.Fody.Expose("DecimalPlaces")]
        [Catel.Fody.Expose("MinF")]
        [Catel.Fody.Expose("MaxF")]
        [Catel.Fody.Expose("DeltaMinor")]
        public ISynthesizer Synthesizer
        {
            get { return GetValue<Synthesizer>(SynthesizerProperty); }
            set { SetValue(SynthesizerProperty, value); }
        }

        /// <summary>Register the CurrentF property so it is known in the class.</summary>
        public static readonly PropertyData CurrentFProperty = RegisterProperty<SynViewModel, decimal>(model => model.CurrentF, default(decimal));

        [ViewModelToModel("Synthesizer")]
        public decimal CurrentF
        {
            get { return GetValue<decimal>(CurrentFProperty); }
            set
            {
                SetValue(CurrentFProperty, value);
                IsStartAnimChangedValue = false;
                _remoteControlService.SetStartWork();
            }
        }

        /// <summary>Register the IsActiveGunState property so it is known in the class.</summary>
        public static readonly PropertyData IsActiveStateProperty = RegisterProperty<SynViewModel, bool>(model => model.IsActiveSynState, default(bool),
            (s, e) =>
            {
                var newValue = (bool)e.NewValue;
                var vm = s;

                if (!vm.IsActiveSynState)
                    vm.StopAutoChange();

                if (newValue)
                {
                    if (s.Synthesizer.Id == 1)
                    {
                        if (s._remoteControlService.ExitsAk.IsCheckedExit1)
                            s._remoteControlService.ExitsAk.IsLowThresholdExit1 = false;
                        else if (s._remoteControlService.ExitsAk.IsCheckedExit2)
                            s._remoteControlService.ExitsAk.IsHighThresholdExit2 = false;

                        s._remoteControlService.Gun1.IsActiveGunState = false;
                        s._remoteControlService.Syn2.IsActiveState = false;
                    }
                    else if(s.Synthesizer.Id == 2)
                    {
                        if (s._remoteControlService.ExitsAk.IsCheckedExit2)
                            s._remoteControlService.ExitsAk.IsLowThresholdExit2 = false;
                        else if (s._remoteControlService.ExitsAk.IsCheckedExit1)
                            s._remoteControlService.ExitsAk.IsHighThresholdExit1 = false;

                        s._remoteControlService.Gun2.IsActiveGunState = false;
                        s._remoteControlService.Syn1.IsActiveState = false;
                    }
                }
                s._remoteControlService.SetStartWork();
            });

        [ViewModelToModel("Synthesizer", "IsActiveState")]
        public bool IsActiveSynState
        {
            get { return GetValue<bool>(IsActiveStateProperty); }
            set
            {
                SetValue(IsActiveStateProperty, value); 
                if(!value) StopAutoChange();
            }
        }

        #endregion

        #region Properties for no model
        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public bool IsCustomAnimationRunning
        {
            get { return GetValue<bool>(IsCustomAnimationRunningProperty); }
            set { SetValue(IsCustomAnimationRunningProperty, value); }
        }

        /// <summary>
        /// Register the IsCustomAnimationRunning property so it is known in the class.
        /// </summary>
        public static readonly PropertyData IsCustomAnimationRunningProperty = RegisterProperty("IsCustomAnimationRunning", typeof(bool), null);
        #endregion

        #region Auto-changing value

        private bool _isAutoUp;
        private bool _isAutoRunning;
        public bool IsAutoRunning
        {
            get { return _isAutoRunning; }
            set
            {
                _isAutoRunning = value;
                IsCustomAnimationRunning = _isAutoRunning;
            }
        }

        private void ProcessAutoChangeValueUpDown()
        {
            var isStart = false;

                if (_remoteControlService.Ak1Status || _remoteControlService.Ak2Status)
                {
                   isStart = CheckStatusFapch();
                }

                if (isStart)
                {
                    if (_isAutoUp) { IncreaseValue();IncreaseHltCommand.Execute(null);}
                    else { DecreaseValue(); DecreaseHltCommand.Execute(null);}
                }
        }

        private readonly DispatcherTimer _timer;

        private void StartAutoChangeF(bool isUp)
        {
            if (!_isAutoEnabled) return;

            _isAutoUp = isUp;
            if (!IsAutoRunning)
            {
                IsAutoRunning = true;
                _timer.Start();
            }
            else
            {
                StopAutoChange();
            }
        }

        private void StopAutoChange()
        {
            if (IsAutoRunning)
            {
                IsAutoRunning = false;
                _timer.Stop();
                _timer.IsEnabled = false;
            }
        }

        private bool _isAutoEnabled = false;
        private void SetAutoEnabled(bool isAutoEnabled)
        {
            _isAutoEnabled = isAutoEnabled;
            if (!_isAutoEnabled && IsAutoRunning) StopAutoChange();
        }

        #endregion

        #region Commands Up Down
        /// <summary>
        /// Gets the IncreaseCommand command.
        /// </summary>
        public Command IncreaseCommand { get; private set; }

        /// <summary>
        /// Method to invoke when the IncreaseCommand command is executed.
        /// </summary>
        private void OnIncreaseCommandExecute()
        {
            IncreaseValue();
            StartAutoChangeF(true);
        }

        /// <summary>
        /// Gets the DecreaseStepCommand command.
        /// </summary>
        public Command DecreaseCommand { get; private set; }

        /// <summary>
        /// Method to invoke when the DecreaseStepCommand command is executed.
        /// </summary>
        private void OnDecreaseCommandExecute()
        {
            DecreaseValue();
            StartAutoChangeF(false);
        }

        private void IncreaseValue()
        {
            lock (_lockObject)
            {
                var value = CurrentF + Synthesizer.DeltaMinor;
                if (value > (decimal) Synthesizer.MaxF)
                {
                    StopAutoChange();
                    if (CheckStatusFapch())
                        CurrentF = (decimal) Synthesizer.MaxF;
                    else
                    {IsActiveSynState = false;}
                }
                else if (value >= (decimal)Synthesizer.MinF)
                {
                    if (CheckStatusFapch())
                        CurrentF += Synthesizer.DeltaMinor;
                    else { IsActiveSynState = false; }
                }
                else
                    StopAutoChange();
            }
        }

        private bool CheckStatusFapch()
        {
            if (Synthesizer.Id == 1 && _remoteControlService.CaptureFapch1Status)
                return true;
            if (Synthesizer.Id == 2 && _remoteControlService.CaptureFapch2Status)
                return true;
            return false;
        }

        private void DecreaseValue()
        {
            lock (_lockObject)
            {
                var value = CurrentF - Synthesizer.DeltaMinor;
                if (value < (decimal)Synthesizer.MinF)
                {
                    StopAutoChange();
                    if (CheckStatusFapch())
                        CurrentF = (decimal)Synthesizer.MinF;
                    else { IsActiveSynState = false; }
                }
                else if (value <= (decimal)Synthesizer.MaxF)
                {
                    if (CheckStatusFapch())
                        CurrentF -= Synthesizer.DeltaMinor;
                    else { IsActiveSynState = false; }
                }
                else
                    StopAutoChange();
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

        #endregion

        #region Animation of Control
        public static readonly PropertyData IsAnimChangedValueProperty = RegisterProperty<SynViewModel, bool>(model => model.IsAnimChangedValue, default(bool));

        public bool IsAnimChangedValue
        {
            get { return GetValue<bool>(IsAnimChangedValueProperty); }
            set { SetValue(IsAnimChangedValueProperty, value); }
        }

        public static readonly PropertyData IsStartAnimChangedValueProperty = RegisterProperty<SynViewModel, bool>(model => model.IsStartAnimChangedValue, default(bool));

        public bool IsStartAnimChangedValue
        {
            get { return GetValue<bool>(IsStartAnimChangedValueProperty); }
            set { SetValue(IsStartAnimChangedValueProperty, value); }
        }

        #endregion

    }
}
