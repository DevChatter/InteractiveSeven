using InteractiveSeven.Core.Events;
using InteractiveSeven.Core.Models;
using ReactiveUI;
using System.Drawing;
using System.Reactive;

namespace InteractiveSeven.UI.ViewModels
{
    public class MenuControlViewModel : ReactiveObject
    {
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

        public ReactiveCommand<Unit, Unit> RefreshColorsCmd { get; }
        public ReactiveCommand<Unit, Unit> SetColorsCmd { get; }

        public MenuControlViewModel()
        {
            DomainEvents.Register<MenuColorChanging>(de => SetColors(de.MenuColors));
            RefreshColorsCmd = ReactiveCommand.Create(() => {});
            SetColorsCmd = ReactiveCommand.Create(() => { });
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