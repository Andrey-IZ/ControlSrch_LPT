
namespace Common.Models
{
    using Catel.Data;
    using Interfaces;

    public class ControlFapch : ModelBase, IControlFapch
    {
        #region Implementation of IControlFapch

        /// <summary>Register the IsDivisorInputSignal property so it is known in the class.</summary>
        public static readonly PropertyData IsDivisorInputSignalProperty = RegisterProperty<ControlFapch, bool>(model => model.IsDivisorInputSignal);

        public bool IsDivisorInputSignal
        {
            get { return GetValue<bool>(IsDivisorInputSignalProperty); }
            set { SetValue(IsDivisorInputSignalProperty, value); }
        }

        /// <summary>Register the IsDivisorSupportingFreq property so it is known in the class.</summary>
        public static readonly PropertyData IsDivisorSupportingFreqProperty = RegisterProperty<ControlFapch, bool>(model => model.IsDivisorSupportingFreq);

        public bool IsDivisorSupportingFreq
        {
            get { return GetValue<bool>(IsDivisorSupportingFreqProperty); }
            set { SetValue(IsDivisorSupportingFreqProperty, value); }
        }

        /// <summary>Register the IsPhaseDetectorAnalog property so it is known in the class.</summary>
        public static readonly PropertyData IsPhaseDetectorAnalogProperty = RegisterProperty<ControlFapch, bool>(model => model.IsPhaseDetectorAnalog);

        public bool IsPhaseDetectorAnalog
        {
            get { return GetValue<bool>(IsPhaseDetectorAnalogProperty); }
            set { SetValue(IsPhaseDetectorAnalogProperty, value); }
        }

        /// <summary>Register the IsPhaseDetectorDigital property so it is known in the class.</summary>
        public static readonly PropertyData IsPhaseDetectorDigitalProperty = RegisterProperty<ControlFapch, bool>(model => model.IsPhaseDetectorDigital);

        public bool IsPhaseDetectorDigital
        {
            get { return GetValue<bool>(IsPhaseDetectorDigitalProperty); }
            set { SetValue(IsPhaseDetectorDigitalProperty, value); }
        }

        #endregion
    }
}