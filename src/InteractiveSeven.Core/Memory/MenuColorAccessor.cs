using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Settings;

namespace InteractiveSeven.Core.Memory
{
    public class MenuColorAccessor : IMenuColorAccessor
    {
        private readonly IMemoryAccessor _memoryAccessor;

        public MenuColorAccessor(IMemoryAccessor memoryAccessor)
        {
            _memoryAccessor = memoryAccessor;
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
                    _memoryAccessor.WriteMem(processName, MemLoc.MenuColorAll.Address, menuColor.GetDisplayBytes());
                    Thread.Sleep(100);
                }
            }
            _memoryAccessor.WriteMem(processName, MemLoc.MenuColorAll.Address, menuColors.GetDisplayBytes());

            _memoryAccessor.WriteMem(processName, MemLoc.MenuColorAllSave.Address, menuColors.GetSaveBytes());
        }

        private MenuColors[] GetColorSteps(MenuColors startColor, MenuColors endingColor)
        {
            const double steps = 19d;
            var colorSteps = new MenuColors[(int)steps];

            var tlrInc = (endingColor.TopLeft.R - startColor.TopLeft.R) / steps;
            var tlgInc = (endingColor.TopLeft.G - startColor.TopLeft.G) / steps;
            var tlbInc = (endingColor.TopLeft.B - startColor.TopLeft.B) / steps;

            var blrInc = (endingColor.BotLeft.R - startColor.BotLeft.R) / steps;
            var blgInc = (endingColor.BotLeft.G - startColor.BotLeft.G) / steps;
            var blbInc = (endingColor.BotLeft.B - startColor.BotLeft.B) / steps;

            var trrInc = (endingColor.TopRight.R - startColor.TopRight.R) / steps;
            var trgInc = (endingColor.TopRight.G - startColor.TopRight.G) / steps;
            var trbInc = (endingColor.TopRight.B - startColor.TopRight.B) / steps;

            var brrInc = (endingColor.BotRight.R - startColor.BotRight.R) / steps;
            var brgInc = (endingColor.BotRight.G - startColor.BotRight.G) / steps;
            var brbInc = (endingColor.BotRight.B - startColor.BotRight.B) / steps;

            for (int i = 0; i < colorSteps.Length; i++)
            {
                int tlr = (int)Math.Round(startColor.TopLeft.R + tlrInc * (i + 1));
                int tlg = (int)Math.Round(startColor.TopLeft.G + tlgInc * (i + 1));
                int tlb = (int)Math.Round(startColor.TopLeft.B + tlbInc * (i + 1));

                int blr = (int)Math.Round(startColor.BotLeft.R + blrInc * (i + 1));
                int blg = (int)Math.Round(startColor.BotLeft.G + blgInc * (i + 1));
                int blb = (int)Math.Round(startColor.BotLeft.B + blbInc * (i + 1));

                int trr = (int)Math.Round(startColor.TopRight.R + trrInc * (i + 1));
                int trg = (int)Math.Round(startColor.TopRight.G + trgInc * (i + 1));
                int trb = (int)Math.Round(startColor.TopRight.B + trbInc * (i + 1));

                int brr = (int)Math.Round(startColor.BotRight.R + brrInc * (i + 1));
                int brg = (int)Math.Round(startColor.BotRight.G + brgInc * (i + 1));
                int brb = (int)Math.Round(startColor.BotRight.B + brbInc * (i + 1));

                colorSteps[i] = new MenuColors
                {
                    TopLeft = Color.FromArgb(tlr, tlg, tlb),
                    BotLeft = Color.FromArgb(blr, blg, blb),
                    TopRight = Color.FromArgb(trr, trg, trb),
                    BotRight = Color.FromArgb(brr, brg, brb),
                };
            }

            return colorSteps;
        }
    }
}