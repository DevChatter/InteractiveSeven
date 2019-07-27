using InteractiveSeven.Core.FinalFantasy;
using InteractiveSeven.Core.Settings;
using System;

namespace InteractiveSeven.Core.Diagnostics.Memory
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
            _memoryAccessor.WriteMem(Settings.ProcessName, Addresses.Gil.Address, BitConverter.GetBytes(gil));
        }
    }
}