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
            ColorDialog myColorDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            myColorDialog.AllowFullOpen = false;
            // Allows the user to get help. (The default is false.)
            myColorDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.
            int.TryParse(TopLeftRedTextBox.Text, out int red);
            int.TryParse(TopLeftGreenTextBox.Text, out int green);
            int.TryParse(TopLeftBlueTextBox.Text, out int blue);
            myColorDialog.Color = Color.FromArgb(red, green, blue);

            // Update the text box color if the user clicks OK 
            if (myColorDialog.ShowDialog() == DialogResult.OK)
            {
                TopLeftRedTextBox.Text = myColorDialog.Color.R.ToString();
                TopLeftGreenTextBox.Text = myColorDialog.Color.G.ToString();
                TopLeftBlueTextBox.Text = myColorDialog.Color.B.ToString();
            }
        }
    }
}
