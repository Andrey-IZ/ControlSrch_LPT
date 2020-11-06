
namespace Modules.Forms.FormDebug.ViewModels
{
    using Catel.MVVM;
    using System.Threading.Tasks;
    using System.Linq;
    using Catel;
    using Catel.Collections;
    using Catel.Data;
    using Common.Models;
    using Common.Models.Interfaces;
    using Common.Services.Interfaces;
    using Common.ViewModel;

    public class FormDebugViewModel : TabViewModel
    {
        private readonly IDebugModelRepository _debugModelRepository;
        private readonly IRemoteControlService _remoteControlService;

        /// <exception cref="System.ArgumentNullException">The <paramref name="remoteControlService"/> is <c>null</c>.</exception>
        public FormDebugViewModel(IDebugModelRepository debugModelRepository, IRemoteControlService remoteControlService)
        {
            Argument.IsNotNull(() => remoteControlService);
            Argument.IsNotNull(() => debugModelRepository);

            this.TabModel = new TabModel("Отладка");

            _debugModelRepository = debugModelRepository;
            _remoteControlService = remoteControlService;

            SendPacketCommand = new Command(OnSendPacketCommandExecute);
            SentPackets = new FastObservableCollection<ISentPacket>(debugModelRepository.GetListSentPackets());
            LastSentPacket = debugModelRepository.CurrentSentPacket;
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
            /// Gets the SendPacketCommand command.
            /// </summary>
        public Command SendPacketCommand { get; private set; }

        /// <summary>
        /// Method to invoke when the SendPacketCommand command is executed.
        /// </summary>
        private void OnSendPacketCommandExecute()
        {
            // TODO: Handle command logic here
            _remoteControlService.WriteDebugData((byte) Address, (ushort) Data);
            LastSentPacket.Address = Address;
            LastSentPacket.Data = Data;
            _debugModelRepository.CurrentSentPacket = LastSentPacket;
            SentPackets.Insert(0, _debugModelRepository.AddPacket(LastSentPacket));
            SelectedSentPacket = SentPackets.FirstOrDefault();
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [Model]
        //[Catel.Fody.Expose("Data")]
        public IPacket LastSentPacket
        {
            get { return GetValue<IPacket>(SentPacketProperty); }
            set { SetValue(SentPacketProperty, value); }
        }

        /// <summary>
        /// Register the LastSentPacket property so it is known in the class.
        /// </summary>
        public static readonly PropertyData SentPacketProperty = RegisterProperty("LastSentPacket", typeof(IPacket));

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [ViewModelToModel("LastSentPacket")]
        public uint Address
        {
            get { return GetValue<uint>(AddressProperty); }
            set { SetValue(AddressProperty, value); }
        }

        /// <summary>
        /// Register the Address property so it is known in the class.
        /// </summary>
        public static readonly PropertyData AddressProperty = RegisterProperty("Address", typeof(uint));

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [ViewModelToModel("LastSentPacket")]
        public uint Data
        {
            get { return GetValue<uint>(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        /// <summary>
        /// Register the Address property so it is known in the class.
        /// </summary>
        public static readonly PropertyData DataProperty = RegisterProperty("Data", typeof(uint));
        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public FastObservableCollection<ISentPacket> SentPackets
        {
            get { return GetValue<FastObservableCollection<ISentPacket>>(SentPacketsProperty); }
            set { SetValue(SentPacketsProperty, value); }
        }

        /// <summary>
        /// Register the SentPackets property so it is known in the class.
        /// </summary>
        public static readonly PropertyData SentPacketsProperty = RegisterProperty("SentPackets", typeof(FastObservableCollection<ISentPacket>), null);

        /// <summary>
            /// Gets or sets the property value.
            /// </summary>
        public ISentPacket SelectedSentPacket
        {
            get { return GetValue<ISentPacket>(SelectedSentPacketProperty); }
            set { SetValue(SelectedSentPacketProperty, value); }
        }

        /// <summary>
        /// Register the SelectedSentPacket property so it is known in the class.
        /// </summary>
        public static readonly PropertyData SelectedSentPacketProperty = RegisterProperty("SelectedSentPacket", typeof(ISentPacket), null);
        
        #endregion
    }
}
