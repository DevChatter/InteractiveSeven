using System.ComponentModel;
using System.Windows.Forms;

namespace InteractiveSeven.UI.UserControls
{
    public partial class PartyMemberStats : UserControl
    {
        public PartyMemberStats()
        {
            InitializeComponent();
        }

        [Description("Displayed Character Name"), Category("Data")]
        public string CharacterName
        {
            get => charNameLabel.Text;
            set => charNameLabel.Text = value;
        }
    }
}
