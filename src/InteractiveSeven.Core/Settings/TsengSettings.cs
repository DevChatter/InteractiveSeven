using CommunityToolkit.Mvvm.ComponentModel;

namespace InteractiveSeven.Core.Settings;

public partial class TsengSettings : ObservableObject
{
    [ObservableProperty]
    private int _memoryReadIntervalInMs = 500;

    [ObservableProperty]
    private int _portNumber = 7777;
}
