using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.Settings;
using System;
using System.ComponentModel;
using System.Threading;

namespace InteractiveSeven.Core.Moods
{
    public class MoodEnforcer : IDisposable
    {
        private readonly IStatusHubEmitter _statusHubEmitter;
        private Mood _nextMood;
        public Mood CurrentMood { get; private set; }
        private Timer _runTimer;
        private Timer _checkerTimer;

        private MoodSettings MoodSettings => ApplicationSettings.Instance.MoodSettings;

        public MoodEnforcer(IStatusHubEmitter statusHubEmitter)
        {
            _statusHubEmitter = statusHubEmitter;
            MoodSettings.PropertyChanged += MoodSettingsChanged;
        }

        public void ChangeMood(Mood mood)
        {
            _nextMood = mood;
        }

        public void Start()
        {
            if (MoodSettings.Enabled && _runTimer == null && _checkerTimer == null)
            {
                _runTimer = new Timer(DoTheWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));

                _checkerTimer = new Timer(Check, null, TimeSpan.Zero, TimeSpan.FromSeconds(120));
            }
        }

        private void Check(object state)
        {
            // TODO: Check current vote balance and set nextMood.
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
            CurrentMood?.ApplyEffect();

            if (_nextMood != null)
            {
                CurrentMood = _nextMood;
                _nextMood = null;
                _statusHubEmitter.ShowEvent($"{CurrentMood.Name} is now Active.");
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