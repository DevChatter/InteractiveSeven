using CommunityToolkit.Mvvm.ComponentModel;

namespace InteractiveSeven.Core.Settings;

public partial class PlayerGilSettings : ObservableObject
{
    [ObservableProperty]
    private int _giveMultiplier = 100;
    [ObservableProperty]
    private int _removeMultiplier = 100;
    [ObservableProperty]
    private bool _allowModOverride = true;
    [ObservableProperty]
    private bool _giveGilEnabled = false;
    [ObservableProperty]
    private bool _removeGilEnabled = false;
}
