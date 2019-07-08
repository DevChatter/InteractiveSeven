using InteractiveSeven.Core.Battle;
using InteractiveSeven.Core.Settings;
using System;
using System.Threading;

namespace InteractiveSeven.Core.Memory
{
    public class StatusAccessor : IStatusAccessor
    {
        private readonly IMemoryAccessor _memoryAccessor;

        private string ProcessName => ApplicationSettings.Instance.ProcessName;

        public StatusAccessor(IMemoryAccessor memoryAccessor)
        {
            _memoryAccessor = memoryAccessor;
        }

        public void SetActorStatus(Allies actor, StatusEffects statusEffect)
        {
            int status = GetTrueStatus(actor);

            status += (int)statusEffect;

            const int manipStatus = 0b_00000000_1000000_00000000_00000000;
            if ((status & manipStatus) == manipStatus‬‬) // If have manip
            {
                status -= manipStatus; // remove manip
            }

            for (int attemptCount = 0; attemptCount < 26; attemptCount++)
            {
                if (attemptCount % 2 == 0)
                {
                    WriteStatus(actor, status + manipStatus);
                }
                else
                {
                    WriteStatus(actor, status);
                    int trueStatus = GetTrueStatus(actor);
                    if (trueStatus == status)
                    {
                        break;
                    }
                }
                Thread.Sleep(100);
            }
        }

        private void WriteStatus(Allies actor, int status)
        {
            var bytes = BitConverter.GetBytes(status);
            _memoryAccessor.WriteMem(ProcessName, actor.PrimaryStatus.Address, bytes);
        }

        private int GetTrueStatus(Allies actor)
        {
            var bytes = new byte[4];
            _memoryAccessor.ReadMem(ProcessName, actor.SecondaryStatus.Address, bytes);
            int status = BitConverter.ToInt32(bytes);
            return status;
        }
    }
}