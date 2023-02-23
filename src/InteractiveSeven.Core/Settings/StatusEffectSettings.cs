using System;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using InteractiveSeven.Core.Commands.Battle;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace InteractiveSeven.Core.Settings;

public partial class StatusEffectSettings : ObservableObject, INamedSetting
{
    [JsonConverter(typeof(StringEnumConverter))]
    public StatusEffects Effect { get; set; }

    public string Name { get; set; }

    public StatusEffectSettings()
    {
    }

    public StatusEffectSettings(string name, StatusEffects effect, bool enabled, int cost, int cureCost, params string[] words)
    {
        Name = name;
        Effect = effect;
        _words = words ?? Array.Empty<string>();
        _enabled = enabled;
        _cost = cost;
        _cureCost = cureCost;
    }

    private string[] _words;
    public string[] Words
    {
        get => _words;
        set
        {
            _words = RemoveDuplicates(value);
            OnPropertyChanged();
        }
    }

    private string[] RemoveDuplicates(string[] value)
    {
        var allStatusEffects = ApplicationSettings.Instance.BattleSettings.AllStatusEffects;

        var otherEffectWords = allStatusEffects
            .Where(x => x.Name != Name)
            .SelectMany(x => x.Words);

        return value.Except(otherEffectWords, StringComparer.OrdinalIgnoreCase).ToArray();
    }

    [ObservableProperty]
    private bool _enabled = true;

    [ObservableProperty]
    private int _cost = 100;

    [ObservableProperty]
    private int _cureCost = 100;
}
