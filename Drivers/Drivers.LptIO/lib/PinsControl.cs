
namespace Drivers.LptIO.lib
{
    public class PinsControl : IPinsControl
    {
        private readonly IPortControl _port;

        public PinsControl(IPortControl port)
        {
            _port = port;
        }

        /// <summary>
        ///  Битовый расклад регистра УПРАВЛЕНИЯ 
        /// </summary>
        private enum TypeBit
        {
            Strobe = 0x01, //  Строб,          1 /1    
            Autofeed = 0x02, //  Автопротяжка,   14/14   
            Init = 0x04, //  Инициализация,  16/31   
            Selectin = 0x08, //  Выбор принтера, 17/36   
            Irqe = 0x10, //  Прерывание,     ------  
            Direction = 0x20, //  Направление ШД, ------  
        }

        #region Implementation of IPinsControl

        public bool Pin1 => true ^
                            ((int) TypeBit.Strobe ==
                             ((int) TypeBit.Strobe & _port.ReadPort(TypeLptRegister.ControlRegister)));

        public bool Pin14 => true ^
                             ((int) TypeBit.Autofeed ==
                              ((int) TypeBit.Autofeed & _port.ReadPort(TypeLptRegister.ControlRegister)));

        public bool Pin16 => true ^
                             ((int) TypeBit.Init ==
                              ((int) TypeBit.Init & _port.ReadPort(TypeLptRegister.ControlRegister)));

        public bool Pin17 => true ^
                             ((int) TypeBit.Selectin ==
                              ((int) TypeBit.Selectin & _port.ReadPort(TypeLptRegister.ControlRegister)));

        public void SetPin1()
        {
            _port.WritePort((byte) (1 ^ _port.ReadPort(TypeLptRegister.ControlRegister)),
                TypeLptRegister.ControlRegister);
        }

        public void SetPin14()
        {
            _port.WritePort((byte) (2 ^ _port.ReadPort(TypeLptRegister.ControlRegister)),
                TypeLptRegister.ControlRegister);
        }

        public void SetPin16()
        {
            _port.WritePort((byte) (4 ^ _port.ReadPort(TypeLptRegister.ControlRegister)),
                TypeLptRegister.ControlRegister);
        }

        public void SetPin17()
        {
            _port.WritePort((byte) (8 ^ _port.ReadPort(TypeLptRegister.ControlRegister)),
                TypeLptRegister.ControlRegister);
        }

        #endregion
    }
}