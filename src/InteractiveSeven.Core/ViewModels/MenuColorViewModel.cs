using InteractiveSeven.Core.Events;
using InteractiveSeven.Core.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Drawing;
using System;
using InteractiveSeven.Core.Memory;
using InteractiveSeven.Core.Settings;

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
