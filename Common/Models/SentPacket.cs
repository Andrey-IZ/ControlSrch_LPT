
namespace Common.Models
{
    using System;
    using Catel.Data;
    using Interfaces;

    public class SentPacket: Packet, ISentPacket
    {
        public SentPacket():this(0, 0)
        {
        }

        public SentPacket(uint address, uint data): this(address, data, DateTime.Now)
        {
        }

        public SentPacket(IPacket packet) : this(packet.Address, packet.Data, DateTime.Now)
        {
        }
        public SentPacket(IPacket packet, DateTime time) : this(packet.Address, packet.Data, time)
        {
        }

        public SentPacket(uint address, uint data, DateTime time):base(address, data)
        {
            Time = time;
        }

        /// <summary>
            /// Gets or sets the property value.
            /// </summary>
        public DateTime Time
        {
            get { return GetValue<DateTime>(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        /// <summary>
        /// Register the Time property so it is known in the class.
        /// </summary>
        public static readonly PropertyData TimeProperty = RegisterProperty("Time", typeof(DateTime), null);

    }
}