using InteractiveSeven.Core.ViewModels;
using MahApps.Metro.Controls;

namespace InteractiveSeven
{
    /// <summary>
    /// Interaction logic for AccentStyleWindow.xaml
    /// </summary>
    public partial class AccentStyleWindow : MetroWindow
    {
        public AccentStyleWindow(ThemeViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
