using System.Collections.Generic;
using Drivers.LptIO.lib;

namespace Drivers.LptIO
{
    public interface ILtpRemoteControl
    {
        IEnumerable<int> GetActualPortAddressList();

        TypeLptPort CurrentTypeLptPort { get; set; }
        int CurrentAddressPort { get; }

        bool SetPortAddress(int portAddress);

        bool ReadKnp();
        bool ReadAk1();
        bool ReadAk2();
        bool ReadCaptureFapch1();
        bool ReadCaptureFapch2();
        /// <summary>
        /// Проверка канала связи
        /// </summary>
        /// <returns></returns>
        bool TestLink();
        void WriteGun1(int value);

        void WriteGun2(int value);

        void WriteSyn1(decimal value);

        void WriteSyn2(decimal value);
        void WriteParamModulation(uint amplitudeCode, bool isNoise, uint amplitudeCodeMseq, bool isMseq);
        void WriteCommutators(bool isUk1, bool isUk2);
        void WriteModeWork(RemoteModeWork rmw);
        /// <summary>
        /// выключение СРЧ
        /// </summary>
        void ShutdownSrch();

        void WriteDebugData(byte address, ushort data);
    }

    public class RemoteModeWork
    {
        public bool IsGun1 { get; set; }
        public bool IsGun2 { get; set; }
        public bool IsSyn1 { get; set; }
        public bool IsSyn2 { get; set; }
        public bool IsExit1 { get; set; }
        public bool IsExit1HighLimit { get; set; }
        public bool IsExit1LowLimit { get; set; }
        public bool IsExit2 { get; set; }
        public bool IsExit2HighLimit { get; set; }
        public bool IsExit2LowLimit { get; set; }
    }
}