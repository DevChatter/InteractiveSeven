using System.Collections.Generic;
using System.Threading;
using InteractiveSeven.Core.Settings;

namespace InteractiveSeven.Core.Diagnostics.Memory
{
    public class MemoryFreezer
    {
        private readonly IMemoryAccessor _memoryAccessor;
        private ApplicationSettings Settings => ApplicationSettings.Instance;

        private readonly List<Freeze> _frozenValues = new List<Freeze>();

        private bool _isFrozen = true;

        public MemoryFreezer(IMemoryAccessor memoryAccessor)
        {
            _memoryAccessor = memoryAccessor;
        }

        public void AddValue(MemLoc memLoc, byte[] value)
        {
            _frozenValues.Add(new Freeze(memLoc, value));
        }

        public void ThreadStart()
        {
            while (_isFrozen)
            {
                Thread.Sleep(10);
                foreach (Freeze freeze in _frozenValues)
                {
                    _memoryAccessor.WriteMem(Settings.ProcessName, freeze.MemLoc.Address, freeze.Value);
                }
            }
        }

        public void Thaw()
        {
            _isFrozen = false;
        }
    }

    record Freeze(MemLoc MemLoc, byte[] Value);
}
