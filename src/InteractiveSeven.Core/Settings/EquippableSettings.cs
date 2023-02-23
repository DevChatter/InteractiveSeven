using CommunityToolkit.Mvvm.ComponentModel;
using InteractiveSeven.Core.Data.Items;
using Newtonsoft.Json;

namespace InteractiveSeven.Core.Settings;

public partial class EquippableSettings : ObservableObject, INamedSetting
{
    private Equipment _item;
    [JsonIgnore]
    public Equipment Item
    {
        get { return _item ??= Items.GetByItemId(ItemId) as Equipment; }
    }

    public ushort ItemId { get; set; }

    public string Name { get; set; }

    public EquippableSettings() // Required for Json Deserialization
    {
    }

    public EquippableSettings(Equipment item, bool enabled, params string[] words)
    {
        _item = item;
        ItemId = item.ItemId;
        Name = item.Name;
        Enabled = enabled;
        Cost = item.DefaultPrice;
        Words = words;
    }

    [ObservableProperty]
    private bool _enabled = true;

    [ObservableProperty]
    private int _cost;

    [ObservableProperty]
    private string[] _words; // TODO: Add to Settings Screen
}
