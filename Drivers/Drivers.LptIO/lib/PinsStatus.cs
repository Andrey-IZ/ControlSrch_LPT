namespace Drivers.LptIO.lib
{
    public class PinsStatus: IPinsStatus
    {
        private readonly IPortControl _port;

        public PinsStatus(IPortControl port)
        {
            _port = port;
        }

        /// <summary>
        ///  Битовый расклад регистра СОСТОЯНИЯ 
        /// </summary>
        private enum TypeBit
        {
            Irqs = 0x04,        // Флаг прерывания,------           
            Error = 0x08,       // Признак ошибки, 15/32            
            Select = 0x10,      // Признак выбора, 13/13            
            Paperend = 0x20,    // Конец бумаги,   12/12            
            Ack = 0x40,         // Готовность к приёму данных, 10/10
            Busy = 0x80,        // Занятость,      11/11            
        }

        #region Implementation of IPinsStatus

        public bool Pin10 => true ^
                             ((int)TypeBit.Ack ==
                              ((int)TypeBit.Ack & _port.ReadPort(TypeLptRegister.StatusRegister)));

        public bool Pin11 => true ^
                             ((int)TypeBit.Busy ==
                              ((int)TypeBit.Busy & _port.ReadPort(TypeLptRegister.StatusRegister)));

        public bool Pin12 => true ^
                             ((int)TypeBit.Paperend ==
                              ((int)TypeBit.Paperend & _port.ReadPort(TypeLptRegister.StatusRegister)));

        public bool Pin13 => true ^
                             ((int)TypeBit.Select ==
                              ((int)TypeBit.Select & _port.ReadPort(TypeLptRegister.StatusRegister)));

        public bool Pin15 => true ^
                             ((int)TypeBit.Error ==
                              ((int)TypeBit.Error & _port.ReadPort(TypeLptRegister.StatusRegister)));

        #endregion
    }
}