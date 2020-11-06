
using System.Collections.Generic;

namespace Common.Models
{
    using Interfaces;
    using Catel.Data;

    public class Packet : ModelBase, IPacket
    {
        public Packet():this(0, 0)
        {
        }

        public Packet(uint address, uint data)
        {
            Address = address;
            Data = data;
        }

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public uint Address
        {
            get { return GetValue<uint>(AddressProperty); }
            set { SetValue(AddressProperty, value); }
        }

        /// <summary>
        /// Register the Address property so it is known in the class.
        /// </summary>
        public static readonly PropertyData AddressProperty = RegisterProperty("Address", typeof(uint), null);

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public uint Data
        {
            get { return GetValue<uint>(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        /// <summary>
        /// Register the Data property so it is known in the class.
        /// </summary>
        public static readonly PropertyData DataProperty = RegisterProperty("Data", typeof(uint), null);


        //protected override void ValidateFields(List<IFieldValidationResult> validationResults)
        //{
        //    if (Address > ushort.MaxValue)
        //    {
        //        validationResults.Add(FieldValidationResult.CreateError(AddressProperty, "Превышен размер адреса"));
        //    }

        //    if (Data > byte.MaxValue)
        //    {
        //        validationResults.Add(FieldValidationResult.CreateError(DataProperty, "Превышен размер данных"));
        //    }
        //}
    }
}