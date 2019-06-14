using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace InteractiveSeven.Core.Settings
{
    public class CommandSettings : ObservableSettingsBase
    {
        private string[] _costsCommandWords = { "Costs", "Cost", "Price", "Prices"};
        private string[] _balanceCommandWords = { "Balance", "Gil" };
        private string[] _giveGilCommandWords = { "GiveGil", "Give" };
        private string[] _i7CommandWords = { "i7", "Interactive7", "Interactive" };
        private string[] _menuCommandWords = { "Menu", "MenuColor", "Window", "Windows" };
        private string[] _nameBidsCommandWords = { "NameBids" };
        private string[] _refreshCommandWords = { "Refresh" };
        private List<(string Name, Func<string[]> Words)> AllWordSets { get; }

        public CommandSettings()
        {
            AllWordSets = new List<(string,Func<string[]>)>
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
    }
}