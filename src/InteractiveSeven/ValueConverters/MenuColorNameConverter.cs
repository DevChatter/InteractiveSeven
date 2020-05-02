using System;
using System.Globalization;
using System.Windows.Data;

namespace InteractiveSeven.ValueConverters
{
    public class MenuColorNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is System.Drawing.Color color)
            {
                string colorName = color.IsNamedColor ? color.Name : $"#{color.Name[2..]}";
                return $"{colorName} | R:{color.R} G:{color.G} B:{color.B}";
            }
            return "Unexpected Binding";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}