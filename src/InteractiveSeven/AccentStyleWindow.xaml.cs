using InteractiveSeven.Core.ViewModels;
using InteractiveSeven.Core.Windows;

namespace InteractiveSeven
{
    public partial class AccentStyleWindow : IAccentStyleWindow
    {
        public AccentStyleWindow(ThemeViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
