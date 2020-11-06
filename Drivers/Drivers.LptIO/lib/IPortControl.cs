using System.Collections.Generic;

namespace Drivers.LptIO.lib
{
    public interface IPortControl
    {
        IEnumerable<TypeLptPort> PortLptList { get; }
        IEnumerable<int> GetPortAddressList();

        int GetAddressPort(TypeLptPort typeLptPort);
        TypeLptPort GeTypeLptPortOrNone(int portAddress);

        TypeLptPort CurrentTypeLptPort { get; set; }
        bool SetPortAddress(int portAddress);

        /// <summary>
        /// Чтение с порта Lpt и нужного регистра
        /// </summary>
        byte ReadPort(TypeLptRegister typeLptRegister = TypeLptRegister.DataRegister);

        /// <summary>
        /// Запись в порт Lpt в нужный регистр
        /// </summary>
        void WritePort(byte value, TypeLptRegister typeLptRegister = TypeLptRegister.DataRegister);

        void SbrosLpt();

        /// <summary>
        /// Установить сброс
        /// </summary>
        void SendBurstLpt(byte address, ushort data);

        bool IsPortAddressActual(int portAddress);

        IPinsControl PinsControl { get; }
        IPinsData PinsData { get; }
        IPinsStatus PinsStatus { get; }
    }

    public enum TypeLptRegister
    {
        DataRegister = 0,
        StatusRegister = 1,
        ControlRegister = 2,
        EppAddressRegister = 3,
        EppDaataRegister = 4
    }

    public enum TypeLptPort
    {
        None = -1,
        Lpt1,
        Lpt2,
        Lpt3
    }


}
