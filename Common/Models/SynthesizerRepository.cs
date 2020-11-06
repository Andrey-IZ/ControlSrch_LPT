using System.Collections.Generic;
using System.Linq;
using Common.Models.Interfaces;

namespace Common.Models
{
    public class SynthesizerRepository : ISynthesizerRepository
    {
        private readonly List<ISynthesizer> _synthesizers;

        public SynthesizerRepository()
        {
            RebuildStep = new RebuildStep
            {
                CurrentStepValue = 0.1m,
                MaxStepValue = 5,
                MinStepValue = 0.1m,
                MinorDelta = 0.1m,
            };

            _synthesizers = new List<ISynthesizer>
            {
                new Synthesizer // Синтезатор 1
                {
                    Id = 1,
                    Header = "Синтезатор 1",
                    CurrentF = 0,
                    DecimalPlaces = 1,
                    DeltaMinor = 0.1m,
                    IsActiveState = false,
                    MinF = 0,
                    MaxF = 2,
                },
                new Synthesizer // Синтезатор 2
                {
                    Id = 2,
                    Header = "Синтезатор 2",
                    CurrentF = 0,
                    DecimalPlaces = 1,
                    DeltaMinor = 0.1m,
                    IsActiveState = false,
                    MinF = 0,
                    MaxF = 2,
                }
            };
        }

        public ISynthesizer GetSynthesizerById(int id)
        {
            lock (_synthesizers)
            {
                return (from synthesizer in _synthesizers
                    where synthesizer.Id == id
                    select synthesizer).FirstOrDefault();
            }
        }
        public IRebuildStep RebuildStep { get; }

        public IEnumerable<ISynthesizer> GetSynthesizers()
        {
            lock (_synthesizers)
            {
                return from synthesizer in _synthesizers
                       select synthesizer;
            }
        }
    }
}