using System;
using System.Globalization;
using WpfCustomControlLibraryVS2015.Converters.Base;
//using WpfToolsLib.Converters.Base;

namespace WpfCustomControlLibraryVS2015.Converters
{
    public class LedStateToBoolConvertor : ConvertorBase<LedStateToBoolConvertor>
    {
        #region Overrides of ConvertorBase<LedStateToBoolConvertor>

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
                return (bool)value ? TypeStateColorEnum.On : TypeStateColorEnum.Off;
            return TypeStateColorEnum.Disabled;
        }

        #endregion
    }
}