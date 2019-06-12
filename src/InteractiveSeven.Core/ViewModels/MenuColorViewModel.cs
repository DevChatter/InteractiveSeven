using InteractiveSeven.Core.Events;
using InteractiveSeven.Core.Memory;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Settings;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace InteractiveSeven.Core.ViewModels
{
    public class MenuColorViewModel : INotifyPropertyChanged
    {
        private readonly IMenuColorAccessor _menuColorAccessor;
        private string ProcessName => ApplicationSettings.Instance.ProcessName;

        public MenuColorViewModel(IMenuColorAccessor menuColorAccessor)
        {
            _menuColorAccessor = menuColorAccessor;

            DomainEvents.Register<MenuColorChanging>(HandleMenuColorChanging);
            DomainEvents.Register<RefreshEvent>(HandleNameRefresh);
        }

        private Color _topLeft = Color.FromArgb(0, 88, 176);
        private Color _botLeft = Color.FromArgb(0, 0, 80);
        private Color _topRight = Color.FromArgb(0, 0, 128);
        private Color _botRight = Color.FromArgb(0, 0, 32);

        public Color TopLeft
        {
            get => _topLeft;
            set
            {
                _topLeft = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PreviewImage));
            }
        }
        public Color BotLeft
        {
            get => _botLeft;
            set
            {
                _botLeft = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PreviewImage));
            }
        }
        public Color TopRight
        {
            get => _topRight;
            set
            {
                _topRight = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PreviewImage));
            }
        }
        public Color BotRight
        {
            get => _botRight;
            set
            {
                _botRight = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PreviewImage));
            }
        }

        public MenuColors PreviewImage => new MenuColors { TopLeft = TopLeft, TopRight = TopRight, BotLeft = BotLeft, BotRight = BotRight };

        private void HandleMenuColorChanging(MenuColorChanging obj)
        {
            TopLeft = obj.MenuColors.TopLeft;
            TopRight = obj.MenuColors.TopRight;
            BotLeft = obj.MenuColors.BotLeft;
            BotRight = obj.MenuColors.BotRight;

            _menuColorAccessor.SetMenuColors(ProcessName, PreviewImage);
        }

        private void HandleNameRefresh(RefreshEvent e)
        {
            try
            {
                _menuColorAccessor.SetMenuColors(ProcessName, PreviewImage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public ObservableCollection<ChangeRecord> Changes { get; } = new ObservableCollection<ChangeRecord>();

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
