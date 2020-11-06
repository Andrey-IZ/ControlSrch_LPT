
namespace Modules.ManipulationsCode.ViewModels
{
    using Common.Communications;
    using Common.Messages;
    using Catel;
    using Catel.Data;
    using Catel.Messaging;
    using Common.Models.Interfaces;
    using Catel.MVVM;
    using System.Threading.Tasks;
    using Common.Services.Interfaces;
    using System;
    using System.Windows.Input;
    using System.Windows.Threading;


    public class GunViewModel : ViewModelBase
    {
        private readonly IMessageMediator _messageMediator;
        private readonly IRemoteControlService _rControlService;
        private readonly object _lockObject = new object();

        /// <exception cref="System.ArgumentNullException">The <paramref name="gun"/> is <c>null</c>.</exception>
        /// <exception cref="System.ArgumentNullException">The <paramref name="messageMediator"/> is <c>null</c>.</exception>
        /// <exception cref="System.ArgumentNullException">The <paramref name="rControlService"/> is <c>null</c>.</exception>
        public GunViewModel(IGun gun, IMessageMediator messageMediator, IRemoteControlService rControlService)
        {
            Argument.IsNotNull(() => rControlService);
            Argument.IsNotNull(() => gun);
            Argument.IsNotNull(() => messageMediator);
            _messageMediator = messageMediator;
            _rControlService = rControlService;
            Gun = gun;
            SetAutoEnabled(_rControlService.RebuildFreq.IsAuto);

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

        public override string Title { get { return "View model title"; } }

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            // TODO: subscribe to events here
            _messageMediator.Register<int>(this, deltaMinor => Gun.DeltaMinor = deltaMinor, Commands.NotifyStepRebuildChanged);
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
            _messageMediator.SendMessage(new ChangeStepBuilderMessage(this, false), Commands.ChangeStepRebuild);
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
            _messageMediator.SendMessage(new ChangeStepBuilderMessage(this, true), Commands.ChangeStepRebuild);
        }
        #endregion

        #region Handlers of Messages
        #endregion

        #region Property
        /// <summary>Register the Gun property so it is known in the class.</summary>
        public static readonly PropertyData GunProperty = RegisterProperty<GunViewModel, IGun>(model => model.Gun);

        [Model]
        [Catel.Fody.Expose("Header")]
        [Catel.Fody.Expose("DecimalPlaces")]
        [Catel.Fody.Expose("MinGunValue")]
        [Catel.Fody.Expose("MaxGunValue")]
        [Catel.Fody.Expose("DeltaMinor")]
        public IGun Gun
        {
            get { return GetValue<IGun>(GunProperty); }
            set { SetValue(GunProperty, value); }
        }

        /// <summary>Register the CurrentGunValue property so it is known in the class.</summary>
        public static readonly PropertyData CurrentGunValueProperty = RegisterProperty<GunViewModel, int>(model => model.CurrentGunValue, default(int), (s, e) => s.OnCurrentGunValueChanged());

        [ViewModelToModel("Gun")]
        public int CurrentGunValue
        {
            get { return GetValue<int>(CurrentGunValueProperty); }
            set
            {
                SetValue(CurrentGunValueProperty, value);
                IsStartAnimChangedValue = false;
                _rControlService.SetStartWork();
            }
        }

        /// <summary>Occurs when the value of the CurrentGunValue property is changed.</summary>
        private void OnCurrentGunValueChanged()
        {
        }

        /// <summary>Register the IsActiveGunState property so it is known in the class.</summary>
        public static readonly PropertyData IsActiveStateProperty = RegisterProperty<GunViewModel, bool>(model => model.IsActiveState, default(bool),
            (s, e) =>
            {
                var newValue = (bool)e.NewValue;
                var vm = s;

                if (!vm.IsActiveState)
                    vm.StopAutoChange();

                if (newValue)
                {
                    if (s.Gun.Id == 1)
                    {
                        if (s._rControlService.ExitsAk.IsCheckedExit1)
                            s._rControlService.ExitsAk.IsLowThresholdExit1 = false;
                        else if (s._rControlService.ExitsAk.IsCheckedExit2)
                            s._rControlService.ExitsAk.IsHighThresholdExit2 = false;

                        s._rControlService.Syn1.IsActiveState = false;
                        s._rControlService.Gun2.IsActiveGunState = false;
                    }
                    else if (s.Gun.Id == 2)
                    {
                        if (s._rControlService.ExitsAk.IsCheckedExit2)
                            s._rControlService.ExitsAk.IsLowThresholdExit2 = false;
                        else if (s._rControlService.ExitsAk.IsCheckedExit1)
                            s._rControlService.ExitsAk.IsHighThresholdExit1 = false;

                        s._rControlService.Syn2.IsActiveState = false;
                        s._rControlService.Gun1.IsActiveGunState = false;
                    }
                }
                s._rControlService.SetStartWork();
            });

        [ViewModelToModel("Gun", "IsActiveGunState")]
        public bool IsActiveState
        {
            get { return GetValue<bool>(IsActiveStateProperty); }
            set { SetValue(IsActiveStateProperty, value); }
        }

        #region Animation of Control
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
                var value = CurrentGunValue + Gun.DeltaMinor;
                if (value > Gun.MaxGunValue)
                {
                    StopAutoChange();
                    CurrentGunValue = Gun.MaxGunValue;
                }
                else if (value >= Gun.MinGunValue)
                {
                    CurrentGunValue += (int) Gun.DeltaMinor;
                }
                else
                    StopAutoChange();
            }
        }

        private void DecreaseValue()
        {
            lock (_lockObject)
            {
                var value = CurrentGunValue - Gun.DeltaMinor;
                if (value < Gun.MinGunValue)
                {
                    StopAutoChange();
                    CurrentGunValue = Gun.MinGunValue;
                }
                else if (value <= Gun.MaxGunValue)
                {
                    CurrentGunValue -= (int) Gun.DeltaMinor;
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
            if (_isAutoUp) { IncreaseValue(); IncreaseHltCommand.Execute(null); }
            else { DecreaseValue(); DecreaseHltCommand.Execute(null); }
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
            }
        }

        private bool _isAutoEnabled = false;
        private void SetAutoEnabled(bool isAutoEnabled)
        {
            _isAutoEnabled = isAutoEnabled;
            if (!_isAutoEnabled && IsAutoRunning) StopAutoChange();
        }

        #endregion
    }
}
