using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InteractiveSeven.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
    }
}
