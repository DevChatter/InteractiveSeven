﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.FinalFantasy.Constants;
using InteractiveSeven.Core.FinalFantasy.Models;
using InteractiveSeven.Core.Payments;
using InteractiveSeven.Core.ViewModels;

namespace InteractiveSeven.Core.Commands.Battle
{
    public class EsunaCommand : BaseCommand
    {
        private readonly IChatClient _chatClient;
        private readonly PartyStatusViewModel _partyStatus;
        private readonly IStatusAccessor _statusAccessor;
        private readonly PaymentProcessor _paymentProcessor;
        private readonly IStatusHubEmitter _statusHubEmitter;

        public EsunaCommand(IChatClient chatClient, PartyStatusViewModel partyStatus,
            IStatusAccessor statusAccessor, PaymentProcessor paymentProcessor,
            IStatusHubEmitter statusHubEmitter)
            : base(x => x.EsunaCommandWords,
                x => x.BattleSettings.AllowEsunaCommand && x.BattleSettings.AllowStatusEffects)
        {
            _chatClient = chatClient;
            _partyStatus = partyStatus;
            _statusAccessor = statusAccessor;
            _paymentProcessor = paymentProcessor;
            _statusHubEmitter = statusHubEmitter;
        }

        public override async Task Execute(CommandData commandData)
        {
            List<Allies> targeted = Allies.ByWord(commandData.Arguments.FirstOrDefault());
            if (!targeted.Any())
            {
                await _chatClient.SendMessage(commandData.Channel, "Be sure to name a valid actor. Example: !esuna top");
                return;
            }

            var validTargets = CheckTargetValidity(targeted);

            if (await CouldNotAfford(validTargets.Count, commandData))
            {
                return;
            }

            foreach (Allies target in validTargets)
            {
                Character character = GetTargetedCharacter(target);
                _statusAccessor.ClearNegativeStatuses(target);
                string message = $"Removed Negative Effects from {character.Name}.";
                await _chatClient.SendMessage(commandData.Channel, message);
                await _statusHubEmitter.ShowEvent(message);
            }
        }

        protected async Task<bool> CouldNotAfford(int targetCount, CommandData commandData)
        {
            GilTransaction gilTransaction = await _paymentProcessor.ProcessPayment(
                commandData, Settings.BattleSettings.EsunaCost * targetCount,
                Settings.BattleSettings.AllowModOverride);

            return !gilTransaction.Paid;
        }

        protected List<Allies> CheckTargetValidity(IEnumerable<Allies> targets)
        {
            return targets.Where(x => GetTargetedCharacter(x).Id != FF7Const.Empty).ToList();
        }

        private Character GetTargetedCharacter(Allies ally)
        {
            return _partyStatus.Party[ally.Index];
        }
    }
}
