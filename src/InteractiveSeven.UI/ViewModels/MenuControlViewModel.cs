using InteractiveSeven.Core.Models;
using System.Drawing;

namespace InteractiveSeven.UI.ViewModels
{
    public class MenuControlViewModel : ViewModelBase
    {
        private Color _topLeftColor = Color.FromArgb(0,88,176);
        public Color TopLeftColor
        {
            get => _topLeftColor;
            set
            {
                _topLeftColor = value;
                NotifyPropertyChanged();
            }
        }

        private Color _topRightColor = Color.FromArgb(0, 0, 80);
        public Color TopRightColor
        {
            get => _topRightColor;
            set
            {
                _topRightColor = value;
                NotifyPropertyChanged();
            }
        }

        private Color _bottomLeftColor = Color.FromArgb(0, 0, 128);
        public Color BottomLeftColor
        {
            get => _bottomLeftColor;
            set
            {
                _bottomLeftColor = value;
                NotifyPropertyChanged();
            }
        }

        private Color _bottomRightColor = Color.FromArgb(0, 0, 32);
        public Color BottomRightColor
        {
            get => _bottomRightColor;
            set
            {
                _bottomRightColor = value;
                NotifyPropertyChanged();
            }
        }

        public void SetColors(MenuColors menuColors)
        {
            TopLeftColor = menuColors.TopLeft;
            TopRightColor = menuColors.TopRight;
            BottomRightColor = menuColors.BotRight;
            BottomLeftColor = menuColors.BotLeft;
        }
    }
}