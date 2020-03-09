using InteractiveSeven.Core.ViewModels;

namespace InteractiveSeven
{
    /// <summary>
    /// Interaction logic for DebugWindow.xaml
    /// </summary>
    public partial class DebugWindow
    {
        public DebugWindow(DebugWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            ViewModel = viewModel;
        }

        public DebugWindowViewModel ViewModel { get; }
    }
}
