using System;
using System.Threading;
using InteractiveSeven.Core.Commands.Battle;
using InteractiveSeven.Core.Settings;

namespace InteractiveSeven.Core.Diagnostics.Memory
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
            if (statusEffect.HasBlockingOpposite()
                && RemoveActorStatus(actor, statusEffect.GetOpposite()))
            {
                return;
            }

            if (statusEffect.HasOpposite())
            {
                RemoveActorStatus(actor, statusEffect.GetOpposite());
            }

            int status = GetTrueStatus(actor);

            status |= (int)statusEffect;

            ApplyFullStatus(actor, status);
        }

        public void ClearNegativeStatuses(Allies actor)
        {
            StatusEffects negativeEffects = StatusEffects.Sleep | StatusEffects.Poison | StatusEffects.Sadness |
                                            StatusEffects.Fury | StatusEffects.Confusion | StatusEffects.Silence |
                                            StatusEffects.Frog | StatusEffects.Small | StatusEffects.Petrify |
                                            StatusEffects.Berserk | StatusEffects.Paralyzed | StatusEffects.Darkness;
            RemoveActorStatus(actor, negativeEffects);
        }

        public bool RemoveActorStatus(Allies actor, StatusEffects statusEffect)
        {
            int status = GetTrueStatus(actor);

            if ((status & (int)statusEffect) == 0) // they don't have the status
            {
                return false;
            }

            status &= (int)~statusEffect;

            ApplyFullStatus(actor, status);

            return true;
        }

        private void ApplyFullStatus(Allies actor, int status)
        {
            const int manipStatus = 0b_00000000_1000000_00000000_00000000;
            if ((status & manipStatus) == manipStatus) // If have manip
            {
                status ^= manipStatus; // remove manip
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