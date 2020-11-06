using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfTools.Converters.TextBox
{
    public class UIntHexStringConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as uint?)?.ToString("X");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var s = value as string;
            if (s != null && s.Length <= 4)
            {
                uint result;
                if (uint.TryParse(s, NumberStyles.HexNumber, null, out result))
                    return result;
            }
            return null;
        }
    }
}