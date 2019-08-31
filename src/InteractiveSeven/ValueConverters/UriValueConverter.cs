using System;
using System.Globalization;
using System.Windows.Data;

namespace InteractiveSeven.ValueConverters
{
    public class UriValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
            {
                return new Uri(str);
            }

            throw new ArgumentException("Value must be a string.", nameof(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Uri uri)
            {
                return uri.AbsoluteUri;
            }

            throw new ArgumentException("Value must be a Uri.", nameof(value));
        }
    }
}