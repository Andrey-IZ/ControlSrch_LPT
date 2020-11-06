using System;
using System.Collections.Generic;
using System.Linq;
using Drivers.LptIO.lib;
using Drivers.LptIO.Modules;

namespace Drivers.LptIO
{
    public class LptRemoteControl: ILtpRemoteControl
    {
        private IPortControl _port;
        private readonly DriverSynthesizer _controlSyn;
        private readonly List<int> _portAddressList;

        public LptRemoteControl()
        {
            _port = new PortControl();
            _controlSyn = new DriverSynthesizer(_port, 0, 2, 1);

            CurrentAddressPort = _port.GetAddressPort(_port.CurrentTypeLptPort);
            _portAddressList = _port.GetPortAddressList().ToList();
            _port.SbrosLpt();
        }

        #region Implementation of ILtpRemoteControl

        public IEnumerable<int> GetActualPortAddressList()
        {
            return _portAddressList;
        }

        public TypeLptPort CurrentTypeLptPort
        {
            get { return _port.CurrentTypeLptPort; }
            set { _port.CurrentTypeLptPort = value; }
        }

        public int CurrentAddressPort { get; set; }

        public bool SetPortAddress(int portAddress)
        {
            return _port.SetPortAddress(portAddress);
        }

        public bool ReadKnp()
        {
            return _port.PinsStatus.Pin11;
        }

        public bool ReadAk1()
        {
            _port.SendBurstLpt(0x0, 0);
            return (_port.ReadPort(TypeLptRegister.StatusRegister) & 0x0) != 0;
        }

        public bool ReadAk2()
        {
            _port.SendBurstLpt(0x0, 1);
            return (_port.ReadPort(TypeLptRegister.StatusRegister) & 0x0 ) != 0;
        }

        public bool ReadCaptureFapch1()
        {
            _port.SendBurstLpt(0x0, 04);
            return (_port.ReadPort(TypeLptRegister.StatusRegister) & 0x0) != 0;
        }

        public bool ReadCaptureFapch2()
        {
            _port.SendBurstLpt(0x0, 02);
            return (_port.ReadPort(TypeLptRegister.StatusRegister) & 0x0) != 0;
        }

        public bool TestLink()
        {
            var testLink = true;
            return testLink;
        }

        public void WriteGun1(int value)
        {
            _port.SendBurstLpt(0x07, (ushort) value);  // ГУН 1
        }

        public void WriteGun2(int value)
        {
            _port.SendBurstLpt(0x03, (ushort)value);  // ГУН 2
        }

        public void WriteSyn1(decimal value)
        {
            _controlSyn.SetFrequency(value, 1);     // Синтезатор 1
        }

        public void WriteSyn2(decimal value)
        {
            _controlSyn.SetFrequency(value, 2);     // Синтезатор 2
        }

        public void WriteParamModulation(uint amplitudeCode, bool isNoise, uint amplitudeCodeMseq, bool isMseq)
        {

        }

        public void WriteCommutators(bool isUk1, bool isUk2)
        {
            var valueTemp = 0;
            if (isUk1) valueTemp = 0x01;
            if (isUk2) valueTemp |= 0x02;

            _port.SendBurstLpt(0x0, (ushort) valueTemp);
        }

        public void WriteModeWork(RemoteModeWork rmw)
        {
            var tempValue = 0;
            if (rmw.IsGun1) tempValue = 0x01;   // ГУН 1
            // TODO: check that assertion
            if (rmw.IsSyn1) tempValue = 0x2;   // Синтезатор 1
            if (rmw.IsSyn2) tempValue |= 0x3;   // Синтезатор 2
            if (rmw.IsGun2) tempValue |= 0x4;  // ГУН 2

            if (rmw.IsExit1)                    // Выход 1
                if (rmw.IsExit1HighLimit)
                    tempValue |= 1;         // 8-10
                else if (rmw.IsExit1LowLimit)
                    tempValue |= 0x1;         // 10-12

            if (rmw.IsExit2)                    // Выход 2
                if (rmw.IsExit2LowLimit)
                    tempValue |= 0x1;         // 8-10
                else if (rmw.IsExit2HighLimit)
                    tempValue |= 0x1;         // 10-12

            _port.SendBurstLpt(0x0, (ushort) tempValue);
        }

        public void ShutdownSrch()
        {
            _port.SendBurstLpt(0x0, 0x0);
        }

        public void WriteDebugData(byte address, ushort data)
        {
            _port.SendBurstLpt(address, data);
        }

        #endregion
    }
}