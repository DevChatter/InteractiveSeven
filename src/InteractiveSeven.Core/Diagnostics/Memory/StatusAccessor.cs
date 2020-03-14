using InteractiveSeven.Core.Battle;
using InteractiveSeven.Core.Settings;
using System;
using System.Threading;

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

        public void SetActorStatus(IHasStatus actor, StatusEffects statusEffect)
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

            StatusEffects status = GetTrueStatus(actor);

            status |= statusEffect;

            ApplyFullStatus(actor, status);
        }

        public void ClearNegativeStatuses(IHasStatus actor)
        {
            StatusEffects negativeEffects = StatusEffects.Sleep | StatusEffects.Poison | StatusEffects.Sadness |
                                            StatusEffects.Fury | StatusEffects.Confusion | StatusEffects.Silence |
                                            StatusEffects.Frog | StatusEffects.Small | StatusEffects.Petrify |
                                            StatusEffects.Berserk | StatusEffects.Paralyzed | StatusEffects.Darkness;
            RemoveActorStatus(actor, negativeEffects);
        }


        public bool RemoveActorStatus(IHasStatus actor, StatusEffects statusEffect)
        {
            StatusEffects status = GetTrueStatus(actor);

            if ((status & statusEffect) == 0) // they don't have the status
            {
                return false;
            }

            status &= ~statusEffect;

            ApplyFullStatus(actor, status);

            return true;
        }

        private void ApplyFullStatus(IHasStatus actor, StatusEffects status)
        {
            if ((status & StatusEffects.Manipulate) > 0) // If have manip
            {
                status ^= StatusEffects.Manipulate; // remove manip
            }

            for (int attemptCount = 0; attemptCount < 26; attemptCount++)
            {
                if (attemptCount % 2 == 0)
                {
                    WriteStatus(actor, status | StatusEffects.Manipulate);
                }
                else
                {
                    WriteStatus(actor, status);
                    StatusEffects trueStatus = GetTrueStatus(actor);
                    if (trueStatus == status)
                    {
                        break;
                    }
                }

                Thread.Sleep(100);
            }
        }

        private void WriteStatus(IHasStatus actor, StatusEffects status)
        {
            var bytes = BitConverter.GetBytes((uint)status);
            _memoryAccessor.WriteMem(ProcessName, actor.PrimaryStatus.Address, bytes);
        }

        private StatusEffects GetTrueStatus(IHasStatus actor)
        {
            var bytes = new byte[4];
            _memoryAccessor.ReadMem(ProcessName, actor.SecondaryStatus.Address, bytes);
            uint status = BitConverter.ToUInt32(bytes);
            return (StatusEffects)status;
        }
    }
}