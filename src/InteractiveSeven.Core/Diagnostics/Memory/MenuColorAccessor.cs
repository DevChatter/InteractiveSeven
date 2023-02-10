using System;
using System.Drawing;
using System.Threading;
using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.FinalFantasy;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Settings;
using Microsoft.Extensions.Logging;

namespace InteractiveSeven.Core.Diagnostics.Memory
{
    public class MenuColorAccessor : IMenuColorAccessor
    {
        private MenuColors _currentColor = MenuColors.Classic;
        private readonly IMemoryAccessor _memoryAccessor;
        private readonly IStatusHubEmitter _statusHubEmitter;
        private readonly ILogger<MenuColorAccessor> _logger;

        public MenuColorAccessor(IMemoryAccessor memoryAccessor,
            IStatusHubEmitter statusHubEmitter, ILogger<MenuColorAccessor> logger)
        {
            _memoryAccessor = memoryAccessor;
            _statusHubEmitter = statusHubEmitter;
            _logger = logger;
        }

        public MenuColors GetMenuColors(string processName)
        {
            byte[] topLeftBuffer = new byte[3];
            byte[] botLeftBuffer = new byte[3];
            byte[] topRightBuffer = new byte[3];
            byte[] botRightBuffer = new byte[3];

            if (_memoryAccessor.ReadMem(processName, Addresses.MenuTopLeft.Address, topLeftBuffer)
                && _memoryAccessor.ReadMem(processName, Addresses.MenuBotLeft.Address, botLeftBuffer)
                && _memoryAccessor.ReadMem(processName, Addresses.MenuTopRight.Address, topRightBuffer)
                && _memoryAccessor.ReadMem(processName, Addresses.MenuBotRight.Address, botRightBuffer))
            {
                return new MenuColors
                {
                    TopLeft = topLeftBuffer.ToColor(),
                    BotLeft = botLeftBuffer.ToColor(),
                    TopRight = topRightBuffer.ToColor(),
                    BotRight = botRightBuffer.ToColor(),
                };
            }

            return _currentColor;
        }

        public void SetMenuColors(string processName, MenuColors menuColors)
        {
            if (ApplicationSettings.Instance.MenuSettings.TransitionColors)
            {
                MenuColors startColor = GetMenuColors(processName);
                MenuColors[] colorSteps = GetColorSteps(startColor, menuColors);
                foreach (var menuColor in colorSteps)
                {
                    UpdateDisplayColors(processName, menuColor);
                    Thread.Sleep(100);
                }
            }
            UpdateDisplayColors(processName, menuColors);

            _memoryAccessor.WriteMem(processName, Addresses.MenuColorAllSave.Address, menuColors.GetSaveBytes());
            _currentColor = menuColors;
        }

        private void UpdateDisplayColors(string processName, MenuColors menuColors)
        {
            _memoryAccessor.WriteMem(
                processName,
                Addresses.MenuColorAll.Address,
                menuColors.GetDisplayBytes());
            _statusHubEmitter.ShowNewColors(menuColors)
                .RunInBackgroundSafely(true,
                    ex => _logger.LogError(ex, "Error Showing New Colors"));
        }

        private MenuColors[] GetColorSteps(MenuColors startColor, MenuColors endingColor)
        {
            const double steps = 19d;
            var colorSteps = new MenuColors[(int)steps];

            var tlIncr = GetRgbIncrements(startColor.TopLeft, endingColor.TopLeft, steps);
            var blIncr = GetRgbIncrements(startColor.BotLeft, endingColor.BotLeft, steps);
            var trIncr = GetRgbIncrements(startColor.TopRight, endingColor.TopRight, steps);
            var brIncr = GetRgbIncrements(startColor.BotRight, endingColor.BotRight, steps);

            for (int i = 0; i < colorSteps.Length; i++)
            {
                colorSteps[i] = new MenuColors
                {
                    TopLeft = CalculateStepColor(startColor.TopLeft, tlIncr.r, tlIncr.g, tlIncr.b, i),
                    BotLeft = CalculateStepColor(startColor.BotLeft, blIncr.r, blIncr.g, blIncr.b, i),
                    TopRight = CalculateStepColor(startColor.TopRight, trIncr.r, trIncr.g, trIncr.b, i),
                    BotRight = CalculateStepColor(startColor.BotRight, brIncr.r, brIncr.g, brIncr.b, i),
                };
            }

            return colorSteps;
        }

        private static Color CalculateStepColor(Color startColor,
            double incrR, double incrG, double incrB, int stepNumber)
        {
            int r = (int)Math.Round(startColor.R + incrR * (stepNumber + 1));
            int g = (int)Math.Round(startColor.G + incrG * (stepNumber + 1));
            int b = (int)Math.Round(startColor.B + incrB * (stepNumber + 1));
            return Color.FromArgb(r, g, b);
        }

        private static (double r, double g, double b)
            GetRgbIncrements(Color startColor, Color endingColor, double steps)
        {
            double r = (endingColor.R - startColor.R) / steps;
            double g = (endingColor.G - startColor.G) / steps;
            double b = (endingColor.B - startColor.B) / steps;
            return (r, g, b);
        }
    }
}