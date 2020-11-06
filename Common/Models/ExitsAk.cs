
namespace Common.Models
{
    using Catel.Data;
    using Interfaces;

    public class ExitsAk : ModelBase, IExitsAk
    {
        public ExitsAk()
        {
        }

        #region Implementation of IExitsAk

        /// <summary>Register the IsCheckedExit1 property so it is known in the class.</summary>
        public static readonly PropertyData IsCheckedExit1Property = RegisterProperty<ExitsAk, bool>(model => model.IsCheckedExit1);

        public bool IsCheckedExit1
        {
            get { return GetValue<bool>(IsCheckedExit1Property); }
            set { SetValue(IsCheckedExit1Property, value); }
        }

        /// <summary>Register the IsCheckedExit2 property so it is known in the class.</summary>
        public static readonly PropertyData IsCheckedExit2Property = RegisterProperty<ExitsAk, bool>(model => model.IsCheckedExit2);

        public bool IsCheckedExit2
        {
            get { return GetValue<bool>(IsCheckedExit2Property); }
            set { SetValue(IsCheckedExit2Property, value); }
        }

        /// <summary>Register the IsHighThresholdExit1 property so it is known in the class.</summary>
        public static readonly PropertyData HighThresholdExit1Property = RegisterProperty<ExitsAk, bool>(model => model.IsHighThresholdExit1);

        public bool IsHighThresholdExit1
        {
            get { return GetValue<bool>(HighThresholdExit1Property); }
            set { SetValue(HighThresholdExit1Property, value); }
        }

        /// <summary>Register the IsLowThresholdExit1 property so it is known in the class.</summary>
        public static readonly PropertyData LowThresholdExit1Property = RegisterProperty<ExitsAk, bool>(model => model.IsLowThresholdExit1);

        public bool IsLowThresholdExit1
        {
            get { return GetValue<bool>(LowThresholdExit1Property); }
            set { SetValue(LowThresholdExit1Property, value); }
        }

        /// <summary>Register the IsHighThresholdExit2 property so it is known in the class.</summary>
        public static readonly PropertyData HighThresholdExit2Property = RegisterProperty<ExitsAk, bool>(model => model.IsHighThresholdExit2);

        public bool IsHighThresholdExit2
        {
            get { return GetValue<bool>(HighThresholdExit2Property); }
            set { SetValue(HighThresholdExit2Property, value); }
        }

        /// <summary>Register the IsLowThresholdExit2 property so it is known in the class.</summary>
        public static readonly PropertyData LowThresholdExit2Property = RegisterProperty<ExitsAk, bool>(model => model.IsLowThresholdExit2);

        public bool IsLowThresholdExit2
        {
            get { return GetValue<bool>(LowThresholdExit2Property); }
            set { SetValue(LowThresholdExit2Property, value); }
        }

        /// <summary>Register the IsOnAk1 property so it is known in the class.</summary>
        public static readonly PropertyData IsOnAk1Property = RegisterProperty<ExitsAk, bool>(model => model.IsOnAk1);

        public bool IsOnAk1
        {
            get { return GetValue<bool>(IsOnAk1Property); }
            set { SetValue(IsOnAk1Property, value); }
        }

        /// <summary>Register the IsOnAk2 property so it is known in the class.</summary>
        public static readonly PropertyData IsOnAk2Property = RegisterProperty<ExitsAk, bool>(model => model.IsOnAk2);

        public bool IsOnAk2
        {
            get { return GetValue<bool>(IsOnAk2Property); }
            set { SetValue(IsOnAk2Property, value); }
        }

        #endregion
    }
}