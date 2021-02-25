using ControlzEx.Theming;
using MahApps.Metro;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using XamlColorSchemeGenerator;

namespace InteractiveSeven
{
    /// <summary>
    /// Interaction logic for AccentStyleWindow.xaml
    /// </summary>
    public partial class AccentStyleWindow : MetroWindow
    {
        public static readonly DependencyProperty ColorsProperty
            = DependencyProperty.Register("Colors",
                typeof(List<KeyValuePair<string, Color>>),
                typeof(AccentStyleWindow),
                new PropertyMetadata(default(List<KeyValuePair<string, Color>>)));

        public List<KeyValuePair<string, Color>> Colors
        {
            get => (List<KeyValuePair<string, Color>>)GetValue(ColorsProperty);
            set => SetValue(ColorsProperty, value);
        }

        public AccentStyleWindow()
        {
            InitializeComponent();

            this.DataContext = this;

            this.Colors = typeof(Colors)
                .GetProperties()
                .Where(prop => typeof(Color).IsAssignableFrom(prop.PropertyType))
                .Select(prop => new KeyValuePair<String, Color>(prop.Name, (Color)prop.GetValue(null)))
                .ToList();

            var theme = ThemeManager.Current.DetectTheme(Application.Current);
            ThemeManager.Current.ChangeTheme(this, theme);
        }

        private void ChangeAppBaseColorButtonClick(object sender, RoutedEventArgs e)
        {
            ThemeManager.Current.ChangeThemeBaseColor(Application.Current, ((Button)sender).Content.ToString());
            Application.Current?.MainWindow?.Activate();
        }

        private void ChangeAppAccentButtonClick(object sender, RoutedEventArgs e)
        {
            ThemeManager.Current.ChangeThemeColorScheme(Application.Current, ((Button)sender).Content.ToString());
            Application.Current?.MainWindow?.Activate();
        }

        private void ChangeAppThemeButtonClick(object sender, RoutedEventArgs e)
        {
            var expectedTheme = ThemeManager.Current.GetTheme(((Button)sender).Content.ToString());
            ThemeManager.Current.ChangeTheme(Application.Current, expectedTheme);
        }

        private void ColorsSelectorOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ColorsSelector.SelectedItem is KeyValuePair<string, Color> selectedColor)
            {
                var currentTheme = ThemeManager.Current.DetectTheme(Application.Current);
                var theme = RuntimeThemeGenerator.Current.GenerateRuntimeTheme(currentTheme.BaseColorScheme, selectedColor.Value);
                ThemeManager.Current.ChangeTheme(Application.Current, theme);
                Application.Current?.MainWindow?.Activate();
            }
        }
    }
}
