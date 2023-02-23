using CommunityToolkit.Mvvm.ComponentModel;
using InteractiveSeven.Core.Data.Items;
using Newtonsoft.Json;

namespace InteractiveSeven.Core.Settings;

public partial class SpecificItemSettings : ObservableObject, INamedSetting
{
    private Items _item;
    [JsonIgnore]
    public Items Item
    {
        get { return _item ??= Items.GetByItemId(ItemId); }
    }

    public ushort ItemId { get; set; }
    public string Name { get; set; }

    public SpecificItemSettings() // Required for Json Deserialization
    {
    }

    public SpecificItemSettings(Items item, bool enabled)
    {
        _item = item;
        ItemId = item.ItemId;
        Name = item.Name;
        Enabled = enabled;
        Cost = item.DefaultPrice;
        DropCost = item.DefaultPrice + 50;
    }

    [ObservableProperty]
    private bool _enabled = true;

    [ObservableProperty]
    private int _cost;

    [ObservableProperty]
    private int _dropCost;

    public bool IsMatchByName(string name) => Name.NoSpaces().StartsWithIns(name) || Name.StartsWithIns(name);
}
