using System.ComponentModel;
using System.Windows.Forms;

namespace InteractiveSeven.UI.UserControls
{
    public partial class DateStatus : UserControl
    {

        public DateStatus()
        {
            InitializeComponent();
        }

        [Description("Displayed Character Name"), Category("Data")]
        public string CharacterName
        {
            get => nameLabel.Text;
            set => nameLabel.Text = value;
        }
    }
}
