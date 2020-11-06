using System;
using System.Collections.Generic;
using Catel.Data;
using Common.Models.Interfaces;

namespace Common.Models
{
    public class DebugModel : ModelBase, IDebugModel
    {
        public DebugModel()
        {

        }

        #region Implementation of IDebugModel

        /// <summary>Register the LogPacketRecords property so it is known in the class.</summary>
        public static readonly PropertyData LogPacketRecordsProperty =
            RegisterProperty<DebugModel, IEnumerable<IPacketRecord>>(model => model.LogPacketRecords);

        public IEnumerable<IPacketRecord> LogPacketRecords
        {
            get { return GetValue<IEnumerable<IPacketRecord>>(LogPacketRecordsProperty); }
            set { SetValue(LogPacketRecordsProperty, value); }
        }

        /// <summary>Register the CurrentPacketRecord property so it is known in the class.</summary>
        public static readonly PropertyData CurrentPacketRecordProperty =
            RegisterProperty<DebugModel, IPacketRecord>(model => model.CurrentPacketRecord);

        public IPacketRecord CurrentPacketRecord
        {
            get { return GetValue<IPacketRecord>(CurrentPacketRecordProperty); }
            set { SetValue(CurrentPacketRecordProperty, value); }
        }

        #endregion
    }
}