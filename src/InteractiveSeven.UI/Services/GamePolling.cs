using InteractiveSeven.Core.Services;
using System.Threading.Tasks;

namespace InteractiveSeven.UI.Services
{
    public class GamePolling
    {
        private readonly Form1 _mainForm;
        private readonly IFormSync _formSync;

        public GamePolling(Form1 mainForm, IFormSync formSync)
        {
            _mainForm = mainForm;
            _formSync = formSync;
        }

        public void Start()
        {
            Task.Run(DoWork);
        }

        private void DoWork()
        {
            while (true)
            {
                _formSync.RefreshPartyStats();
                System.Threading.Thread.Sleep(15000);
            }
        }
    }
}