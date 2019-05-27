using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Linq;
using System.Windows.Media.Imaging;

namespace InteractiveSeven.ViewModels
{
    public class MenuColorViewModel : INotifyPropertyChanged
    {
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
        public Color MidPoint
        {
            get => Color.FromRgb((byte)AverageRed(), (byte)AverageGreen(), (byte)AverageBlue());
        }

        public ImageSource PreviewImage
        {
            get => ColorPreview.MakeBmp(TopLeft.ToOther(), TopRight.ToOther(), BotRight.ToOther(), BotLeft.ToOther());
        }

        private int AverageRed() => (int)(new int[] { topLeft.R, topRight.R, botLeft.R, botRight.R }).Average();
        private int AverageGreen() => (int)(new int[] { topLeft.G, topRight.G, botLeft.G, botRight.G }).Average();
        private int AverageBlue() => (int)(new int[] { topLeft.B, topRight.B, botLeft.B, botRight.B }).Average();

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
    }
}
