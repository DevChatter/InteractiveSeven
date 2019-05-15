using InteractiveSeven.Core.Memory;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Twitch;
using InteractiveSeven.UI.Helpers;
using InteractiveSeven.UI.Settings;
using InteractiveSeven.UI.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using InteractiveSeven.UI.UserControls;
using TwitchLib.Client.Events;
using TwitchLib.Communication.Events;

namespace InteractiveSeven.UI
{
    public partial class MainView : Form, IViewFor<MainViewModel>
    {
        private MenuColorControlTab _menuColorControlTab;
        private readonly PartyStatAccessor _partyStatAccessor;
        private readonly ISettingsStore _settingsStore;
        private readonly ChatBot _chatBot;
        public List<PartyStat> PartyStats { get; set; }

        public MainView(MenuColorControlTab menuColorControlTab,
            PartyStatAccessor partyStatAccessor,
            MainViewModel viewModel,
            ISettingsStore settingsStore,
            ChatBot chatBot)
        {
            _partyStatAccessor = partyStatAccessor;
            _settingsStore = settingsStore;
            ViewModel = viewModel;
            _chatBot = chatBot;

            InitializeComponent();

            ConfigureMenuColorControlTab(menuColorControlTab);

            SetDataBindings();

            _chatBot.OnConnected += ChatBot_OnConnected;
            _chatBot.OnDisconnected += ChatBot_OnDisconnected;
        }

        private void ConfigureMenuColorControlTab(MenuColorControlTab menuColorControlTab)
        {
            _menuColorControlTab = menuColorControlTab;
            menuColorTab.Controls.Add(_menuColorControlTab);
            _menuColorControlTab.Location = new System.Drawing.Point(4, 7);
            _menuColorControlTab.Dock = DockStyle.Fill;
            _menuColorControlTab.TabIndex = 17;
        }

        private void SetDataBindings()
        {
            this.WhenActivated(d =>
            {
                d(this.Bind(ViewModel, vm => vm.ProcessName, v => v.ExeTextBox.Text));
                d(this.OneWayBind(ViewModel, vm => vm.ConnectionStatus, v => v.twitchConnectionLabel.Text));
            });
        }

        private void ChatBot_OnConnected(object sender, OnConnectedArgs args)
        {
            Invoke((MethodInvoker)delegate {
                ViewModel.IsConnected = true;
                ViewModel.ConnectionStatus = "Connected";
            });
        }

        private void ChatBot_OnDisconnected(object sender, OnDisconnectedEventArgs args)
        {
            Invoke((MethodInvoker)delegate {
                ViewModel.IsConnected = false;
                ViewModel.ConnectionStatus = "Disconnected";
            });
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void ExeBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ViewModel.ProcessName = GetProcessNameFromFileName(openFileDialog1.FileName);
                }
                catch (Exception ex)
                {
                    string message = $"Error message: {ex.Message}\n\nDetails:\n\n{ex.StackTrace}";
                    MessageBox.Show(message);
                }
            }
        }

        internal void RefreshPartyStats()
        {
            string processName = ViewModel.ProcessName;
            if (string.IsNullOrWhiteSpace(processName))
            {
                return;
            }

            PartyStats = _partyStatAccessor.GetPartyStats(processName);
        }

        private string GetProcessNameFromFileName(string fileName)
        {
            return fileName
                .Split('\\')
                .LastOrDefault()
                ?.Split('.')
                ?.FirstOrDefault();
        }

        internal string GetProcessName()
        {
            return ViewModel.ProcessName;
        }

        private void TwitchConnectButton_Click(object sender, EventArgs e)
        {
            _chatBot.Connect(
                TwitchSettings.Settings.Username,
                TwitchSettings.Settings.AccessToken,
                TwitchSettings.Settings.Channel);
        }

        private void TwitchDisconnectButton_Click(object sender, EventArgs e)
        {
            _chatBot.Disconnect();
        }

        MainViewModel IViewFor<MainViewModel>.ViewModel
        {
            get => ViewModel;
            set => ViewModel = value;
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (MainViewModel)value;
        }

        public MainViewModel ViewModel { get; set; }

        private void FileSaveMenuItem_Click(object sender, EventArgs e)
        {
            MessageBoxes.DoIfConfirmed("Are you sure you want to overwrite the current settings?",
                "Confirm Settings Overwrite",
                SaveSettings);
        }

        private void SaveSettings() => _settingsStore.SaveSettings();

        private void FileExitMenuItem_Click(object sender, EventArgs e)
        {
            MessageBoxes.DoIfConfirmed("Are you sure you wish to exit?",
                "Confirm Exit",
                ExitApp);
        }

        private void ExitApp() => Application.Exit();
    }
}
