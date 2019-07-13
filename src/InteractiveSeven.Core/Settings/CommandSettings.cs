using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace InteractiveSeven.Core.Settings
{
    public class CommandSettings : ObservableSettingsBase
    {
        private string[] _costsCommandWords = { "Costs", "Cost", "Price", "Prices" };
        private string[] _balanceCommandWords = { "Balance", "Gil" };
        private string[] _giveGilCommandWords = { "GiveGil", "Give" };
        private string[] _i7CommandWords = { "i7", "Interactive7", "Interactive" };
        private string[] _menuCommandWords = { "Menu", "MenuColor", "Window", "Windows" };
        private string[] _nameBidsCommandWords = { "NameBids" };
        private string[] _refreshCommandWords = { "Refresh" };
        private string[] _helpCommandWords = { "Help" };
        private string[] _paletteCommandWords = {"Palette", "Pallete", "Pallette", "Palete"};
        private string[] _rainbowCommandWords = {"Rainbow", "RainbowMode"};
        private string[] _makoCommandWords = {"Mako", "MakoMode"};

        private string[] _weaponCommandWords = { "Weapon", "Weap", "weapons" };
        private string[] _armletCommandWords = { "Armlet", "armor", "armlets" };
        private string[] _accessoryCommandWords = { "Accessory", "Accessories" };
        private string[] _pauperCommandWords = { "Pauper", "poor" };

        private string[] _cloudWords = { "cloud", "cluod", "clodu" };
        private string[] _barretWords = { "barret", "baret", "barett", "barrett" };
        private string[] _tifaWords = { "tifa", "tiaf", "tfia" };
        private string[] _aerisWords = { "aeris", "aerith" };
        private string[] _caitWords = { "caitsith", "cait" };
        private string[] _cidWords = { "cid" };
        private string[] _redWords = { "red", "redxiii", "nanaki", "redxii", "redxiiii", "red13" };
        private string[] _vincentWords = { "vincent", "vince" };
        private string[] _yuffieWords = { "yuffie" };

        [JsonIgnore]
        public List<(string Name, Func<string[]> Words)> AllWordSets { get; }

        public CommandSettings()
        {
            AllWordSets = new List<(string, Func<string[]>)>
            {
                (nameof(CostsCommandWords),
                    () => CostsCommandWords),
                (nameof(BalanceCommandWords),
                    () => BalanceCommandWords),
                (nameof(GiveGilCommandWords),
                    () => GiveGilCommandWords),
                (nameof(I7CommandWords),
                    () => I7CommandWords),
                (nameof(MenuCommandWords),
                    () => MenuCommandWords),
                (nameof(NameBidsCommandWords),
                    () => NameBidsCommandWords),
                (nameof(RefreshCommandWords),
                    () => RefreshCommandWords),
                (nameof(HelpCommandWords),
                    () => HelpCommandWords),
                (nameof(PaletteCommandWords),
                    () => PaletteCommandWords),
                (nameof(RainbowCommandWords),
                    () => RainbowCommandWords),
                (nameof(MakoCommandWords),
                    () => MakoCommandWords),

                (nameof(WeaponCommandWords),
                    () => WeaponCommandWords),
                (nameof(ArmletCommandWords),
                    () => ArmletCommandWords),
                (nameof(AccessoryCommandWords),
                    () => AccessoryCommandWords),
                (nameof(PauperCommandWords),
                    () => PauperCommandWords),

                (nameof(CloudCommandWords),
                    () => CloudCommandWords),
                (nameof(BarretCommandWords),
                    () => BarretCommandWords),
                (nameof(TifaCommandWords),
                    () => TifaCommandWords),
                (nameof(AerisCommandWords),
                    () => AerisCommandWords),
                (nameof(CaitCommandWords),
                    () => CaitCommandWords),
                (nameof(CidCommandWords),
                    () => CidCommandWords),
                (nameof(RedCommandWords),
                    () => RedCommandWords),
                (nameof(VincentCommandWords),
                    () => VincentCommandWords),
                (nameof(YuffieCommandWords),
                    () => YuffieCommandWords),
            };
        }

        private string[] RemoveAllDuplicates(IEnumerable<string> strings, [CallerMemberName] string propertyName = null)
        {
            foreach (Func<string[]> wordSet in AllWordSets.Where(x => x.Name != propertyName).Select(x => x.Words))
            {
                strings = strings.Except(wordSet());
            }

            return strings.ToArray();
        }

        public string[] CostsCommandWords
        {
            get => _costsCommandWords;
            set
            {
                _costsCommandWords = RemoveAllDuplicates(value);
                OnPropertyChanged();
            }
        }

        public string[] BalanceCommandWords
        {
            get => _balanceCommandWords;
            set
            {
                _balanceCommandWords = RemoveAllDuplicates(value);
                OnPropertyChanged();
            }
        }

        public string[] GiveGilCommandWords
        {
            get => _giveGilCommandWords;
            set
            {
                _giveGilCommandWords = RemoveAllDuplicates(value);
                OnPropertyChanged();
            }
        }

        public string[] I7CommandWords
        {
            get => _i7CommandWords;
            set
            {
                _i7CommandWords = RemoveAllDuplicates(value);
                OnPropertyChanged();
            }
        }

        public string[] MenuCommandWords
        {
            get => _menuCommandWords;
            set
            {
                _menuCommandWords = RemoveAllDuplicates(value);
                OnPropertyChanged();
            }
        }

        public string[] NameBidsCommandWords
        {
            get => _nameBidsCommandWords;
            set
            {
                _nameBidsCommandWords = RemoveAllDuplicates(value);
                OnPropertyChanged();
            }
        }

        public string[] RefreshCommandWords
        {
            get => _refreshCommandWords;
            set
            {
                _refreshCommandWords = RemoveAllDuplicates(value);
                OnPropertyChanged();
            }
        }

        public string[] HelpCommandWords
        {
            get => _helpCommandWords;
            set
            {
                _helpCommandWords = RemoveAllDuplicates(value);
                OnPropertyChanged();
            }
        }

        public string[] PaletteCommandWords // TODO: Add to Settings View
        {
            get => _paletteCommandWords;
            set
            {
                _paletteCommandWords = RemoveAllDuplicates(value);
                OnPropertyChanged();
            }
        }

        public string[] RainbowCommandWords // TODO: Add to Settings View
        {
            get => _rainbowCommandWords;
            set
            {
                _rainbowCommandWords = RemoveAllDuplicates(value);
                OnPropertyChanged();
            }
        }

        public string[] MakoCommandWords // TODO: Add to Settings View
        {
            get => _makoCommandWords;
            set
            {
                _makoCommandWords = RemoveAllDuplicates(value);
                OnPropertyChanged();
            }
        }

        public string[] WeaponCommandWords
        {
            get => _weaponCommandWords;
            set
            {
                _weaponCommandWords = RemoveAllDuplicates(value);
                OnPropertyChanged();
            }
        }

        public string[] ArmletCommandWords
        {
            get => _armletCommandWords;
            set
            {
                _armletCommandWords = RemoveAllDuplicates(value);
                OnPropertyChanged();
            }
        }

        public string[] AccessoryCommandWords
        {
            get => _accessoryCommandWords;
            set
            {
                _accessoryCommandWords = RemoveAllDuplicates(value);
                OnPropertyChanged();
            }
        }

        public string[] PauperCommandWords
        {
            get => _pauperCommandWords;
            set
            {
                _pauperCommandWords = RemoveAllDuplicates(value);
                OnPropertyChanged();
            }
        }

        public string[] CloudCommandWords
        {
            get => _cloudWords;
            set
            {
                _cloudWords = RemoveAllDuplicates(value);
                OnPropertyChanged();
            }
        }

        public string[] BarretCommandWords
        {
            get => _barretWords;
            set
            {
                _barretWords = RemoveAllDuplicates(value);
                OnPropertyChanged();
            }
        }

        public string[] TifaCommandWords
        {
            get => _tifaWords;
            set
            {
                _tifaWords = RemoveAllDuplicates(value);
                OnPropertyChanged();
            }
        }

        public string[] AerisCommandWords
        {
            get => _aerisWords;
            set
            {
                _aerisWords = RemoveAllDuplicates(value);
                OnPropertyChanged();
            }
        }

        public string[] CaitCommandWords
        {
            get => _caitWords;
            set
            {
                _caitWords = RemoveAllDuplicates(value);
                OnPropertyChanged();
            }
        }

        public string[] CidCommandWords
        {
            get => _cidWords;
            set
            {
                _cidWords = RemoveAllDuplicates(value);
                OnPropertyChanged();
            }
        }

        public string[] RedCommandWords
        {
            get => _redWords;
            set
            {
                _redWords = RemoveAllDuplicates(value);
                OnPropertyChanged();
            }
        }

        public string[] VincentCommandWords
        {
            get => _vincentWords;
            set
            {
                _vincentWords = RemoveAllDuplicates(value);
                OnPropertyChanged();
            }
        }

        public string[] YuffieCommandWords
        {
            get => _yuffieWords;
            set
            {
                _yuffieWords = RemoveAllDuplicates(value);
                OnPropertyChanged();
            }
        }
    }
}