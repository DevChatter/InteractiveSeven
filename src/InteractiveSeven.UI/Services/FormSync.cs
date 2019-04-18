using System.Windows.Forms;
using InteractiveSeven.Core.Services;

namespace InteractiveSeven.UI.Services
{
    public class FormSync : IFormSync
    {
        private readonly Form1 _form1;

        public FormSync(Form1 form1)
        {
            _form1 = form1;
        }

        public void RefreshColors()
        {
            _form1.Invoke((MethodInvoker)delegate {
                // Running on the UI thread
                _form1.RefreshColors();
            });
        }

        public string GetProcessName()
        {
            return _form1.GetProcessName();
        }
    }
}
