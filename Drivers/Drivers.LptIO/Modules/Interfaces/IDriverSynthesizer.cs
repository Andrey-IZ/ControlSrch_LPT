using System;

namespace Drivers.LptIO.Modules.Interfaces
{
    public interface IDriverSynthesizer
    {
        decimal MinFValue { get; set; }
        decimal MaxFValue { get; set; }
        decimal MiddleFValue { get; set; }
        decimal CurrentFValue { get; }

        void SetFrequency(decimal freqValue, byte synType);
    }
}