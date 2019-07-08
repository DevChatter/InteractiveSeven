﻿using InteractiveSeven.Core.Settings;
using System;

namespace InteractiveSeven.Core.Memory
{
    public class GilAccessor : IGilAccessor
    {
        private readonly IMemoryAccessor _memoryAccessor;
        private ApplicationSettings Settings => ApplicationSettings.Instance;

        public GilAccessor(IMemoryAccessor memoryAccessor)
        {
            _memoryAccessor = memoryAccessor;
        }

        public void SetGil(int gil)
        {
            _memoryAccessor.WriteMem(Settings.ProcessName, MemLoc.Gil.Address, BitConverter.GetBytes(gil));
        }
    }
}