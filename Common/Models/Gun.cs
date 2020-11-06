
namespace Common.Models
{
    using Catel.Data;
    using Interfaces;

    public class Gun: ModelBase, IGun
    {
        public Gun()
        {
        }

        #region Implementation of IGun

        /// <summary>Register the Id property so it is known in the class.</summary>
        public static readonly PropertyData IdProperty = RegisterProperty<Gun, int>(model => model.Id);

        public int Id
        {
            get { return GetValue<int>(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

        /// <summary>Register the Header property so it is known in the class.</summary>
        public static readonly PropertyData HeaderProperty = RegisterProperty<Gun, string>(model => model.Header);

        public string Header
        {
            get { return GetValue<string>(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        /// <summary>Register the IsActiveGunState property so it is known in the class.</summary>
        public static readonly PropertyData IsActiveStateProperty = RegisterProperty<Gun, bool>(model => model.IsActiveGunState, default(bool));

        public bool IsActiveGunState
        {
            get { return GetValue<bool>(IsActiveStateProperty); }
            set { SetValue(IsActiveStateProperty, value); }
        }

        /// <summary>Register the CurrentGunValue property so it is known in the class.</summary>
        public static readonly PropertyData CurrentValueProperty = RegisterProperty<Gun, int>(model => model.CurrentGunValue, default(int));

        public int CurrentGunValue
        {
            get { return GetValue<int>(CurrentValueProperty); }
            set { SetValue(CurrentValueProperty, value); }
        }

        /// <summary>Register the MinGunValue property so it is known in the class.</summary>
        public static readonly PropertyData MinValueProperty = RegisterProperty<Gun, int>(model => model.MinGunValue, 0);

        public int MinGunValue
        {
            get { return GetValue<int>(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        /// <summary>Register the MaxGunValue property so it is known in the class.</summary>
        public static readonly PropertyData MaxValueProperty = RegisterProperty<Gun, int>(model => model.MaxGunValue, 100);

        public int MaxGunValue
        {
            get { return GetValue<int>(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        /// <summary>Register the DeltaMinor property so it is known in the class.</summary>
        public static readonly PropertyData DeltaMinorProperty = RegisterProperty<Gun, decimal>(model => model.DeltaMinor, 0);

        public decimal DeltaMinor
        {
            get { return GetValue<decimal>(DeltaMinorProperty); }
            set { SetValue(DeltaMinorProperty, value); }
        }

        /// <summary>Register the DecimalPlaces property so it is known in the class.</summary>
        public static readonly PropertyData DecimalPlacesProperty = RegisterProperty<Gun, int>(model => model.DecimalPlaces, 0);

        public int DecimalPlaces
        {
            get { return GetValue<int>(DecimalPlacesProperty); }
            set { SetValue(DecimalPlacesProperty, value); }
        }
        #endregion
    }
}