
namespace Modules.StatusBar.ViewModels
{
    using System;
    using System.Windows;
    using System.Windows.Threading;
    using Common.Models.Interfaces;
    using Common.Services.Interfaces;
    using System.Threading.Tasks;
    using Catel;
    using Catel.MVVM;
    using Catel.Data;

    public class StatusBarViewModel : ViewModelBase
    {
        public readonly IRemoteControlService RemoteControlService;

        /// <exception cref="System.ArgumentNullException">The <paramref name="statusBarRepository"/> is <c>null</c>.</exception>
        /// <exception cref="System.ArgumentNullException">The <paramref name="remoteControlService"/> is <c>null</c>.</exception>
        public StatusBarViewModel(IRemoteControlService remoteControlService, IStatusBarRepository statusBarRepository)
        {
            Argument.IsNotNull(() => remoteControlService);
            Argument.IsNotNull(() => statusBarRepository);

            RemoteControlService = remoteControlService;
            StatusBar = statusBarRepository.GetStatusBar();
            AlwaysInvokeNotifyChanged = true;
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
        [Catel.Fody.Expose("IsKnp")]
        [Catel.Fody.Expose("IsLinkOn")]
        [Catel.Fody.Expose("LptAddressList")]
        public IStatusBar StatusBar
        {
            get { return GetValue<IStatusBar>(StatusBarProperty); }
            private set { SetValue(StatusBarProperty, value); }
        }

        /// <summary>
        /// Register the StatusBar property so it is known in the class.
        /// </summary>
        public static readonly PropertyData StatusBarProperty = RegisterProperty("StatusBar", typeof(IStatusBar));

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [ViewModelToModel("StatusBar")]
        //[ViewToViewModel("TryCurrentLpt", MappingType = ViewToViewModelMappingType.TwoWayViewModelWins)]
        public int CurrentLpt
        {
            get { return GetValue<int>(CurrentLptProperty); }
            set
            {
                if (RemoteControlService != null && RemoteControlService.SetPortAddress(value))
                {
                    SetValue(CurrentLptProperty, value);
                }
                else
                {
                    Application.Current.Dispatcher.BeginInvoke(
                        new Action(() =>
                        {
                            // Do this against the underlying value so 
                            //  that we don't invoke the cancellation question again.
                            SetValue(CurrentLptProperty, CurrentLpt);
                        }),
                        DispatcherPriority.ContextIdle,
                        null);
                }
            }
        }

        /// <summary>
        /// Register the CurrentLpt property so it is known in the class.
        /// </summary>
        public static readonly PropertyData CurrentLptProperty = RegisterProperty("CurrentLpt", typeof(int));
        #endregion
    }
}
