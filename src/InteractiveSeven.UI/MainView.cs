using InteractiveSeven.Core.Memory;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Twitch;
using InteractiveSeven.UI.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using InteractiveSeven.UI.Settings;
using TwitchLib.Client.Events;
using TwitchLib.Communication.Events;

namespace InteractiveSeven.UI
{
    public partial class MainView : Form, IViewFor<MainViewModel>
    {
        private readonly PartyStatAccessor _partyStatAccessor;
        private readonly ChatBot _chatBot;
        public List<PartyStat> PartyStats { get; set; }

        public MainView(PartyStatAccessor partyStatAccessor,
            MainViewModel viewModel,
            ChatBot chatBot)
        {
            _partyStatAccessor = partyStatAccessor;
            ViewModel = viewModel;
            _chatBot = chatBot;

            InitializeComponent();

            SetDataBindings();

            _chatBot.OnConnected += ChatBot_OnConnected;
            _chatBot.OnDisconnected += ChatBot_OnDisconnected;
        }

        private void SetDataBindings()
        {
            this.WhenActivated(d =>
            {
                d(this.Bind(ViewModel, x => x.ProcessName, x => x.ExeTextBox.Text));
                d(this.Bind(ViewModel, x => x.ConnectionStatus, x => x.twitchConnectionLabel.Text));
                d(this.BindCommand(ViewModel, x => x.BrowseExeCmd, x => x.ExeBrowse));
            });

        }

        private void ChatBot_OnConnected(object sender, OnConnectedArgs args)
        {
            ViewModel.IsConnected = true;
        }

        private void ChatBot_OnDisconnected(object sender, OnDisconnectedEventArgs args)
        {
            ViewModel.IsConnected = false;
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

        private void AllowChatMenuControl_CheckedChanged(object sender, EventArgs e)
        {
            _chatBot.IsMenuCommandAllowed = allowChatMenuControl.Checked;
        }

        MainViewModel IViewFor<MainViewModel>.ViewModel
        {
            get => ViewModel;
            set => ViewModel = value;
        }

        object IViewFor.ViewModel
        {
            get => null;
            set => ViewModel = (MainViewModel)value;
        }

        public MainViewModel ViewModel { get; set; }
    }
}
