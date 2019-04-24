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
        public List<PartyStat> PartyStats { get; set; }

        public Form1()
        {
            InitializeComponent();
            var memoryAccessor = new MemoryAccessor();
            _menuColorAccessor = new MenuColorAccessor(memoryAccessor);
            _partyStatAccessor = new PartyStatAccessor(memoryAccessor);
            var formSync = new FormSync(this);
            _chatBot = new ChatBot(_menuColorAccessor, formSync);
            _chatBot.OnConnected += ChatBot_OnConnected;
            _chatBot.OnDisconnected += ChatBot_OnDisconnected;
            _gamePolling = new GamePolling(this, formSync);
        }

        private void ChatBot_OnConnected(object sender, OnConnectedArgs args) => DisplayStatus(@"Connected");

        private void ChatBot_OnDisconnected(object sender, OnDisconnectedEventArgs args) => DisplayStatus(@"Disconnected");

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
                    ExeTextBox.Text = GetProcessNameFromFileName(openFileDialog1.FileName);
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
            string processName = ExeTextBox.Text;
            if (string.IsNullOrWhiteSpace(processName))
            {
                return;
            }

            var currentColors = _menuColorAccessor.GetMenuColors(processName);

            topLeftColorPicker.Color = Color.FromArgb(
                currentColors.TopLeft.Red,
                currentColors.TopLeft.Green,
                currentColors.TopLeft.Blue);

            botLeftColorPicker.Color = Color.FromArgb(
                currentColors.BotLeft.Red,
                currentColors.BotLeft.Green,
                currentColors.BotLeft.Blue);

            topRightColorPicker.Color = Color.FromArgb(
                currentColors.TopRight.Red,
                currentColors.TopRight.Green,
                currentColors.TopRight.Blue);

            botRightColorPicker.Color = Color.FromArgb(
                currentColors.BotRight.Red,
                currentColors.BotRight.Green,
                currentColors.BotRight.Blue);
        }

        private void SetColorsButton_Click(object sender, EventArgs e)
        {
            string processName = ExeTextBox.Text;
            if (string.IsNullOrWhiteSpace(processName))
            {
                return;
            }

            var menuColors = new MenuColors
            {
                TopLeft = new MenuCornerColor(topLeftColorPicker.Color),
                TopRight = new MenuCornerColor(topRightColorPicker.Color),
                BotLeft = new MenuCornerColor(botLeftColorPicker.Color),
                BotRight = new MenuCornerColor(botRightColorPicker.Color)
            };

            _menuColorAccessor.SetMenuColors(processName, menuColors);
        }

        internal void RefreshPartyStats()
        {
            string processName = ExeTextBox.Text;
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
            return ExeTextBox.Text;
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
