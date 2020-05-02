using System;
using System.Globalization;
using System.Windows.Data;

namespace InteractiveSeven.ValueConverters
{
    [ValueConversion(typeof(bool), typeof(string))]
    public sealed class BoolToOnOffConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (!(value is bool))
                return null;
            return (bool)value ? "On" : "Off";
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}