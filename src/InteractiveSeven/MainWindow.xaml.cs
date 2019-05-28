using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.ViewModels;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Navigation;

namespace InteractiveSeven
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MenuColorViewModel menuColorViewModel = new MenuColorViewModel();

        public MainWindow()
        {
            InitializeComponent();
            MenuColorGrid.DataContext = menuColorViewModel;
            MenuColorGroup.DataContext = menuColorViewModel;
            SettingsTab.DataContext = ApplicationSettings.Instance;
        }

        private void PatreonLink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = e.Uri.AbsoluteUri,
                UseShellExecute = true
            };
            Process.Start(psi);
            e.Handled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            menuColorViewModel.Changes.Insert(0, new ChangeRecord
            {
                Changer = $"Brendoneus ({new Random().Next(100, 200)})",
                Change = $"#FF0000 #00FF00 #FF0000 #000000"
            });
        }
    }

    public class ColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color color)
            {
                return new SolidColorBrush(color);
            }
            throw new ArgumentException($"Argument must be a {typeof(Color).FullName}", nameof(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
