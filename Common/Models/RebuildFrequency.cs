
namespace Common.Models
{
    using Catel.Data;
    using Interfaces;

    public class RebuildFrequency: ModelBase, IRebuildFrequency
    {
        #region Implementation of IRebuildFrequency

        /// <summary>Register the IsManual property so it is known in the class.</summary>
        public static readonly PropertyData IsManualProperty = RegisterProperty<RebuildFrequency, bool>(model => model.IsManual);

        public bool IsManual
        {
            get { return GetValue<bool>(IsManualProperty); }
            set { SetValue(IsManualProperty, value); }
        }

        /// <summary>Register the IsAuto property so it is known in the class.</summary>
        public static readonly PropertyData IsAutoProperty = RegisterProperty<RebuildFrequency, bool>(model => model.IsAuto);

        public bool IsAuto
        {
            get { return GetValue<bool>(IsAutoProperty); }
            set { SetValue(IsAutoProperty, value); }
        }

        /// <summary>Register the IsStop property so it is known in the class.</summary>
        public static readonly PropertyData IsStopProperty = RegisterProperty<RebuildFrequency, bool>(model => model.IsStop);

        public bool IsStop
        {
            get { return GetValue<bool>(IsStopProperty); }
            set { SetValue(IsStopProperty, value); }
        }

        #endregion
    }
}