using InteractiveSeven.Core.Bidding.Moods;
using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;

namespace InteractiveSeven.Core.Moods
{
    public class MoodEnforcer : IDisposable
    {
        private const int MinutesBetweenMoodChanges = 2;
        private readonly IStatusHubEmitter _statusHubEmitter;
        private readonly MoodBidding _moodBidding;
        public Mood CurrentMood { get; private set; }
        private Timer _runTimer;
        private Timer _checkerTimer;
        private DateTime _nextMoodChange;
        private readonly IList<Mood> _moods;

        private readonly object _padlock = new object();

        private MoodSettings MoodSettings => ApplicationSettings.Instance.MoodSettings;

        public MoodEnforcer(IStatusHubEmitter statusHubEmitter, MoodBidding moodBidding, IList<Mood> moods)
        {
            _statusHubEmitter = statusHubEmitter;
            _moodBidding = moodBidding;
            _moods = moods;
            MoodSettings.PropertyChanged += MoodSettingsChanged;
        }

        public void Start()
        {
            _nextMoodChange = DateTime.UtcNow;
            if (MoodSettings.Enabled && _runTimer == null && _checkerTimer == null)
            {
                _runTimer = new Timer(DoTheWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));

                _checkerTimer = new Timer(Check, null, TimeSpan.Zero, TimeSpan.FromSeconds(20));
            }
        }

        private void Check(object _)
        {
            if (_nextMoodChange < DateTime.UtcNow)
            {
                lock (_padlock)
                {
                    int topMoodId = _moodBidding.GetTopMoodId();
                    if (topMoodId != CurrentMood?.Id)
                    {
                        var previousMood = CurrentMood;
                        CurrentMood = _moods.SingleOrDefault(x => x.Id == topMoodId);
                        previousMood?.RemoveEffect();
                        CurrentMood?.ApplyEffect();
                        if (CurrentMood != null)
                        {
                            _moodBidding.ResetBids(topMoodId);
                            _statusHubEmitter.ShowEvent($"{CurrentMood.Name} Starting",
                                $"Next Mood in {MinutesBetweenMoodChanges} minutes.");
                        }
                    }
                    _nextMoodChange = DateTime.UtcNow.AddMinutes(MinutesBetweenMoodChanges);
                }
            }
        }

        private void MoodSettingsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MoodSettings.Enabled))
            {
                if (MoodSettings.Enabled)
                {
                    Start();
                }
                else
                {
                    Stop();
                }
            }
        }

        private void DoTheWork(object _)
        {
            lock (_padlock)
            {
                CurrentMood?.ApplyEffect();
            }
        }

        public void Stop()
        {
            _runTimer?.Dispose();
            _checkerTimer?.Dispose();
        }

        public void Dispose()
        {
            Stop();
            MoodSettings.PropertyChanged -= MoodSettingsChanged;
        }
    }
}
