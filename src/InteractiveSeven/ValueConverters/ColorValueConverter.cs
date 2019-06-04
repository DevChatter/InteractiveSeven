using System;
using System.Globalization;
using System.Windows.Data;

namespace InteractiveSeven.ValueConverters
{
    public class ColorValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is System.Drawing.Color drawColor)
            {
                return System.Windows.Media.Color.FromRgb(drawColor.R, drawColor.G, drawColor.B);
            }
            return System.Windows.Media.Color.FromRgb(0, 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is System.Windows.Media.Color mediaColor)
            {
                return System.Drawing.Color.FromArgb(mediaColor.R, mediaColor.G, mediaColor.B);
            }
            return System.Drawing.Color.FromArgb(0, 0, 0);
        }
    }
}
