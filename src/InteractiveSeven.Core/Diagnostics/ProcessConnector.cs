using InteractiveSeven.Core.Settings;
using System.Diagnostics;
using System.Linq;

namespace InteractiveSeven.Core.Diagnostics
{
    public class ProcessConnector
    {
        private string ProcessName => ApplicationSettings.Instance.ProcessName;

        private readonly object _padlock = new object();

        private Process _ff7Process;
        public Process FF7Process
        {
            get
            {
                try
                {
                    if (_ff7Process is null)
                    {
                        lock (_padlock)
                        {
                            if (_ff7Process is null)
                            {
                                _ff7Process ??= GetProcess();
                            }
                        }
                    }

                    return _ff7Process;
                }
                catch
                {
                    // TODO: Log here
                }

                return null;

                Process GetProcess()
                {
                    return (!string.IsNullOrWhiteSpace(ProcessName)
                               ? Process.GetProcessesByName(ProcessName).FirstOrDefault()
                               : _ff7Process)
                           ?? Process.GetProcessesByName("ff7_en").FirstOrDefault()
                           ?? Process.GetProcessesByName("ff7").FirstOrDefault();
                }
            }

        }
    }
}