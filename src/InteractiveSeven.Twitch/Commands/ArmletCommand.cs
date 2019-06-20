using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Data.Items;
using InteractiveSeven.Core.Memory;
using InteractiveSeven.Core.Model;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Twitch.Model;
using System.Linq;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class ArmletCommand : BaseCommand
    {
        private readonly IEquipmentAccessor _equipmentAccessor;
        private readonly GilBank _gilBank;
        private readonly ITwitchClient _twitchClient;

        public ArmletCommand(IEquipmentAccessor equipmentAccessor,
            GilBank gilBank, ITwitchClient twitchClient)
            : base(x => x.ArmletCommandWords, x => x.EquipmentSettings.Enabled)
        {
            _equipmentAccessor = equipmentAccessor;
            _gilBank = gilBank;
            _twitchClient = twitchClient;
        }

        public override void Execute(CommandData commandData)
        {
            (bool isValidName, CharNames charName) =
                CharNames.GetByName(commandData.Arguments.FirstOrDefault());
            var armletText = commandData.Arguments.ElementAtOrDefault(1);

            if (!isValidName
                || !int.TryParse(armletText ?? "", out int armletId)
                || !Armlets.IsValid(armletId))
            {
                _twitchClient.SendMessage(commandData.Channel,
                    "Invalid Request - Specify character and armlet number like this !armlet cloud 15");
                return;
            }

            if (!CanOverrideBitRestriction(commandData.User))
            {
                const int cost = 100;
                (int balance, int withdrawn) = _gilBank.Withdraw(commandData.User, cost, true);
                if (withdrawn < cost)
                {
                    _twitchClient.SendMessage(commandData.Channel,
                        $"Insufficient gil. You only have {balance} gil and needed {cost}");
                    return;
                }
            }

            Armlets armlet = Armlets.Get(armletId);
            _equipmentAccessor.SetCharacterArmlet(charName, armlet.Value);
        }

        private bool CanOverrideBitRestriction(ChatUser user)
            => (Settings.EquipmentSettings.AllowModOverride && user.IsMod)
               || user.IsMe || user.IsBroadcaster;
    }
}