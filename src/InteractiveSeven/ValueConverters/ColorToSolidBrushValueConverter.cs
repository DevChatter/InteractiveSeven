using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace InteractiveSeven.ValueConverters
{
    public class ColorToSolidBrushValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is System.Drawing.Color drawColor)
            {
                return new SolidColorBrush(Color.FromRgb(drawColor.R, drawColor.G, drawColor.B));
            }
            return new SolidColorBrush(Color.FromRgb(0, 0, 0));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}