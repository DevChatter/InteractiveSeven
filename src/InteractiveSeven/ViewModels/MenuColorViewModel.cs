using InteractiveSeven.Core.Events;
using InteractiveSeven.Core.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace InteractiveSeven.ViewModels
{
    public class MenuColorViewModel : INotifyPropertyChanged
    {
        public MenuColorViewModel()
        {
            DomainEvents.Register<MenuColorChanging>(HandleMenuColorChanging);
        }

        private Color topLeft = Color.FromRgb(0, 88, 176);
        private Color botLeft = Color.FromRgb(0, 0, 80);
        private Color topRight = Color.FromRgb(0, 0, 128);
        private Color botRight = Color.FromRgb(0, 0, 32);

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
        public ImageSource PreviewImage
        {
            get => ColorPreview.MakeBmp(TopLeft.ToOther(), TopRight.ToOther(), BotRight.ToOther(), BotLeft.ToOther());
        }

        private void HandleMenuColorChanging(MenuColorChanging obj)
        {
            TopLeft = obj.MenuColors.TopLeft.ToOther();
            TopRight = obj.MenuColors.TopRight.ToOther();
            BotLeft = obj.MenuColors.BotLeft.ToOther();
            BotRight = obj.MenuColors.BotRight.ToOther();
        }

        public ObservableCollection<ChangeRecord> Changes { get; } = new ObservableCollection<ChangeRecord>();

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public static class ColorExtension
    {
        public static System.Drawing.Color ToOther(this Color color)
        {
            return System.Drawing.Color.FromArgb(color.R, color.G, color.B);
        }
        public static Color ToOther(this System.Drawing.Color color)
        {
            return Color.FromRgb(color.R, color.G, color.B);
        }
    }
}
