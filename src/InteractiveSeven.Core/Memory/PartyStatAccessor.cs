using InteractiveSeven.Core.Models;
using System.Collections.Generic;

namespace InteractiveSeven.Core.Memory
{
    public class PartyStatAccessor
    {
        private readonly IMemoryAccessor _memoryAccessor;

        public PartyStatAccessor(IMemoryAccessor memoryAccessor)
        {
            _memoryAccessor = memoryAccessor;
        }

        public List<PartyStat> GetPartyStats(string processName)
        {
            byte[] bytes = new byte[MemLoc.CloudName.NumBytes];
            _memoryAccessor.ReadMem(processName, MemLoc.CloudName.Address, bytes);
            string name = bytes.MapFf7BytesToString();

            return new List<PartyStat> { new PartyStat { Name = name } };
        }
    }
}