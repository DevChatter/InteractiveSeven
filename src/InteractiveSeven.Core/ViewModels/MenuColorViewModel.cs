using InteractiveSeven.Core.Events;
using InteractiveSeven.Core.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Drawing;

namespace InteractiveSeven.Core.ViewModels
{
    public class MenuColorViewModel : INotifyPropertyChanged
    {
        public MenuColorViewModel()
        {
            DomainEvents.Register<MenuColorChanging>(HandleMenuColorChanging);
        }

        private Color topLeft = Color.FromArgb(0, 88, 176);
        private Color botLeft = Color.FromArgb(0, 0, 80);
        private Color topRight = Color.FromArgb(0, 0, 128);
        private Color botRight = Color.FromArgb(0, 0, 32);

        public Color TopLeft
        {
            get => topLeft;
            set
            {
                topLeft = value;
                OnPropertyChanged();
                OnPropertyChanged("MidPoint");
                OnPropertyChanged("PreviewImage");
            }
        }
        public Color BotLeft
        {
            get => botLeft;
            set
            {
                botLeft = value;
                OnPropertyChanged();
                OnPropertyChanged("MidPoint");
                OnPropertyChanged("PreviewImage");
            }
        }
        public Color TopRight
        {
            get => topRight;
            set
            {
                topRight = value;
                OnPropertyChanged();
                OnPropertyChanged("MidPoint");
                OnPropertyChanged("PreviewImage");
            }
        }
        public Color BotRight
        {
            get => botRight;
            set
            {
                botRight = value;
                OnPropertyChanged();
                OnPropertyChanged("MidPoint");
                OnPropertyChanged("PreviewImage");
            }
        }
        public MenuColors PreviewImage
        {
            get => new MenuColors { TopLeft = TopLeft, TopRight = TopRight, BotLeft = BotLeft, BotRight = BotRight };
        }

        private void HandleMenuColorChanging(MenuColorChanging obj)
        {
            TopLeft = obj.MenuColors.TopLeft;
            TopRight = obj.MenuColors.TopRight;
            BotLeft = obj.MenuColors.BotLeft;
            BotRight = obj.MenuColors.BotRight;
        }

        public ObservableCollection<ChangeRecord> Changes { get; } = new ObservableCollection<ChangeRecord>();

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
