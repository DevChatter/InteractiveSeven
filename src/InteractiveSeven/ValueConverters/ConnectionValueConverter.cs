using System;
using System.Globalization;
using System.Windows.Data;

namespace InteractiveSeven.ValueConverters
{
    public class ConnectionValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isConnected)
            {
                if (isConnected)
                    return "Connected";
                else
                    return "Disconnected";
            }
            return "Disconnected";
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
