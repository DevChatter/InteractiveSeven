using System.Windows;
using InteractiveSeven.Core;

namespace InteractiveSeven
{
    public class DialogService : IDialogService
    {
        public bool ConfirmDialog(string message) =>
            MessageBox.Show(message, "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question)
            == MessageBoxResult.Yes;
    }
}