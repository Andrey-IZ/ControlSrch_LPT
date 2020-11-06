
namespace Modules.RangeReproductionF.ViewModels
{
    using Catel.MVVM;
    using System.Threading.Tasks;
    using Catel;
    using Catel.Data;
    using Common.Models.Interfaces;
    using Catel.Messaging;
    using Common.Services.Interfaces;

    public class RangeReproductionFViewModel : ViewModelBase
    {
        private readonly IRemoteControlService _remoteControlService;
        private readonly IMessageMediator _messageMediator;

        /// <exception cref="System.ArgumentNullException">The <paramref name="rangeReproductionFRepository"/> is <c>null</c>.</exception>
        /// <exception cref="System.ArgumentNullException">The <paramref name="remoteControlService"/> is <c>null</c>.</exception>
        /// <exception cref="System.ArgumentNullException">The <paramref name="messageMediator"/> is <c>null</c>.</exception>
        public RangeReproductionFViewModel(IRangeReproductionFRepository rangeReproductionFRepository,
            IRemoteControlService remoteControlService, IMessageMediator messageMediator)
        {
            Argument.IsNotNull(() => messageMediator);
            Argument.IsNotNull(() => remoteControlService);
            Argument.IsNotNull(() => rangeReproductionFRepository);

            _remoteControlService = remoteControlService;
            _messageMediator = messageMediator;

            ExistsAk = rangeReproductionFRepository.GetExitsAk();
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
        [Catel.Fody.Expose("IsOnAk1")]
        [Catel.Fody.Expose("IsOnAk2")]
        public IExitsAk ExistsAk
        {
            get { return GetValue<IExitsAk>(ExistsAkProperty); }
            set { SetValue(ExistsAkProperty, value); }
        }

        /// <summary>
        /// Register the ExistsAk property so it is known in the class.
        /// </summary>
        public static readonly PropertyData ExistsAkProperty = RegisterProperty("ExistsAk", typeof(IExitsAk));

        /// <summary>
        /// Gets or sets the IsCheckedExit1 value.
        /// </summary>
        [ViewModelToModel("ExistsAk")]
        public bool IsCheckedExit1
        {
            get { return GetValue<bool>(IsCheckedExit1Property); }
            set { SetValue(IsCheckedExit1Property, value); }
        }

        /// <summary>
        /// Register the IsCheckedExit1 property so it is known in the class.
        /// </summary>
        public static readonly PropertyData IsCheckedExit1Property = RegisterProperty("IsCheckedExit1", typeof(bool), false,
            (sender, args) =>
            {
                var vm = sender as RangeReproductionFViewModel;
                if (vm == null) return;

                if ((bool)args.NewValue) vm.IsCheckedExit2 = false;
                else
                {
                    vm.IsHighThresholdExit1 = false;
                    vm.IsLowThresholdExit1 = false;
                }
                // TODO:
                vm._remoteControlService.SetStartWork();    // Why does is do ?
            });

        /// <summary>
        /// Gets or sets the IsCheckedExit2 value.
        /// </summary>
        [ViewModelToModel("ExistsAk")]
        public bool IsCheckedExit2
        {
            get { return GetValue<bool>(IsCheckedExit2Property); }
            set { SetValue(IsCheckedExit2Property, value); }
        }

        /// <summary>
        /// Register the IsCheckedExit2 property so it is known in the class.
        /// </summary>
        public static readonly PropertyData IsCheckedExit2Property = RegisterProperty("IsCheckedExit2", typeof(bool), false,
            (sender, args) =>
            {
                var vm = sender as RangeReproductionFViewModel;
                if (vm == null) return;

                if ((bool)args.NewValue) vm.IsCheckedExit1 = false;
                else
                {
                    vm.IsHighThresholdExit2 = false;
                    vm.IsLowThresholdExit2 = false;
                }
                // TODO:
                vm._remoteControlService.SetStartWork();    // Why does is do ?
            });

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [ViewModelToModel("ExistsAk")]
        public bool IsHighThresholdExit1
        {
            get { return GetValue<bool>(IsHighThresholdExit1Property); }
            set
            {
                SetValue(IsHighThresholdExit1Property, value);
            }
        }

        /// <summary>
        /// Register the IsHighThresholdExit1 property so it is known in the class.
        /// </summary>
        public static readonly PropertyData IsHighThresholdExit1Property = RegisterProperty("IsHighThresholdExit1", typeof(bool), false,
            (sender, args) =>
            {
                var vm = sender as RangeReproductionFViewModel;
                if (vm == null) return;

                if ((bool) args.NewValue)
                {
                    vm.IsLowThresholdExit1 = false;
                    if (!vm.IsCheckedExit2)
                    {
                        vm._remoteControlService.Syn2.IsActiveState = false;
                        vm._remoteControlService.Gun2.IsActiveGunState = false;
                    }
                }
                vm._remoteControlService.SetStartWork();
            });

        /// <summary>
            /// Gets or sets the property value.
            /// </summary>
        [ViewModelToModel("ExistsAk")]
        public bool IsLowThresholdExit1
        {
            get { return GetValue<bool>(IsLowThresholdExit1Property); }
            set { SetValue(IsLowThresholdExit1Property, value); }
        }

        /// <summary>
        /// Register the IsLowThresholdExit1 property so it is known in the class.
        /// </summary>
        public static readonly PropertyData IsLowThresholdExit1Property = RegisterProperty("IsLowThresholdExit1", typeof(bool), false,
            (sender, args) =>
            {
                var vm = sender as RangeReproductionFViewModel;
                if (vm == null) return;

                if ((bool) args.NewValue)
                {
                    vm.IsHighThresholdExit1 = false;
                    if (!vm.IsCheckedExit2)
                    {
                        vm._remoteControlService.Syn1.IsActiveState = false;
                        vm._remoteControlService.Gun1.IsActiveGunState = false;
                    }
                }
                vm._remoteControlService.SetStartWork();
            });

        /// <summary>
            /// Gets or sets the property value.
            /// </summary>
        [ViewModelToModel("ExistsAk")]
        public bool IsHighThresholdExit2
        {
            get { return GetValue<bool>(IsHighThresholdExit2Property); }
            set { SetValue(IsHighThresholdExit2Property, value); }
        }

        /// <summary>
        /// Register the IsHighThresholdExit2 property so it is known in the class.
        /// </summary>
        public static readonly PropertyData IsHighThresholdExit2Property = RegisterProperty("IsHighThresholdExit2", typeof(bool), false,
            (sender, args) =>
            {
                var vm = sender as RangeReproductionFViewModel;
                if (vm == null) return;

                if ((bool) args.NewValue)
                {
                    vm.IsLowThresholdExit2 = false;
                    if (!vm.IsCheckedExit1)
                    {
                        vm._remoteControlService.Syn1.IsActiveState = false;
                        vm._remoteControlService.Gun1.IsActiveGunState = false;
                    }
                }
                vm._remoteControlService.SetStartWork();
    });

        /// <summary>
            /// Gets or sets the property value.
            /// </summary>
        [ViewModelToModel("ExistsAk")]
        public bool IsLowThresholdExit2
        {
            get { return GetValue<bool>(IsLowThresholdExit2Property); }
            set { SetValue(IsLowThresholdExit2Property, value); }
        }

        /// <summary>
        /// Register the IsLowThresholdExit2 property so it is known in the class.
        /// </summary>
        public static readonly PropertyData IsLowThresholdExit2Property = RegisterProperty("IsLowThresholdExit2", typeof(bool), false,
            (sender, args) =>
            {
                var vm = sender as RangeReproductionFViewModel;
                if (vm == null) return;

                if ((bool) args.NewValue)
                {
                    vm.IsHighThresholdExit2 = false;
                    if (!vm.IsCheckedExit1)
                    {
                        vm._remoteControlService.Syn2.IsActiveState = false;
                        vm._remoteControlService.Gun2.IsActiveGunState = false;
                    }
                }
                vm._remoteControlService.SetStartWork();
            });


        #endregion

    }
}
