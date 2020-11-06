using System.Collections.Generic;
using Common.Models.Interfaces;
using Drivers.LptIO.lib;

namespace Common.Services.Interfaces
{
    public interface IRemoteControlService
    {
        IEnumerable<int> PortAddressList { get; }
        bool LinkStatus { get; }
        bool KnpStatus { get; }
        bool Ak2Status { get; }
        bool Ak1Status { get; }
        bool CaptureFapch1Status { get; }
        bool CaptureFapch2Status { get; }

        IRebuildFrequency RebuildFreq { get; }
        ISynthesizer Syn1 { get; }
        ISynthesizer Syn2 { get; }
        IGun Gun1 { get; }
        IGun Gun2 { get; }
        IExitsAk ExitsAk { get; }

        TypeLptPort CurrentLptPort { get; set; }
        bool SetPortAddress(int portAddress);

        void Run();
        void SetStartWork();

        void WriteDebugData(byte address, ushort data);
    }
}