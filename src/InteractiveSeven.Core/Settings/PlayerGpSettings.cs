using CommunityToolkit.Mvvm.ComponentModel;

namespace InteractiveSeven.Core.Settings;

public partial class PlayerGpSettings : ObservableObject
{
    [ObservableProperty]
    private ushort _giveMultiplier = 100;
    [ObservableProperty]
    private ushort _removeMultiplier = 100;
    [ObservableProperty]
    private bool _allowModOverride = true;
    [ObservableProperty]
    private bool _giveGpEnabled = false;
    [ObservableProperty]
    private bool _removeGpEnabled = false;
}
