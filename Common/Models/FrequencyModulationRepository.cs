
namespace Common.Models
{
    using Interfaces;

    public class FrequencyModulationRepository : IFrequencyModulationRepository
    {
        private readonly IFrequencyModulation _frequencyModulation;

        public FrequencyModulationRepository()
        {
            _frequencyModulation = new FrequencyModulation
            {
                IsOnUk1 = false,
                IsOnUk2 = false,
                
                IsNoise = false,
                AmplitudeCode = 0,
                MaxAmplitudeCode = 2,
                MinAmplitudeCode = 0,
                MinorDeltaAmplitudeCode = 1,

                IsMseq = false,
                AmplitudeCodeMseq = 0,
                MaxAmplitudeCodeMseq = 1,
                MinAmplitudeCodeMseq = 0,
            };
        }

        #region Implementation of IFrequencyModulationRepository

        public IFrequencyModulation GetFrequencyModulation()
        {
            return _frequencyModulation;
        }

        #endregion
    }
}