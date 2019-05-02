using InteractiveSeven.Core.Memory;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Twitch;
using InteractiveSeven.UI.Services;
using InteractiveSeven.UI.Twitch;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using InteractiveSeven.UI.ViewModels;
using TwitchLib.Client.Events;
using TwitchLib.Communication.Events;

namespace InteractiveSeven.UI
{
    public partial class Form1 : Form
    {
        private readonly MenuColorAccessor _menuColorAccessor;
        private readonly PartyStatAccessor _partyStatAccessor;
        private readonly ChatBot _chatBot;
        private readonly GamePolling _gamePolling;
        private readonly MainViewModel _mainViewModel;
        private readonly MenuControlViewModel _menuControlViewModel;
        public List<PartyStat> PartyStats { get; set; }

        public Form1()
        {
            InitializeComponent();

            _mainViewModel = new MainViewModel();
            MainVmBinding.DataSource = _mainViewModel;
            _menuControlViewModel = new MenuControlViewModel();
            MenuControlVmBinding.DataSource = _menuControlViewModel;

            SetMainDataBindings();
            SetColorDataBindings();
            var memoryAccessor = new MemoryAccessor();
            _menuColorAccessor = new MenuColorAccessor(memoryAccessor);
            _partyStatAccessor = new PartyStatAccessor(memoryAccessor);
            var formSync = new FormSync(this);
            _chatBot = new ChatBot(_menuColorAccessor, formSync);
            _chatBot.OnConnected += ChatBot_OnConnected;
            _chatBot.OnDisconnected += ChatBot_OnDisconnected;
            _gamePolling = new GamePolling(this, formSync);
        }

        private void SetMainDataBindings()
        {
            ExeTextBox.DataBindings.Add(
                "Text", MainVmBinding, "ProcessName", true,
                DataSourceUpdateMode.OnPropertyChanged);

            twitchConnectionLabel.DataBindings.Add(
                "Text", MainVmBinding, "ConnectionStatus", true,
                DataSourceUpdateMode.OnPropertyChanged);
        }

        private void SetColorDataBindings()
        {
            SetBinding(topLeftColorPicker, "TopLeftColor", "Color");
            SetBinding(botLeftColorPicker, "BottomLeftColor", "Color");
            SetBinding(topRightColorPicker, "TopRightColor", "Color");
            SetBinding(botRightColorPicker, "BottomRightColor", "Color");

            SetBinding(topLeftColorSwatch, "TopLeftColor", "BackColor");
            SetBinding(botLeftColorSwatch, "BottomLeftColor", "BackColor");
            SetBinding(topRightColorSwatch, "TopRightColor", "BackColor");
            SetBinding(botRightColorSwatch, "BottomRightColor", "BackColor");

            void SetBinding(Control ctrl, string dataMember, string propName) 
                => ctrl.DataBindings.Add(
                    propName, MenuControlVmBinding, dataMember, true,
                    DataSourceUpdateMode.OnPropertyChanged);
        }

        private void ChatBot_OnConnected(object sender, OnConnectedArgs args) 
            => DisplayStatus(@"Connected");

        private void ChatBot_OnDisconnected(object sender, OnDisconnectedEventArgs args) 
            => DisplayStatus(@"Disconnected");

        private void DisplayStatus(string status) =>
            Invoke((MethodInvoker) delegate
            {
                // Running on the UI thread
                twitchConnectionLabel.Text = status;
            });

        private void Form1_Load(object sender, EventArgs e)
        {
            _gamePolling.Start();
        }

        private void ExeBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _mainViewModel.ProcessName = GetProcessNameFromFileName(openFileDialog1.FileName);
                }
                catch (Exception ex)
                {
                    string message = $"Error message: {ex.Message}\n\nDetails:\n\n{ex.StackTrace}";
                    MessageBox.Show(message);
                }
            }
        }

        private void RefreshColorsButton_Click(object sender, EventArgs e)
        {
            RefreshColors();
        }

        internal void RefreshColors()
        {
            string processName = _mainViewModel.ProcessName;
            if (string.IsNullOrWhiteSpace(processName))
            {
                return;
            }

            MenuColors currentColors = _menuColorAccessor.GetMenuColors(processName);

            _menuControlViewModel.SetColors(currentColors);
        }

        private void SetColorsButton_Click(object sender, EventArgs e)
        {
            string processName = _mainViewModel.ProcessName;
            if (string.IsNullOrWhiteSpace(processName))
            {
                return;
            }

            var menuColors = new MenuColors
            {
                TopLeft = topLeftColorPicker.Color,
                TopRight = topRightColorPicker.Color,
                BotLeft = botLeftColorPicker.Color,
                BotRight = botRightColorPicker.Color
            };

            _menuColorAccessor.SetMenuColors(processName, menuColors);
        }

        internal void RefreshPartyStats()
        {
            string processName = _mainViewModel.ProcessName;
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
            return _mainViewModel.ProcessName;
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

        private void TopLeftColorPicker_ColorChanged(object sender, EventArgs e)
        {
            topLeftColorSwatch.BackColor = topLeftColorPicker.Color;
        }

        private void TopRightColorPicker_ColorChanged(object sender, EventArgs e)
        {
            topRightColorSwatch.BackColor = topRightColorPicker.Color;
        }

        private void BotLeftColorPicker_ColorChanged(object sender, EventArgs e)
        {
            botLeftColorSwatch.BackColor = botLeftColorPicker.Color;
        }

        private void BotRightColorPicker_ColorChanged(object sender, EventArgs e)
        {
            botRightColorSwatch.BackColor = botRightColorPicker.Color;
        }

        private void AllowChatMenuControl_CheckedChanged(object sender, EventArgs e)
        {
            _chatBot.IsMenuCommandAllowed = allowChatMenuControl.Checked;
        }
    }
}
