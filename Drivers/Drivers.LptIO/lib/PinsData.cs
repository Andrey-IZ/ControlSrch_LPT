namespace Drivers.LptIO.lib
{
    public class PinsData:IPinsData
    {
        private readonly IPortControl _port;

        public PinsData(IPortControl port)
        {
            _port = port;
        }

        #region Implementation of IPinsData

        public bool Pin2 => 1 == (1 & _port.ReadPort());
        public bool Pin3 => 2 == (2 & _port.ReadPort());
        public bool Pin4 => 4 == (4 & _port.ReadPort());
        public bool Pin5 => 8 == (8 & _port.ReadPort());
        public bool Pin6 => 16 == (16 & _port.ReadPort());
        public bool Pin7 => 32 == (32 & _port.ReadPort());
        public bool Pin8 => 64 == (64 & _port.ReadPort());
        public bool Pin9 => 128 == (128 & _port.ReadPort());

        public void SetPin2()
        {
            _port.WritePort((byte)(1 ^ _port.ReadPort()));
        }

        public void SetPin3()
        {
            _port.WritePort((byte)(2 ^ _port.ReadPort()));
        }

        public void SetPin4()
        {
            _port.WritePort((byte)(4 ^ _port.ReadPort()));
        }

        public void SetPin5()
        {
            _port.WritePort((byte)(8 ^ _port.ReadPort()));
        }

        public void SetPin6()
        {
            _port.WritePort((byte)(16 ^ _port.ReadPort()));
        }

        public void SetPin7()
        {
            _port.WritePort((byte)(32 ^ _port.ReadPort()));
        }

        public void SetPin8()
        {
            _port.WritePort((byte)(64 ^ _port.ReadPort()));
        }

        public void SetPin9()
        {
            _port.WritePort((byte)(128 ^ _port.ReadPort()));
        }

        #endregion
    }
}