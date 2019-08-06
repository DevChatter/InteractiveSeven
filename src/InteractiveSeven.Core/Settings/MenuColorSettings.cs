namespace InteractiveSeven.Core.Settings
{
    public class MenuColorSettings : ObservableSettingsBase
    {
        private bool _enabled = true;
        private int _bitCost;
        private bool _allowModOverride = true;
        private bool _transitionColors = true;
        private bool _enableRainbowCommand = true;
        private int _rainbowModeCost = 1000;
        private int _rainbowModeIterations = 30;
        private bool _enableMakoCommand = true;
        private int _makoModeCost = 500;
        private int _makoModeIterations = 30;
        private bool _enablePaletteCommand = true;
        private bool _allowModsToCreatePalettes = true;

        public bool Enabled
        {
            get => _enabled;
            set
            {
                _enabled = value;
                OnPropertyChanged();
            }
        }
        public int BitCost
        {
            get => _bitCost;
            set
            {
                _bitCost = value;
                OnPropertyChanged();
            }
        }
        public bool AllowModOverride
        {
            get => _allowModOverride;
            set
            {
                _allowModOverride = value;
                OnPropertyChanged();
            }
        }
        public bool TransitionColors
        {
            get => _transitionColors;
            set
            {
                _transitionColors = value;
                OnPropertyChanged();
            }
        }

        public bool EnableRainbowCommand
        {
            get => _enableRainbowCommand;
            set
            {
                _enableRainbowCommand = value;
                OnPropertyChanged();
            }
        }

        public int RainbowModeCost
        {
            get => _rainbowModeCost;
            set
            {
                _rainbowModeCost = value;
                OnPropertyChanged();
            }
        }

        public int RainbowModeIterations
        {
            get => _rainbowModeIterations;
            set
            {
                _rainbowModeIterations = value;
                OnPropertyChanged();
            }
        }

        public bool EnableMakoCommand
        {
            get => _enableMakoCommand;
            set
            {
                _enableMakoCommand = value;
                OnPropertyChanged();
            }
        }

        public int MakoModeCost
        {
            get => _makoModeCost;
            set
            {
                _makoModeCost = value;
                OnPropertyChanged();
            }
        }

        public int MakoModeIterations
        {
            get => _makoModeIterations;
            set
            {
                _makoModeIterations = value;
                OnPropertyChanged();
            }
        }
        public bool EnablePaletteCommand
        {
            get => _enablePaletteCommand;
            set
            {
                _enablePaletteCommand = value;
                OnPropertyChanged();
            }
        }
        public bool AllowModsToCreatePalettes
        {
            get => _allowModsToCreatePalettes;
            set
            {
                _allowModsToCreatePalettes = value;
                OnPropertyChanged();
            }
        }
    }
}