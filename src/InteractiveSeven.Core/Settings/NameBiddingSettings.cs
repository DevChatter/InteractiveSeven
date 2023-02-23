using CommunityToolkit.Mvvm.ComponentModel;

namespace InteractiveSeven.Core.Settings;

public partial class NameBiddingSettings : ObservableObject
{
    [ObservableProperty]
    private bool _enabled = true;
    [ObservableProperty]
    private int _defaultStartBits = 99;
    [ObservableProperty]
    private bool _allowModeration = true;
    [ObservableProperty]
    private bool _allowModBits = true;
    [ObservableProperty]
    private bool _namingCloudEnabled = true;
    [ObservableProperty]
    private bool _namingBarretEnabled = true;
    [ObservableProperty]
    private bool _namingTifaEnabled = true;
    [ObservableProperty]
    private bool _namingAerisEnabled = true;
    [ObservableProperty]
    private bool _namingCaitSithEnabled = true;
    [ObservableProperty]
    private bool _namingCidEnabled = true;
    [ObservableProperty]
    private bool _namingRedEnabled = true;
    [ObservableProperty]
    private bool _namingVincentEnabled = true;
    [ObservableProperty]
    private bool _namingYuffieEnabled = true;
}
