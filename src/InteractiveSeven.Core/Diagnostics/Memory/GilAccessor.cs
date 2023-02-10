using System;
using InteractiveSeven.Core.FinalFantasy;
using InteractiveSeven.Core.Settings;
using Microsoft.Extensions.Logging;

namespace InteractiveSeven.Core.Diagnostics.Memory
{
    public class GilAccessor : IGilAccessor
    {
        private readonly IMemoryAccessor _memoryAccessor;
        private readonly ILogger<GilAccessor> _logger;
        private ApplicationSettings Settings => ApplicationSettings.Instance;
        private static readonly object Padlock = new object();

        public GilAccessor(IMemoryAccessor memoryAccessor, ILogger<GilAccessor> logger)
        {
            _memoryAccessor = memoryAccessor;
            _logger = logger;
        }

        public void SetGil(int gil)
        {
            try
            {
                lock (Padlock)
                {
                    _memoryAccessor.WriteMem(Settings.ProcessName, Addresses.Gil.Address,
                        BitConverter.GetBytes(gil));
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error setting player gil.");
            }
        }

        public uint GetGil()
        {
            try
            {
                lock (Padlock)
                {
                    var buffer = new byte[Addresses.Gil.NumBytes];
                    _memoryAccessor.ReadMem(Settings.ProcessName, Addresses.Gil.Address, buffer);
                    return BitConverter.ToUInt32(buffer);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error setting player gil.");
            }

            return 0;
        }

        public void AddGil(in uint amount)
        {
            try
            {
                lock (Padlock)
                {
                    var buffer = new byte[Addresses.Gil.NumBytes];
                    _memoryAccessor.ReadMem(Settings.ProcessName, Addresses.Gil.Address, buffer);
                    uint currentBalance = BitConverter.ToUInt32(buffer);
                    currentBalance += amount;
                    _memoryAccessor.WriteMem(Settings.ProcessName, Addresses.Gil.Address,
                        BitConverter.GetBytes(currentBalance));
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error adding gil to player.");
            }
        }

        public void RemoveGil(uint amount)
        {
            try
            {
                lock (Padlock)
                {
                    var buffer = new byte[Addresses.Gil.NumBytes];
                    _memoryAccessor.ReadMem(Settings.ProcessName, Addresses.Gil.Address, buffer);
                    uint currentBalance = BitConverter.ToUInt32(buffer);
                    if (currentBalance >= amount)
                    {
                        currentBalance -= amount;
                    }
                    else
                    {
                        currentBalance = 0;
                    }
                    _memoryAccessor.WriteMem(Settings.ProcessName, Addresses.Gil.Address,
                        BitConverter.GetBytes(currentBalance));
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error adding gil to player.");
            }
        }
    }
}