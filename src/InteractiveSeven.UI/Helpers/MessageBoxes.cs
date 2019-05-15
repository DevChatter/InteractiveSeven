using System;
using System.Windows.Forms;

namespace InteractiveSeven.UI.Helpers
{
    public static class MessageBoxes
    {
        public static void DoIfConfirmed(string text, string caption,
            Action ifYesAction = null, Action ifNoAction = null)
        {
            var confirmResult = MessageBox.Show(text,
                caption,
                MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                ifYesAction?.Invoke();
            }
            else
            {
                ifNoAction?.Invoke();
            }
        }

    }
}