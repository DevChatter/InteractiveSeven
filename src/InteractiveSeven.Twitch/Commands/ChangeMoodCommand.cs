﻿using InteractiveSeven.Core;
using InteractiveSeven.Core.Bidding.Moods;
using InteractiveSeven.Core.Moods;
using InteractiveSeven.Twitch.Model;
using System.Collections.Generic;
using System.Linq;
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

            string nameArg = commandData.Arguments.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(nameArg))
            {
                _twitchClient.SendMessage(commandData.Channel, "Please specify mood name.");
                return;
            }

            var moods = _moods.Where(x => x.Name.StartsWithIns(nameArg)).ToList();

            if (moods.Count == 0)
            {
                _twitchClient.SendMessage(commandData.Channel, $"Mood: {nameArg} not found.");
                return;
            }

            if (moods.Count > 1)
            {
                _twitchClient.SendMessage(commandData.Channel,
                    $"Error: {nameArg} found {moods.Count} moods - {string.Join(", ", moods.Select(x => x.Name))}");
                return;
            }


            _moodBidding.AddBid(new MoodBid(moods.Single().Id));
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