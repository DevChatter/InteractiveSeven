using System;
using System.Collections.Generic;
using System.Drawing;
using InteractiveSeven.Core.Memory;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Settings;
using Microsoft.Extensions.Logging;

namespace InteractiveSeven.Core.Workloads
{
    public class MakoColorsWorkload : IWorkload
    {
        private readonly IMenuColorAccessor _menuColorAccessor;
        private readonly ILogger<MenuColorChangeWorkload> _logger;

        public MakoColorsWorkload(IMenuColorAccessor menuColorAccessor,
            ILogger<MenuColorChangeWorkload> logger)
        {
            _menuColorAccessor = menuColorAccessor;
            _logger = logger;
        }

        private static readonly List<MenuColors> MakoColors = new List<MenuColors>
        {
            new MenuColors
            {
                TopLeft = Color.FromArgb(0,0,90),
                TopRight = Color.FromArgb(0,98,157),
                BotLeft = Color.FromArgb(0,107,98),
                BotRight = Color.FromArgb(0,128,40),
            },
            new MenuColors
            {
                TopLeft = Color.FromArgb(0,0,90),
                TopRight = Color.FromArgb(0,78,127),
                BotLeft = Color.FromArgb(0,87,58),
                BotRight = Color.FromArgb(0,108,40),
            },
            new MenuColors
            {
                TopLeft = Color.FromArgb(0,0,90),
                TopRight = Color.FromArgb(0,78,127),
                BotLeft = Color.FromArgb(0,157,58),
                BotRight = Color.FromArgb(0,128,40),
            },
            new MenuColors
            {
                TopLeft = Color.FromArgb(0,0,90),
                TopRight = Color.FromArgb(0,80,58),
                BotLeft = Color.FromArgb(0,107,78),
                BotRight = Color.FromArgb(0,148,40),
            },
        };

        public void Run()
        {
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    foreach (MenuColors menuColors in MakoColors)
                    {
                        _menuColorAccessor.SetMenuColors(ApplicationSettings.Instance.ProcessName,
                            menuColors);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error changing menu colors.");
            }
        }
    }
}