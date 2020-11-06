
namespace Modules.RebuildFrequency.ViewModels
{
    using Catel.MVVM;
    using System.Threading.Tasks;
    using Catel;
    using Catel.Data;
    using Common.Models.Interfaces;
    using Catel.Messaging;
    using Common.Communications;


    public class RebuildFrequencyViewModel : ViewModelBase
    {
        private readonly IMessageMediator _messageMediator;

        /// <exception cref="System.ArgumentNullException">The <paramref name="rebuildFrequencyRepository"/> is <c>null</c>.</exception>
        /// <exception cref="System.ArgumentNullException">The <paramref name="messageMediator"/> is <c>null</c>.</exception>
        public RebuildFrequencyViewModel(IRebuildFrequencyRepository rebuildFrequencyRepository,
            IMessageMediator messageMediator)
        {
            Argument.IsNotNull(() => messageMediator);
            Argument.IsNotNull(() => rebuildFrequencyRepository);

            _messageMediator = messageMediator;
            RebuildFrequency = rebuildFrequencyRepository.GetRebuildFrequency();
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
        public IRebuildFrequency RebuildFrequency
        {
            get { return GetValue<IRebuildFrequency>(RebuildFrequencyProperty); }
            private set { SetValue(RebuildFrequencyProperty, value); }
        }

        /// <summary>
        /// Register the RebuildFrequency property so it is known in the class.
        /// </summary>
        public static readonly PropertyData RebuildFrequencyProperty = RegisterProperty("RebuildFrequency", typeof(IRebuildFrequency));

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [ViewModelToModel("RebuildFrequency")]
        public bool IsManual
        {
            get { return GetValue<bool>(IsManualProperty); }
            set { SetValue(IsManualProperty, value); }
        }

        /// <summary>
        /// Register the IsManual property so it is known in the class.
        /// </summary>
        public static readonly PropertyData IsManualProperty = RegisterProperty("IsManual", typeof(bool), false, 
            (s,e)=> {
                var vm = s as RebuildFrequencyViewModel;
                var value = (bool) e.NewValue;
                if (vm != null)
                {
                    vm.IsAuto = !value;
                }
        });

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [ViewModelToModel("RebuildFrequency")]
        public bool IsAuto
        {
            get { return GetValue<bool>(IsAutoProperty); }
            set
            {
                SetValue(IsAutoProperty, value);
                _messageMediator.SendMessage<bool>(value, Commands.ChangeIsAutoRebuildF);
                IsStop = false;
            }
        }

        /// <summary>
        /// Register the IsAuto property so it is known in the class.
        /// </summary>
        public static readonly PropertyData IsAutoProperty = RegisterProperty("IsAuto", typeof(bool), false,
            (s, e) => {
                var vm = s as RebuildFrequencyViewModel;
                var value = (bool)e.NewValue;
                if (vm != null)
                {
                    vm.IsManual = !value;
                }
            });

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [ViewModelToModel("RebuildFrequency")]
        public bool IsStop
        {
            get { return GetValue<bool>(IsStopProperty); }
            set
            {
                SetValue(IsStopProperty, value);
                if (value && IsAuto) IsManual = true;
            }
        }

        /// <summary>
        /// Register the IsStop property so it is known in the class.
        /// </summary>
        public static readonly PropertyData IsStopProperty = RegisterProperty("IsStop", typeof(bool));
        #endregion
    }
}
