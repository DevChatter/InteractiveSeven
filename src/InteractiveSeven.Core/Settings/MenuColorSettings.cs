using CommunityToolkit.Mvvm.ComponentModel;

namespace InteractiveSeven.Core.Settings;

public partial class MenuColorSettings : ObservableObject
{
    [ObservableProperty]
    private bool _enabled = true;
    [ObservableProperty]
    private int _bitCost;
    [ObservableProperty]
    private bool _allowModOverride = true;
    [ObservableProperty]
    private bool _transitionColors = true;
    [ObservableProperty]
    private bool _enableRainbowCommand = true;
    [ObservableProperty]
    private int _rainbowModeCost = 1000;
    [ObservableProperty]
    private int _rainbowModeIterations = 30;
    [ObservableProperty]
    private bool _enableMakoCommand = true;
    [ObservableProperty]
    private int _makoModeCost = 500;
    [ObservableProperty]
    private int _makoModeIterations = 30;
    [ObservableProperty]
    private bool _enablePaletteCommand = true;
    [ObservableProperty]
    private bool _allowModsToCreatePalettes = true;
}
