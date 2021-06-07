using System;
using System.Threading;
using System.Threading.Tasks;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.FinalFantasy;
using Microsoft.Extensions.Hosting;

namespace DevChatter.InteractiveGames.Core.Nine
{
    public class MonitoringService : BackgroundService
    {
        private readonly IMemoryAccessor _memoryAccessor;

        public MonitoringService(IMemoryAccessor memoryAccessor)
        {
            _memoryAccessor = memoryAccessor;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                byte[] bytes = _memoryAccessor.ReadMem("FF9", FF9Addresses.Gil);
                uint gil = BitConverter.ToUInt32(bytes);

            }

            throw new System.NotImplementedException();
        }
    }
}
