using System.Diagnostics;
using InteractiveSeven.Core.MvvmCommands;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Core.ViewModels;

namespace InteractiveSeven.Commands
{
    public class TwitchAuth : ITwitchAuth
    {
        private readonly TwitchAuthViewModel _twitchAuthViewModel;

        public TwitchAuth(TwitchAuthViewModel twitchAuthViewModel)
        {
            _twitchAuthViewModel = twitchAuthViewModel;
        }

        public void Show()
        {
            TwitchSettings.Instance.UpdatedFromTwitch = false;
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = _twitchAuthViewModel.AuthRequestUrl,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
    }
}
