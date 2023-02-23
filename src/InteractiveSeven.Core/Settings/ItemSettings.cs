using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using InteractiveSeven.Core.Data.Items;

namespace InteractiveSeven.Core.Settings;

public partial class ItemSettings : ObservableObject
{
    [ObservableProperty]
    private bool _enabled = true;
    [ObservableProperty]
    private bool _allowMod = true; // TODO: Add to Settings Screen

    public ItemSettings()
    {
        AllItems = Items.All.Select(x => new SpecificItemSettings(x, true)).ToList();
    }

    public List<SpecificItemSettings> AllItems { get; set; }

    public List<SpecificItemSettings> AllByName(string name)
        => AllItems.Where(x => x.IsMatchByName(name)).ToList();
}
