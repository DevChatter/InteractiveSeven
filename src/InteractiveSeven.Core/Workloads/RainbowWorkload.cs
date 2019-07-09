using System;
using InteractiveSeven.Core.Memory;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Settings;
using Microsoft.Extensions.Logging;

namespace InteractiveSeven.Core.Workloads
{
    public class RainbowWorkload : IWorkload
    {
        private readonly IMenuColorAccessor _menuColorAccessor;
        private readonly ILogger<MenuColorChangeWorkload> _logger;

        public RainbowWorkload(IMenuColorAccessor menuColorAccessor,
            ILogger<MenuColorChangeWorkload> logger)
        {
            _menuColorAccessor = menuColorAccessor;
            _logger = logger;
        }

        public void Run()
        {
            try
            {
                for (int i = 0; i < 10000; i++) // TODO: Configurable Time
                {
                    _menuColorAccessor.SetMenuColors(ApplicationSettings.Instance.ProcessName,
                        MenuColors.RandomPalette());
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error changing menu colors.");
            }
        }
    }
}