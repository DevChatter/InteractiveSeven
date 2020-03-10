using InteractiveSeven.Core.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InteractiveSeven.Core.Settings
{
    public class MateriaSettings : ObservableSettingsBase
    {
        private bool _enabled = true;
        private bool _allowMod = true;

        public MateriaSettings()
        {
            AllMateria = Materia.All.Select(x => new SpecificMateriaSettings(x, true)).ToList();
        }

        public bool Enabled
        {
            get => _enabled;
            set
            {
                _enabled = value;
                OnPropertyChanged();
            }
        }

        public bool AllowMod // TODO: Add to Settings Screen
        {
            get => _allowMod;
            set
            {
                _allowMod = value;
                OnPropertyChanged();
            }
        }

        public List<SpecificMateriaSettings> AllMateria { get; set; }

        public List<SpecificMateriaSettings> AllByName(string name)
            => AllMateria.Where(x => x.IsMatchByName(name)).ToList();
    }
}