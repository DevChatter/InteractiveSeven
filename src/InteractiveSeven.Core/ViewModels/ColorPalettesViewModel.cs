using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InteractiveSeven.Core.ViewModels
{
    public class ColorPalettesViewModel : INotifyPropertyChanged
    {
        public ColorPalettesViewModel()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
