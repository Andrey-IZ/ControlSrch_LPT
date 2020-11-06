using System.Collections.Generic;

namespace Common.Models.Interfaces
{
    public interface IDebugModelRepository
    {
        IPacket CurrentSentPacket { get; set; }
        IEnumerable<ISentPacket> GetListSentPackets();

        ISentPacket AddPacket(IPacket sentPacket);
        void ClearPacketList();

    }
}