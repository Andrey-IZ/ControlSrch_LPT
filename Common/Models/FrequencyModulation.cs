
namespace Common.Models
{
    using Interfaces;
    using Catel.Data;

    public class FrequencyModulation: ModelBase, IFrequencyModulation
    {
        public FrequencyModulation()
        {
        }

        #region Implementation of IFrequencyModulation

        /// <summary>Register the IsOnUk1 property so it is known in the class.</summary>
        public static readonly PropertyData IsOnUk1Property = RegisterProperty<FrequencyModulation, bool>(model => model.IsOnUk1);

        public bool IsOnUk1
        {
            get { return GetValue<bool>(IsOnUk1Property); }
            set { SetValue(IsOnUk1Property, value); }
        }

        /// <summary>Register the IsOnUk2 property so it is known in the class.</summary>
        public static readonly PropertyData IsOnUk2Property = RegisterProperty<FrequencyModulation, bool>(model => model.IsOnUk2);

        public bool IsOnUk2
        {
            get { return GetValue<bool>(IsOnUk2Property); }
            set { SetValue(IsOnUk2Property, value); }
        }

        /// <summary>Register the AmplitudeCode property so it is known in the class.</summary>
        public static readonly PropertyData AmplitudeCodeProperty = RegisterProperty<FrequencyModulation, uint>(model => model.AmplitudeCode);

        public uint AmplitudeCode
        {
            get { return GetValue<uint>(AmplitudeCodeProperty); }
            set { SetValue(AmplitudeCodeProperty, value); }
        }

        /// <summary>Register the MinAmplitudeCode property so it is known in the class.</summary>
        public static readonly PropertyData MinAmplitudeCodeProperty = RegisterProperty<FrequencyModulation, int>(model => model.MinAmplitudeCode);

        public int MinAmplitudeCode
        {
            get { return GetValue<int>(MinAmplitudeCodeProperty); }
            set { SetValue(MinAmplitudeCodeProperty, value); }
        }

        /// <summary>Register the MaxAmplitudeCode property so it is known in the class.</summary>
        public static readonly PropertyData MaxAmplitudeCodeProperty = RegisterProperty<FrequencyModulation, int>(model => model.MaxAmplitudeCode);

        public int MaxAmplitudeCode
        {
            get { return GetValue<int>(MaxAmplitudeCodeProperty); }
            set { SetValue(MaxAmplitudeCodeProperty, value); }
        }

        /// <summary>Register the IsNoise property so it is known in the class.</summary>
        public static readonly PropertyData IsNoiseProperty = RegisterProperty<FrequencyModulation, bool>(model => model.IsNoise);

        public bool IsNoise
        {
            get { return GetValue<bool>(IsNoiseProperty); }
            set { SetValue(IsNoiseProperty, value); }
        }

        /// <summary>Register the AmplitudeCodeMseq property so it is known in the class.</summary>
        public static readonly PropertyData AmplitudeCodeMseqProperty = RegisterProperty<FrequencyModulation, uint>(model => model.AmplitudeCodeMseq);

        public uint AmplitudeCodeMseq
        {
            get { return GetValue<uint>(AmplitudeCodeMseqProperty); }
            set { SetValue(AmplitudeCodeMseqProperty, value); }
        }

        /// <summary>Register the MinorDeltaAmplitudeCode property so it is known in the class.</summary>
        public static readonly PropertyData MinorDeltaAmplitudeCodeProperty = RegisterProperty<FrequencyModulation, int>(model => model.MinorDeltaAmplitudeCode);

        public int MinorDeltaAmplitudeCode
        {
            get { return GetValue<int>(MinorDeltaAmplitudeCodeProperty); }
            set { SetValue(MinorDeltaAmplitudeCodeProperty, value); }
        }

        /// <summary>Register the MinAmplitudeCodeMseq property so it is known in the class.</summary>
        public static readonly PropertyData MinAmplitudeCodeMseqProperty = RegisterProperty<FrequencyModulation, int>(model => model.MinAmplitudeCodeMseq);

        public int MinAmplitudeCodeMseq
        {
            get { return GetValue<int>(MinAmplitudeCodeMseqProperty); }
            set { SetValue(MinAmplitudeCodeMseqProperty, value); }
        }

        /// <summary>Register the MaxAmplitudeCodeMseq property so it is known in the class.</summary>
        public static readonly PropertyData MaxAmplitudeCodeMseqProperty = RegisterProperty<FrequencyModulation, int>(model => model.MaxAmplitudeCodeMseq);

        public int MaxAmplitudeCodeMseq
        {
            get { return GetValue<int>(MaxAmplitudeCodeMseqProperty); }
            set { SetValue(MaxAmplitudeCodeMseqProperty, value); }
        }

        /// <summary>Register the IsMseq property so it is known in the class.</summary>
        public static readonly PropertyData IsMseqProperty = RegisterProperty<FrequencyModulation, bool>(model => model.IsMseq);

        public bool IsMseq
        {
            get { return GetValue<bool>(IsMseqProperty); }
            set { SetValue(IsMseqProperty, value); }
        }

        #endregion
    }
}