using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Settings;
using ReactiveUI;
using System.Drawing;

namespace InteractiveSeven.UI.ViewModels
{
    public class MenuControlViewModel : ReactiveObject
    {
        public bool EnableChatControl
        {
            get => ApplicationSettings.Instance.MenuSettings.Enabled;
            set => this.RaiseAndSetPropertyIfChanged(value,
                (s) => s.MenuSettings.Enabled, (s,v) => s.MenuSettings.Enabled = v);
        }

        public int BitCost
        {
            get => ApplicationSettings.Instance.MenuSettings.BitCost;
            set => this.RaiseAndSetPropertyIfChanged(value,
                s => s.MenuSettings.BitCost, (s,v) => s.MenuSettings.BitCost = v);
        }

        private Color _topLeftColor = Color.FromArgb(0, 88, 176);
        public Color TopLeftColor
        {
            get => _topLeftColor;
            set => this.RaiseAndSetIfChanged(ref _topLeftColor, value);
        }

        private Color _topRightColor = Color.FromArgb(0, 0, 80);
        public Color TopRightColor
        {
            get => _topRightColor;
            set => this.RaiseAndSetIfChanged(ref _topRightColor, value);
        }

        private Color _bottomLeftColor = Color.FromArgb(0, 0, 128);
        public Color BottomLeftColor
        {
            get => _bottomLeftColor;
            set => this.RaiseAndSetIfChanged(ref _bottomLeftColor, value);
        }

        private Color _bottomRightColor = Color.FromArgb(0, 0, 32);
        public Color BottomRightColor
        {
            get => _bottomRightColor;
            set => this.RaiseAndSetIfChanged(ref _bottomRightColor, value);
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