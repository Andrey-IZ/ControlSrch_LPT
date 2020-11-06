using System;

namespace Common.Models.Interfaces
{
    public interface IPacket
    {
        uint Address { get; set; }
        uint Data { get; set; }
    }

    public interface ISentPacket : IPacket
    {
        DateTime Time { get; set; }
    }
}