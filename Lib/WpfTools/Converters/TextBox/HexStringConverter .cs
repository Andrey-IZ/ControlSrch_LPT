using System;
using System.Linq;
using System.Windows.Data;

namespace WpfTools.Converters.TextBox
{
    public class HexStringConverter : IValueConverter
    {
        private string _lastValidValue;
        public virtual object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string ret = null;

            var s = value as string;
            if (s != null)
            {
                var valueAsString = s;
                var parts = valueAsString.ToCharArray();
                var formatted = parts.Select((p, i) => (++i) % 2 == 0 ? String.Concat(p.ToString(), " ") : p.ToString());
                ret = String.Join(String.Empty, formatted).Trim();
            }

            return ret;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            object ret = null;
            if (value is string)
            {
                var valueAsString = ((string)value).Replace(" ", String.Empty).ToUpper();
                ret = _lastValidValue = IsHex(valueAsString) ? valueAsString : _lastValidValue;
            }

            return ret;
        }


        private bool IsHex(string text)
        {
            var reg = new System.Text.RegularExpressions.Regex("^[0-9A-Fa-f]*$");
            return reg.IsMatch(text);
        }
    }
}