using System.Windows;
using InteractiveSeven.Core.Services;

namespace InteractiveSeven.Services
{
    public class DialogService : IDialogService
    {
        public bool ConfirmDialog(string message) =>
            MessageBox.Show(message, "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question)
            == MessageBoxResult.Yes;
    }
}