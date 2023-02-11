using System.Collections.Generic;
using System.Linq;
using InteractiveSeven.Core;
using InteractiveSeven.Core.Bidding;
using InteractiveSeven.Core.Bidding.Moods;
using InteractiveSeven.Core.Moods;
using InteractiveSeven.Twitch.Model;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class ChangeMoodCommand : BaseCommand
    {
        private readonly ITwitchClient _twitchClient;
        private readonly MoodBidding _moodBidding;
        private readonly IList<Mood> _moods;

        public ChangeMoodCommand(ITwitchClient twitchClient, MoodBidding moodBidding, IList<Mood> moods)
            : base(x => x.ChangeMoodCommandWords, x => x.MoodSettings.Enabled)
        {
            _twitchClient = twitchClient;
            _moodBidding = moodBidding;
            _moods = moods;
        }

        public override void Execute(in CommandData commandData)
        {
            if (!CanRunThisCommand(commandData)) return;

            (string error, int amount, string moodArg) = ParseArgs(commandData.Arguments);
            if (error != null)
            {
                _twitchClient.SendMessage(commandData.Channel, error);
                return;
            }

            var moods = _moods.Where(x => x.Name.StartsWithIns(moodArg)).ToList();

            if (moods.Count == 0)
            {
                _twitchClient.SendMessage(commandData.Channel, $"Mood: {moodArg} not found.");
                return;
            }

            if (moods.Count > 1)
            {
                _twitchClient.SendMessage(commandData.Channel,
                    $"Error: {moodArg} found {moods.Count} moods - {string.Join(", ", moods.Select(x => x.Name))}");
                return;
            }

            var bidRecord = new BidRecord(commandData.User.Username, commandData.User.UserId, amount);
            Mood mood = moods.Single();
            int total = _moodBidding.AddBid(mood.Id, bidRecord);

            _twitchClient.SendMessage(commandData.Channel, $"Added {bidRecord.Bits} to {mood.Name} for total: {total}");
        }

        private (string error, int amount, string recipient) ParseArgs(IList<string> args)
        {
            var amountArg = args
                .Select(x => new { Amount = x.SafeIntParse(), Raw = x })
                .FirstOrDefault(x => x.Amount > 0);
            if (amountArg == null || amountArg.Amount < 1)
            {
                return ("Please specify an amount to bid.", 0, "");
            }

            string nameArg = args.Except(new[] { amountArg.Raw }).FirstOrDefault();

            if (nameArg == null)
            {
                return ("Please specify a mood name.", 0, "");
            }

            return (null, amountArg.Amount, nameArg);
        }


        private static bool CanRunThisCommand(CommandData commandData)
        {
            return commandData.User.IsBroadcaster
                   || commandData.User.IsDevChatter
                   || commandData.User.IsMe
                   || commandData.User.IsMod;
        }
    }
}