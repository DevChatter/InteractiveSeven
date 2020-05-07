using InteractiveSeven.Core.FinalFantasy;
using InteractiveSeven.Core.Settings;
using Microsoft.Extensions.Logging;
using System;

namespace InteractiveSeven.Core.Diagnostics.Memory
{
    public class GpAccessor : IGpAccessor
    {
        private readonly IMemoryAccessor _memoryAccessor;
        private readonly ILogger<GpAccessor> _logger;
        private ApplicationSettings Settings => ApplicationSettings.Instance;
        private static readonly object Padlock = new object();

        public GpAccessor(IMemoryAccessor memoryAccessor, ILogger<GpAccessor> logger)
        {
            _memoryAccessor = memoryAccessor;
            _logger = logger;
        }

        public ushort GetGp()
        {
            try
            {
                lock (Padlock)
                {
                    var buffer = new byte[Addresses.Gp.NumBytes];
                    _memoryAccessor.ReadMem(Settings.ProcessName, Addresses.Gp.Address, buffer);
                    return BitConverter.ToUInt16(buffer);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error setting player GP.");
            }

            return 0;
        }

        public void AddGp(in ushort amount)
        {
            try
            {
                lock (Padlock)
                {
                    var buffer = new byte[Addresses.Gp.NumBytes];
                    _memoryAccessor.ReadMem(Settings.ProcessName, Addresses.Gp.Address, buffer);
                    ushort currentBalance = BitConverter.ToUInt16(buffer);
                    currentBalance += amount;
                    _memoryAccessor.WriteMem(Settings.ProcessName, Addresses.Gp.Address,
                        BitConverter.GetBytes(currentBalance));
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error adding GP to player.");
            }
        }

        public void RemoveGp(ushort amount)
        {
            try
            {
                lock (Padlock)
                {
                    var buffer = new byte[Addresses.Gp.NumBytes];
                    _memoryAccessor.ReadMem(Settings.ProcessName, Addresses.Gp.Address, buffer);
                    ushort currentBalance = BitConverter.ToUInt16(buffer);
                    if (currentBalance >= amount)
                    {
                        currentBalance -= amount;
                    }
                    else
                    {
                        currentBalance = 0;
                    }
                    _memoryAccessor.WriteMem(Settings.ProcessName, Addresses.Gp.Address,
                        BitConverter.GetBytes(currentBalance));
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error adding GP to player.");
            }
        }
    }
}