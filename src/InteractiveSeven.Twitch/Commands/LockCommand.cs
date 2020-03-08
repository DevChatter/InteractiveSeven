﻿using InteractiveSeven.Twitch.Model;
using InteractiveSeven.Twitch.Payments;

namespace InteractiveSeven.Twitch.Commands
{
    public class LockCommand : BaseCommand
    {
        private readonly PaymentProcessor _paymentProcessor;

        public LockCommand(PaymentProcessor paymentProcessor)
            : base(new[] { "BrendanLock", "I7Lock" }, x => true)
        {
            _paymentProcessor = paymentProcessor;
        }

        public override void Execute(in CommandData commandData)
        {
            if (commandData.User.IsDevChatter || commandData.User.IsShojy)
            {
                _paymentProcessor.Lock();
            }
        }
    }
}