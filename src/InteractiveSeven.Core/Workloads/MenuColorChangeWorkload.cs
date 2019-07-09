using InteractiveSeven.Core.Memory;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Settings;
using Microsoft.Extensions.Logging;
using System;

namespace InteractiveSeven.Core.Workloads
{
    public class MenuColorChangeWorkload : IWorkload
    {
        private readonly MenuColors _menuColors;
        private readonly IMenuColorAccessor _menuColorAccessor;
        private readonly ILogger<MenuColorChangeWorkload> _logger;

        public MenuColorChangeWorkload(MenuColors menuColors, IMenuColorAccessor menuColorAccessor,
            ILogger<MenuColorChangeWorkload> logger)
        {
            _menuColors = menuColors;
            _menuColorAccessor = menuColorAccessor;
            _logger = logger;
        }

        public void Run()
        {
            try
            {
                _menuColorAccessor.SetMenuColors(ApplicationSettings.Instance.ProcessName, _menuColors);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error changing menu colors.");
            }
        }
    }
}