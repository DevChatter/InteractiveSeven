using System;
using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Data.Items;

namespace InteractiveSeven.Core.Settings;

public partial class EquipmentSettings : ObservableObject
{
    [ObservableProperty]
    private bool _enabled = true;
    [ObservableProperty]
    private bool _allowModOverride = true;
    [ObservableProperty]
    private bool _keepPreviousEquipment = true;
    [ObservableProperty]
    private bool _enablePauperCommand = true;
    [ObservableProperty]
    private int _pauperCommandCost = 20000;

    public EquipmentSettings()
    {
        AllWeapons = Items.All.OfType<Weapon>().Select(x => new EquippableSettings(x, true, x.Words)).ToList();
        AllArmlets = Items.All.OfType<Armlet>().Select(x => new EquippableSettings(x, true, x.Words)).ToList();
        AllAccessories = Items.All.OfType<Accessory>().Select(x => new EquippableSettings(x, true, x.Words)).ToList();
    }

    public List<EquippableSettings> AllByName(string name, CharNames charName, Type type)
    {
        if (type == typeof(Weapon))
        {
            return AllWeapons.AllByName(name, charName);
        }
        if (type == typeof(Accessory))
        {
            return AllAccessories.AllByName(name, charName);
        }
        if (type == typeof(Armlet))
        {
            return AllArmlets.AllByName(name, charName);
        }
        return new List<EquippableSettings>();
    }

    public List<EquippableSettings> AllWeapons { get; set; }
    public List<EquippableSettings> AllArmlets { get; set; }
    public List<EquippableSettings> AllAccessories { get; set; }
    public PlayerGilSettings PlayerGilSettings { get; set; } = new();
    public PlayerGpSettings PlayerGpSettings { get; set; } = new();
}
