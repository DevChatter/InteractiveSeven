using System.Threading.Tasks;

namespace InteractiveSeven.UI.Services
{
    public class GamePolling
    {
        private readonly Form1 _mainForm;

        public GamePolling(Form1 mainForm)
        {
            _mainForm = mainForm;
        }

        public void Start()
        {
            Task.Run(DoWork);
        }

        private void DoWork()
        {
        }
    }
}