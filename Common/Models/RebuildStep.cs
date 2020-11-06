
namespace Common.Models
{
    using Catel.Data;
    using Interfaces;

    public class RebuildStep: ModelBase, IRebuildStep
    {
        public RebuildStep()
        {
        }
        #region Implementation of IRebuildStep

        /// <summary>Register the CurrentStepValue property so it is known in the class.</summary>
        public static readonly PropertyData CurrentStepValueProperty = RegisterProperty<RebuildStep, decimal>(model => model.CurrentStepValue, default(decimal), (s, e) => s.OnCurrentStepValueChanged(e));

        public decimal CurrentStepValue
        {
            get { return GetValue<decimal>(CurrentStepValueProperty); }
            set { SetValue(CurrentStepValueProperty, value); }
        }

        /// <summary>Occurs when the indexValue of the CurrentStepValue property is changed.</summary>
        /// <param name="e"></param>
        private void OnCurrentStepValueChanged(AdvancedPropertyChangedEventArgs e)
        {
        }

        /// <summary>Register the MinStepValue property so it is known in the class.</summary>
        public static readonly PropertyData MinStepValueProperty = RegisterProperty<RebuildStep, decimal>(model => model.MinStepValue);

        public decimal MinStepValue
        {
            get { return GetValue<decimal>(MinStepValueProperty); }
            set { SetValue(MinStepValueProperty, value); }
        }

        /// <summary>Register the MaxStepValue property so it is known in the class.</summary>
        public static readonly PropertyData MaxStepValueProperty = RegisterProperty<RebuildStep, decimal>(model => model.MaxStepValue);

        public decimal MaxStepValue
        {
            get { return GetValue<decimal>(MaxStepValueProperty); }
            set { SetValue(MaxStepValueProperty, value); }
        }

        /// <summary>Register the MinorDelta property so it is known in the class.</summary>
        public static readonly PropertyData MinorDeltaProperty = RegisterProperty<RebuildStep, decimal>(model => model.MinorDelta, 1);

        public decimal MinorDelta
        {
            get { return GetValue<int>(MinorDeltaProperty); }
            set { SetValue(MinorDeltaProperty, value); }
        }

        #endregion
    }
}