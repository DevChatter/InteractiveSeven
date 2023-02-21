using InteractiveSeven.Core.Data.Items;
using Newtonsoft.Json;

namespace InteractiveSeven.Core.Settings
{
    public class SpecificMateriaSettings : ObservableSettingsBase, INamedSetting
    {
        private Materia _materia;
        [JsonIgnore]
        public Materia Materia
        {
            get { return _materia ??= Materia.Get(MateriaId); }
        }

        public ushort MateriaId { get; set; }
        public string Name { get; set; }

        public SpecificMateriaSettings() // Required for Json Deserialization
        {
        }

        public SpecificMateriaSettings(Materia materia, bool enabled)
        {
            _materia = materia;
            MateriaId = materia.Value;
            Name = materia.Name;
            Enabled = enabled;
            Cost = materia.DefaultPrice;
            DropCost = materia.DefaultPrice + 50;
        }

        private bool _enabled = true;
        public bool Enabled
        {
            get => _enabled;
            set
            {
                _enabled = value;
                OnPropertyChanged();
            }
        }

        private int _cost;
        public int Cost
        {
            get => _cost;
            set
            {
                _cost = value;
                OnPropertyChanged();
            }
        }

        private int _dropCost;
        public int DropCost
        {
            get => _dropCost;
            set
            {
                _dropCost = value;
                OnPropertyChanged();
            }
        }

        public bool IsMatchByName(string name) => Name.NoSpaces().StartsWithIns(name) || Name.StartsWithIns(name);
    }
}
