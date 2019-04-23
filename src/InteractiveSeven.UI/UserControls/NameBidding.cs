using System.ComponentModel;
using System.Windows.Forms;

namespace InteractiveSeven.UI.UserControls
{
    public partial class NameBidding : UserControl
    {
        public NameBidding()
        {
            InitializeComponent();
        }

        [Description("Displayed Character Name"), Category("Data")]
        public string CharacterName
        {
            get => characterNameLabel.Text;
            set => characterNameLabel.Text = value;
        }

    }
}
