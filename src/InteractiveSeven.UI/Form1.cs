using InteractiveSeven.Core.Memory;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Twitch;
using InteractiveSeven.UI.Services;
using InteractiveSeven.UI.Twitch;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace InteractiveSeven.UI
{
    public partial class Form1 : Form
    {
        private readonly MenuColorAccessor _menuColorAccessor;
        private readonly ChatBot _chatBot;

        public Form1()
        {
            InitializeComponent();
            _menuColorAccessor = new MenuColorAccessor(new MemoryAccessor());
            _chatBot = new ChatBot(_menuColorAccessor, new FormSync(this));
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
