using InteractiveSeven.Core.ViewModels;
using InteractiveSeven.Core.Windows;

namespace DevChatter.InteractiveGames.Core.Seven
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
