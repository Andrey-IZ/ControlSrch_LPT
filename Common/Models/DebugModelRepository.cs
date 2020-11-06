using Common.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catel;

namespace Common.Models
{
    public class DebugModelRepository : IDebugModelRepository
    {
        private readonly List<ISentPacket> _sentPackets;

        public DebugModelRepository()
        {
            CurrentSentPacket = new Packet();
            _sentPackets = new List<ISentPacket>();
        }

        public IPacket CurrentSentPacket { get; set; }

        public IEnumerable<ISentPacket> GetListSentPackets()
        {
            return _sentPackets;
        }

        public ISentPacket AddPacket(IPacket sentPacket)
        {
            Argument.IsNotNull("sentPacket", sentPacket);
            var packet = new SentPacket(sentPacket);
            lock (_sentPackets)
            {
                _sentPackets.Add(packet); 
            }
            return packet;
        }

        public void ClearPacketList()
        {
            lock (_sentPackets)
            {
                _sentPackets.Clear();
            }
        }
    }
}
