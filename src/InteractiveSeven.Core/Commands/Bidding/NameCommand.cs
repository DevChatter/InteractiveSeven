using System.Linq;
using System.Threading.Tasks;
using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Events;
using InteractiveSeven.Core.Payments;
using InteractiveSeven.Core.Settings;

namespace InteractiveSeven.Core.Commands.Bidding
{
    public class NameCommand : BaseCommand
    {
        private readonly GilBank _gilBank;

        private static CommandSettings CmdSettings => ApplicationSettings.Instance.CommandSettings;

        private static string[] AllWords(CommandSettings settings) =>
            settings.CloudCommandWords
                .Union(settings.BarretCommandWords)
                .Union(settings.TifaCommandWords)
                .Union(settings.AerisCommandWords)
                .Union(settings.CaitCommandWords)
                .Union(settings.CidCommandWords)
                .Union(settings.RedCommandWords)
                .Union(settings.VincentCommandWords)
                .Union(settings.YuffieCommandWords)
                .ToArray();

        public NameBiddingSettings NameBidSettings => ApplicationSettings.Instance.NameBiddingSettings;

        public NameCommand(GilBank gilBank)
            : base(AllWords, x => x.NameBiddingSettings.Enabled)
        {
            _gilBank = gilBank;
        }

        public override GamePlayEffects GamePlayEffects => GamePlayEffects.MildEffect;

        public override async Task Execute(CommandData data, IChatClient chatClient)
        {
            if (ShouldTriggerFor(data, CmdSettings.CloudCommandWords, NameBidSettings.NamingCloudEnabled))
            {
                await TriggerDomainEvent(CharNames.Cloud, data, chatClient);
            }
            else if (ShouldTriggerFor(data, CmdSettings.BarretCommandWords, NameBidSettings.NamingBarretEnabled))
            {
                await TriggerDomainEvent(CharNames.Barret, data, chatClient);
            }
            else if (ShouldTriggerFor(data, CmdSettings.TifaCommandWords, NameBidSettings.NamingTifaEnabled))
            {
                await TriggerDomainEvent(CharNames.Tifa, data, chatClient);
            }
            else if (ShouldTriggerFor(data, CmdSettings.AerisCommandWords, NameBidSettings.NamingAerisEnabled))
            {
                await TriggerDomainEvent(CharNames.Aeris, data, chatClient);
            }
            else if (ShouldTriggerFor(data, CmdSettings.CaitCommandWords, NameBidSettings.NamingCaitSithEnabled))
            {
                await TriggerDomainEvent(CharNames.CaitSith, data, chatClient);
            }
            else if (ShouldTriggerFor(data, CmdSettings.CidCommandWords, NameBidSettings.NamingCidEnabled))
            {
                await TriggerDomainEvent(CharNames.Cid, data, chatClient);
            }
            else if (ShouldTriggerFor(data, CmdSettings.RedCommandWords, NameBidSettings.NamingRedEnabled))
            {
                await TriggerDomainEvent(CharNames.Red, data, chatClient);
            }
            else if (ShouldTriggerFor(data, CmdSettings.VincentCommandWords, NameBidSettings.NamingVincentEnabled))
            {
                await TriggerDomainEvent(CharNames.Vincent, data, chatClient);
            }
            else if (ShouldTriggerFor(data, CmdSettings.YuffieCommandWords, NameBidSettings.NamingYuffieEnabled))
            {
                await TriggerDomainEvent(CharNames.Yuffie, data, chatClient);
            }
        }

        private static bool ShouldTriggerFor(in CommandData commandData, string[] words, bool enabled)
        {
            string commandText = commandData.CommandText;
            return words.Any(word => word.EqualsIns(commandText)) && enabled;
        }

        private async Task TriggerDomainEvent(CharNames charName, CommandData data, IChatClient chatClient)
        {
            int gil = GetGilFromCommandData(data);

            if (gil < 1)
            {
                await chatClient.SendMessage(data.Channel, $"Be sure to include a gil amount in your name bid, {data.User.Username}");
                return;
            }

            if (!CanOverrideBitRestriction(data.User))
            {
                (int balance, int withdrawn) = _gilBank.Withdraw(data.User, gil, true);
                if (withdrawn == 0)
                {
                    string message = $"You don't have {gil} gil, {data.User.Username}. You have {balance} gil.";
                    await chatClient.SendMessage(data.Channel, message);
                    return;
                }
            }

            string newName = data.Arguments.FirstOrDefault() ?? "";

            var bidRecord = new BidRecord(data.User.Username, data.User.UserId, gil);
            var domainEvent = new NameVoteReceived(charName, newName, bidRecord);
            DomainEvents.Raise(domainEvent);
        }

        private static int GetGilFromCommandData(in CommandData data)
        {
            int gil = data.Arguments.Count > 1
                ? data.Arguments.Skip(1).Max(arg => arg.SafeIntParse())
                : 0;
            if (gil == 0 && data.Bits > 0) // If their cheer is the bits amount
            {
                gil = data.Bits;
            }

            return gil;
        }

        private bool CanOverrideBitRestriction(in ChatUser user)
            => (NameBidSettings.AllowModBits && user.IsMod) || user.IsMe || user.IsBroadcaster;

    }
}
