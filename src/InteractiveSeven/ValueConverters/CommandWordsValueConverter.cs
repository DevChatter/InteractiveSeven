using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace InteractiveSeven.ValueConverters
{
    public class CommandWordsValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string[] words)
            {
                return string.Join(",", words);
            }
            return "None";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string text)
            {
                return text
                    .Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .ToArray();
            }
            return new string[0];
        }
    }
}