using InteractiveSeven.Core.Models;
using System;
using System.Globalization;
using System.Windows.Data;

namespace InteractiveSeven.ValueConverters
{
    public class MenuColorsToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is MenuColors colors)
            {
                return ColorPreview.MakeBmp(colors.TopLeft, colors.TopRight, colors.BotRight, colors.BotLeft);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
