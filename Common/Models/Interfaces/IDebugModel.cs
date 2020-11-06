using System.Collections.Generic;

namespace Common.Models.Interfaces
{
    public interface IDebugModel
    {
        IEnumerable<IPacketRecord> LogPacketRecords { get; set; }
        IPacketRecord CurrentPacketRecord { get; set; }
    }

    public interface IPacketRecord
    {
        uint Address { get; set; }
        uint Data { get; set; }
    }
}