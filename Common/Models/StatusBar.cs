

namespace Common.Models
{
    using System.Collections.Generic;
    using Catel.Data;
    using Interfaces;

    public class StatusBar: ModelBase,IStatusBar
    {
        #region Implementation of IStatusBar

        /// <summary>Register the CurrentLpt property so it is known in the class.</summary>
        public static readonly PropertyData CurrentLptProperty = RegisterProperty<StatusBar, int>(model => model.CurrentLpt);

        public int CurrentLpt
        {
            get { return GetValue<int>(CurrentLptProperty); }
            set { SetValue(CurrentLptProperty, value); }
        }

        /// <summary>Register the LptAddressList property so it is known in the class.</summary>
        public static readonly PropertyData LptListProperty = RegisterProperty<StatusBar, IEnumerable<int>>(model => model.LptAddressList);

        public IEnumerable<int> LptAddressList
        {
            get { return GetValue<IEnumerable<int>>(LptListProperty); }
            set { SetValue(LptListProperty, value); }
        }

        /// <summary>Register the IsKnp property so it is known in the class.</summary>
        public static readonly PropertyData IsKnpProperty = RegisterProperty<StatusBar, bool>(model => model.IsKnp);

        public bool IsKnp
        {
            get { return GetValue<bool>(IsKnpProperty); }
            set { SetValue(IsKnpProperty, value); }
        }

        /// <summary>Register the IsLinkOn property so it is known in the class.</summary>
        public static readonly PropertyData IsLinkOnProperty = RegisterProperty<StatusBar, bool>(model => model.IsLinkOn);

        public bool IsLinkOn
        {
            get { return GetValue<bool>(IsLinkOnProperty); }
            set { SetValue(IsLinkOnProperty, value); }
        }

        #endregion
    }
}