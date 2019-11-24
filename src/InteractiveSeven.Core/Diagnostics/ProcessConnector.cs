using InteractiveSeven.Core.Settings;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;

namespace InteractiveSeven.Core.Diagnostics
{
    public class ProcessConnector
    {
        private readonly ILogger<ProcessConnector> _logger;
        private readonly object _padlock = new object();

        public ProcessConnector(ILogger<ProcessConnector> logger)
        {
            _logger = logger;
        }

        private string ProcessName => ApplicationSettings.Instance.ProcessName;

        private Process _ff7Process;
        public Process FF7Process
        {
            get
            {
                try
                {
                    if (_ff7Process is null || _ff7Process.HasExited)
                    {
                        lock (_padlock)
                        {
                            if (_ff7Process is null || _ff7Process.HasExited)
                            {
                                _ff7Process = GetProcess();
                            }
                        }
                    }

                    return _ff7Process;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error connecting to FF7 Process");
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