using InteractiveSeven.Core.Services;
using System.Windows;

namespace InteractiveSeven.Services
{
    public class DialogService : IDialogService
    {
        public bool ConfirmDialog(string message) =>
            MessageBox.Show(message, "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question)
            == MessageBoxResult.Yes;
    }
}