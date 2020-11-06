using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Drivers.LptIO.lib
{
    public class PortControl : IPortControl
    {
        [DllImport("inpout32.dll")]
        public static extern short Inp32(int address);

        [DllImport("inpout32.dll", EntryPoint = "Out32")]
        public static extern void Output(int adress, int value); // decimal

        public int[] AddressesLptPort { get; } = {
            0x3BC,
            0x378,
            0x278
        };

        private int _curPortAddress;

        public PortControl(TypeLptPort curLptAddress = TypeLptPort.Lpt2)
        {
            PinsControl = new PinsControl(this);
            PinsData = new PinsData(this);
            PinsStatus = new PinsStatus(this);
            CurrentTypeLptPort = curLptAddress;
        }

        public IEnumerable<TypeLptPort> PortLptList => GetPortAddressList().Cast<TypeLptPort>();

        public int GetAddressPort(TypeLptPort typeLptPort)
        {
            return AddressesLptPort.ElementAtOrDefault((int) typeLptPort);
        }

        public TypeLptPort GeTypeLptPortOrNone(int portAddress)
        {
            if (AddressesLptPort.Contains(portAddress))
                return (TypeLptPort) Array.IndexOf(AddressesLptPort, portAddress);
            return TypeLptPort.None;
        }

        public TypeLptPort CurrentTypeLptPort
        {
            get { return GeTypeLptPortOrNone(_curPortAddress); }
            set { _curPortAddress =  GetAddressPort(value); }
        }

        public bool SetPortAddress(int portAddress)
        {
            if (!AddressesLptPort.Contains(portAddress) || !IsPortAddressActual(portAddress))
                return false;
            _curPortAddress = portAddress;
            return true;
        }

        public byte ReadPort(TypeLptRegister typeLptRegister = TypeLptRegister.DataRegister)
        {
            return (byte) Inp32(_curPortAddress + (int) typeLptRegister);
        }

        public void WritePort(byte value, TypeLptRegister typeLptRegister = TypeLptRegister.DataRegister)
        {
            Output(_curPortAddress + (int) typeLptRegister, value);
        }

        public void SbrosLpt()
        {
            /// this is DNA
        }

        /// <summary>
        /// Установить сброс
        /// </summary>
        /// <param name="address"></param>
        /// <param name="data"></param>
        public void SendBurstLpt(byte address, ushort data)
        {
            /// this is DNA
        }

        public bool IsPortAddressActual(int portAddress)
        {
            /// this is DNA
            return true;
        }

        public IEnumerable<int> GetPortAddressList()
        {
            var list = new List<int>();
            for (int i = 0; i < AddressesLptPort.Length; i++)
            {
                if (IsPortAddressActual(AddressesLptPort[i]))
                    list.Add(AddressesLptPort[i]);
            }
            //return AddressesLptPort.Where(IsPortAddressActual).ToList();
            return list;
        }

        public IPinsControl PinsControl { get; }
        public IPinsData PinsData { get; }
        public IPinsStatus PinsStatus { get; }


    }
}
