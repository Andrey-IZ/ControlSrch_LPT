
namespace Common.Models
{
    using Catel.Data;
    using Interfaces;

    public class Synthesizer: ModelBase, ISynthesizer
    {
        public Synthesizer()
        {
        }

        public Synthesizer(float minF, float maxF, decimal currentF, ushort deltaMinor, int decimalPlaces, bool isActiveState=false)
        {
            MinF = minF;
            MaxF = maxF;
            CurrentF = currentF;
            DeltaMinor = deltaMinor;
            DecimalPlaces = decimalPlaces;
            IsActiveState = isActiveState;
        }

        #region Implementation of SynthesizersModel

        /// <summary>Register the Header property so it is known in the class.</summary>
        public static readonly PropertyData HeaderProperty = RegisterProperty<Synthesizer, string>(model => model.Header);

        public int Id { get; set; }

        public string Header
        {
            get { return GetValue<string>(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        /// <summary>Register the IsActiveGunState property so it is known in the class.</summary>
        public static readonly PropertyData IsActiveStateProperty = RegisterProperty<Synthesizer, bool>(model => model.IsActiveState, false);

        public bool IsActiveState
        {
            get { return GetValue<bool>(IsActiveStateProperty); }
            set { SetValue(IsActiveStateProperty, value); }
        }

        /// <summary>Register the CurrentF property so it is known in the class.</summary>
        public static readonly PropertyData CurrentFProperty = RegisterProperty<Synthesizer, decimal>(model => model.CurrentF, default(decimal));

        public decimal CurrentF
        {
            get { return GetValue<decimal>(CurrentFProperty); }
            set { SetValue(CurrentFProperty, value); }
        }

        /// <summary>Register the MinF property so it is known in the class.</summary>
        public static readonly PropertyData MinFProperty = RegisterProperty<Synthesizer, float>(model => model.MinF, 0);

        public float MinF
        {
            get { return GetValue<float>(MinFProperty); }
            set { SetValue(MinFProperty, value); }
        }

        /// <summary>Register the MaxF property so it is known in the class.</summary>
        public static readonly PropertyData MaxFProperty = RegisterProperty<Synthesizer, float>(model => model.MaxF, 100);

        public float MaxF
        {
            get { return GetValue<float>(MaxFProperty); }
            set { SetValue(MaxFProperty, value); }
        }

        /// <summary>Register the DeltaMinor property so it is known in the class.</summary>
        public static readonly PropertyData DeltaMinorProperty = RegisterProperty<Synthesizer, decimal>(model => model.DeltaMinor, 1m);

        public decimal DeltaMinor
        {
            get { return GetValue<decimal>(DeltaMinorProperty); }
            set { SetValue(DeltaMinorProperty, value); }
        }

        /// <summary>Register the DecimalPlaces property so it is known in the class.</summary>
        public static readonly PropertyData DecimalPlacesProperty = RegisterProperty<Synthesizer, int>(model => model.DecimalPlaces, 1);

        public int DecimalPlaces
        {
            get { return GetValue<int>(DecimalPlacesProperty); }
            set { SetValue(DecimalPlacesProperty, value); }
        }

        #endregion
    }
}
