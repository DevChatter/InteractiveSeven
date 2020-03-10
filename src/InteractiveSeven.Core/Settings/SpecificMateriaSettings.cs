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

        }

        private bool _enabled = true;
        public bool Enabled // TODO: Add to Settings Screen
        {
            get => _enabled;
            set
            {
                _enabled = value;
                OnPropertyChanged();
            }
        }

        private int _cost;
        public int Cost // TODO: Add to Settings Screen
        {
            get => _cost;
            set
            {
                _cost = value;
                OnPropertyChanged();
            }
        }

        public bool IsMatchByName(string name) => Name.NoSpaces().StartsWithIns(name);
    }
}