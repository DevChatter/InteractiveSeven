using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace InteractiveSeven.Core.Settings;

public partial class ApplicationSettings : ObservableObject
{
    [ObservableProperty]
    private string _processName = "ff7_en";
    [ObservableProperty]
    private bool _giveSubscriberBonusBits = true;
    [ObservableProperty]
    private int _subscriberBonusBits = 150;
    [ObservableProperty]
    private bool _modsGiveBonusBits = true;
    [ObservableProperty]
    private bool _enableModCommand = true;
    [ObservableProperty]
    private bool _allowGameAffectingCommands = true;

    public static ApplicationSettings Instance { get; private set; }
    static ApplicationSettings()
    {
        Instance = new ApplicationSettings();
    }

    [JsonIgnore]
    public GamePlayEffects GamePlayMode => AllowGameAffectingCommands ?
        GamePlayEffects.DisplayOnly | GamePlayEffects.MildEffect | GamePlayEffects.MajorEffect
        : GamePlayEffects.DisplayOnly;

    public CommandSettings CommandSettings { get; set; } = new();
    public MenuColorSettings MenuSettings { get; set; } = new();
    public NameBiddingSettings NameBiddingSettings { get; set; } = new();
    public BattleSettings BattleSettings { get; set; } = new();
    public EquipmentSettings EquipmentSettings { get; set; } = new();
    public ItemSettings ItemSettings { get; set; } = new();
    public MateriaSettings MateriaSettings { get; set; } = new();
    public TsengSettings TsengSettings { get; set; } = new();

    public static void LoadFromJson(string json, Action<Exception> errorLogging = null)
    {
        try
        {
            JsonConvert.PopulateObject(json, Instance);
        }
        catch (Exception ex)
        {
            Instance = new ApplicationSettings();
            errorLogging?.Invoke(ex);
        }
    }

    /// <summary>
    /// Temporary solution to the duplicate load in collections issue.
    /// </summary>
    public void CleanUpCollections()
    {
        RemoveDuplicates(BattleSettings.AllStatusEffects);
        RemoveDuplicates(EquipmentSettings.AllAccessories);
        RemoveDuplicates(EquipmentSettings.AllArmlets);
        RemoveDuplicates(EquipmentSettings.AllWeapons);
        RemoveDuplicates(ItemSettings.AllItems);
        RemoveDuplicates(MateriaSettings.AllMateria);
    }

    /// <summary>
    /// Keep the later item, since that should be the one from their local store.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collection"></param>
    private void RemoveDuplicates<T>(List<T> collection)
        where T : INamedSetting
    {
        HashSet<string> names = new HashSet<string>();
        for (int i = collection.Count - 1; i >= 0; i--)
        {
            if (names.Contains(collection[i].Name))
            {
                collection.RemoveAt(i);
            }
            else
            {
                names.Add(collection[i].Name);
            }
        }
    }

}
