using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace InteractiveSeven.ValueConverters
{
    public class ConnectionColorConverter : IValueConverter
    {
        public Color ConnectedColor { get; set; } = Color.FromRgb(32, 128, 32);
        public Color DisconnectedColor { get; set; } = Color.FromRgb(192, 32, 32);
        public Color NotConfiguredColor { get; set; } = Color.FromRgb(132, 132, 32);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isConnected)
            {
                if (isConnected)
                {
                    return ConnectedColor;
                }
                return DisconnectedColor;
            }
            return NotConfiguredColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value?.ToString()?.ToLower())
            {
                case "connected":
                    return true;
                case "disconnected":
                    return false;
            }
            return false;
        }
    }
}
