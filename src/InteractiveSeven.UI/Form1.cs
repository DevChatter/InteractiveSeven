using InteractiveSeven.UI.Memory;
using InteractiveSeven.UI.Models;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using InteractiveSeven.UI.Twitch;

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
            _chatBot = new ChatBot(_menuColorAccessor);
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
                    ExeTextBox.Text = openFileDialog1.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }

        private void TopLeftColorButton_Click(object sender, EventArgs e)
        {
            RequestColorChoice(TopLeftRedTextBox, TopLeftGreenTextBox, TopLeftBlueTextBox);
        }

        private void TopRightColorButton_Click(object sender, EventArgs e)
        {
            RequestColorChoice(TopRightRedTextBox, TopRightGreenTextBox, TopRightBlueTextBox);
        }

        private void BotLeftColorButton_Click(object sender, EventArgs e)
        {
            RequestColorChoice(BotLeftRedTextBox, BotLeftGreenTextBox, BotLeftBlueTextBox);
        }

        private void BotRightColorButton_Click(object sender, EventArgs e)
        {
            RequestColorChoice(BotRightRedTextBox, BotRightGreenTextBox, BotRightBlueTextBox);
        }

        private void RequestColorChoice(MaskedTextBox redTextBox, MaskedTextBox greenTextBox, MaskedTextBox blueTextBox)
        {
            ColorDialog myColorDialog = new ColorDialog
            {
                AllowFullOpen = true,
                ShowHelp = true
            };

            int.TryParse(redTextBox.Text, out int red);
            int.TryParse(greenTextBox.Text, out int green);
            int.TryParse(blueTextBox.Text, out int blue);
            myColorDialog.Color = Color.FromArgb(red, green, blue);

            if (myColorDialog.ShowDialog() == DialogResult.OK)
            {
                redTextBox.Text = myColorDialog.Color.R.ToString("D3");
                greenTextBox.Text = myColorDialog.Color.G.ToString("D3");
                blueTextBox.Text = myColorDialog.Color.B.ToString("D3");
            }
        }

        private void RefreshColorsButton_Click(object sender, EventArgs e)
        {
            string processName = ExeTextBox.Text.Split('\\').LastOrDefault()?.Split('.')?.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(processName))
            {
                return;
            }

            var currentColors = _menuColorAccessor.GetMenuColors(processName);

            TopLeftRedTextBox.Text = currentColors.TopLeft.Red.ToString();
            TopLeftGreenTextBox.Text = currentColors.TopLeft.Green.ToString();
            TopLeftBlueTextBox.Text = currentColors.TopLeft.Blue.ToString();

            TopRightRedTextBox.Text = currentColors.TopRight.Red.ToString();
            TopRightGreenTextBox.Text = currentColors.TopRight.Green.ToString();
            TopRightBlueTextBox.Text = currentColors.TopRight.Blue.ToString();

            BotLeftRedTextBox.Text = currentColors.BotLeft.Red.ToString();
            BotLeftGreenTextBox.Text = currentColors.BotLeft.Green.ToString();
            BotLeftBlueTextBox.Text = currentColors.BotLeft.Blue.ToString();

            BotRightRedTextBox.Text = currentColors.BotRight.Red.ToString();
            BotRightGreenTextBox.Text = currentColors.BotRight.Green.ToString();
            BotRightBlueTextBox.Text = currentColors.BotRight.Blue.ToString();
        }

        private void SetColorsButton_Click(object sender, EventArgs e)
        {
            string processName = ExeTextBox.Text.Split('\\').LastOrDefault()?.Split('.')?.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(processName))
            {
                return;
            }

            var menuColors = new MenuColors
            {
                TopLeft = new MenuCornerColor(
                    TopLeftBlueTextBox.Text,
                    TopLeftGreenTextBox.Text,
                    TopLeftRedTextBox.Text),
                TopRight = new MenuCornerColor(
                    TopRightBlueTextBox.Text,
                    TopRightGreenTextBox.Text,
                    TopRightRedTextBox.Text),
                BotLeft = new MenuCornerColor(
                    BotLeftBlueTextBox.Text,
                    BotLeftGreenTextBox.Text,
                    BotLeftRedTextBox.Text),
                BotRight = new MenuCornerColor(
                    BotRightBlueTextBox.Text,
                    BotRightGreenTextBox.Text,
                    BotRightRedTextBox.Text)
            };

            _menuColorAccessor.SetMenuColors(processName, menuColors);
        }

        private void TwitchConnectButton_Click(object sender, EventArgs e)
        {
            _chatBot.Connect();
        }

        private void TwitchDisconnectButton_Click(object sender, EventArgs e)
        {
            _chatBot.Disconnect();
        }
    }
}
