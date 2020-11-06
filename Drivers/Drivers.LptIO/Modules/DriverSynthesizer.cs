using System;
using System.Collections;
using DebugToolsLib.MicroLibrary;
using Drivers.LptIO.lib;
using Drivers.LptIO.Modules.Interfaces;

namespace Drivers.LptIO.Modules
{
    public class DriverSynthesizer : IDriverSynthesizer
    {
        private readonly uint[] _initSequence1 =
        {
            0x1,
            0x1,
            0x1,
            0x1,
            0x1
        };

        private readonly uint[] _initSequence2 =
        {
            0x1,
            0x1,
            0x1,
            0x1,
            0x1
        };

        private readonly IPortControl _port;
        private decimal _currentFValue;

        public DriverSynthesizer(IPortControl port) : this(port, 0, 0, 0)
        {
        }

        public DriverSynthesizer(IPortControl port, decimal minFValue, decimal maxFValue, decimal middleFValue)
        {
            _port = port;
            MinFValue = minFValue;
            MaxFValue = maxFValue;
            MiddleFValue = middleFValue;
            _currentFValue = MinFValue;
        }

        #region Implementation of IDriverSynthesizer

        public decimal MinFValue { get; set; }
        public decimal MaxFValue { get; set; }
        public decimal MiddleFValue { get; set; }

        public decimal CurrentFValue => _currentFValue;

        public void SetFrequency(decimal freqValue, byte synType)
        {
            /// TODO реализация запрещена
            _currentFValue = freqValue;
        }


        private bool GetBits(uint value, byte numBit)
        {
            return (value & (1 << numBit)) != 0;
        }

        #endregion
    }
}