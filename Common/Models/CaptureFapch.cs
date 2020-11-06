
namespace Common.Models
{
    using Catel.Data;
    using Interfaces;

    public class CaptureFapch:ModelBase, ICaptureFapch
    {

        #region Implementation of ICaptureFapch

        /// <summary>Register the IsCaptureFapch1 property so it is known in the class.</summary>
        public static readonly PropertyData IsCaptureFapch1Property = RegisterProperty<CaptureFapch, bool>(model => model.IsCaptureFapch1);

        public bool IsCaptureFapch1
        {
            get { return GetValue<bool>(IsCaptureFapch1Property); }
            set { SetValue(IsCaptureFapch1Property, value); }
        }

        /// <summary>Register the IsCaptureFapch2 property so it is known in the class.</summary>
        public static readonly PropertyData IsCaptureFapch2Property = RegisterProperty<CaptureFapch, bool>(model => model.IsCaptureFapch2);

        public bool IsCaptureFapch2
        {
            get { return GetValue<bool>(IsCaptureFapch2Property); }
            set { SetValue(IsCaptureFapch2Property, value); }
        }

        #endregion
    }
}