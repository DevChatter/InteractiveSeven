using CommunityToolkit.Mvvm.ComponentModel;
using InteractiveSeven.Core.Data.Items;
using Newtonsoft.Json;

namespace InteractiveSeven.Core.Settings;

public partial class SpecificMateriaSettings : ObservableObject, INamedSetting
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

    [ObservableProperty]
    private bool _enabled = true;

    [ObservableProperty]
    private int _cost;

    [ObservableProperty]
    private int _dropCost;

    public bool IsMatchByName(string name) => Name.NoSpaces().StartsWithIns(name) || Name.StartsWithIns(name);
}
