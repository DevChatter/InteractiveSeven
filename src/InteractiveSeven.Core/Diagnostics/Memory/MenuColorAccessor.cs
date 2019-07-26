using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Settings;
using System;
using System.Drawing;
using System.Threading;

namespace InteractiveSeven.Core.Memory
{
    public class MenuColorAccessor : IMenuColorAccessor
    {
        private readonly IMemoryAccessor _memoryAccessor;
        private readonly IMenuHubEmitter _menuHubEmitter;

        public MenuColorAccessor(IMemoryAccessor memoryAccessor, IMenuHubEmitter menuHubEmitter)
        {
            _memoryAccessor = memoryAccessor;
            _menuHubEmitter = menuHubEmitter;
        }

        public MenuColors GetMenuColors(string processName)
        {
            byte[] topLeftBuffer = new byte[3];
            byte[] botLeftBuffer = new byte[3];
            byte[] topRightBuffer = new byte[3];
            byte[] botRightBuffer = new byte[3];

            _memoryAccessor.ReadMem(processName, MemLoc.MenuTopLeft.Address, topLeftBuffer);
            _memoryAccessor.ReadMem(processName, MemLoc.MenuBotLeft.Address, botLeftBuffer);
            _memoryAccessor.ReadMem(processName, MemLoc.MenuTopRight.Address, topRightBuffer);
            _memoryAccessor.ReadMem(processName, MemLoc.MenuBotRight.Address, botRightBuffer);

            return new MenuColors
            {
                TopLeft = topLeftBuffer.ToColor(),
                BotLeft = botLeftBuffer.ToColor(),
                TopRight = topRightBuffer.ToColor(),
                BotRight = botRightBuffer.ToColor(),
            };
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

            _memoryAccessor.WriteMem(processName, MemLoc.MenuColorAllSave.Address, menuColors.GetSaveBytes());
        }

        private void UpdateDisplayColors(string processName, MenuColors menuColors)
        {
            _memoryAccessor.WriteMem(processName, MemLoc.MenuColorAll.Address, menuColors.GetDisplayBytes());
            // TODO: Add logger for exceptions
            _menuHubEmitter.ShowNewColors(menuColors).RunInBackgroundSafely();
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