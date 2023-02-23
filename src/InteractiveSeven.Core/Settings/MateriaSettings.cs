using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using InteractiveSeven.Core.Data.Items;

namespace InteractiveSeven.Core.Settings;
public partial class MateriaSettings : ObservableObject
{
    [ObservableProperty]
    private bool _enabled = true;
    [ObservableProperty]
    private bool _allowMod = true; // TODO: Add to Settings Screen

    public MateriaSettings()
    {
        AllMateria = Materia.All.Select(x => new SpecificMateriaSettings(x, true)).ToList();
    }

    public List<SpecificMateriaSettings> AllMateria { get; set; }

    public List<SpecificMateriaSettings> AllByName(string name)
        => AllMateria.Where(x => x.IsMatchByName(name)).ToList();
}
