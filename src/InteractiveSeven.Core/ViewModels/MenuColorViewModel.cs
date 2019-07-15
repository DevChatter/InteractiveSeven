using InteractiveSeven.Core.Events;
using InteractiveSeven.Core.Model;
using InteractiveSeven.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace InteractiveSeven.Core.ViewModels
{
    public class MenuColorViewModel : INotifyPropertyChanged
    {
        private readonly ILogger<MenuColorViewModel> _logger;

        public MenuColorViewModel(ILogger<MenuColorViewModel> logger)
        {
            _logger = logger;

            DomainEvents.Register<MenuColorChanging>(HandleMenuColorChanging);
            DomainEvents.Register<RefreshEvent>(HandleColorRefresh);
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
            try
            {
                TopLeft = obj.MenuColors.TopLeft;
                TopRight = obj.MenuColors.TopRight;
                BotLeft = obj.MenuColors.BotLeft;
                BotRight = obj.MenuColors.BotRight;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to change menu colors.");
            }
        }

        private void HandleColorRefresh(RefreshEvent e)
        {
            DomainEvents.Raise(new MenuColorChanging(PreviewImage, new ChatUser(), 0));
        }

        public ObservableCollection<ChangeRecord> Changes { get; } = new ObservableCollection<ChangeRecord>();

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
