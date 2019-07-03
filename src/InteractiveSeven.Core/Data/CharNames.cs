using InteractiveSeven.Core.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InteractiveSeven.Core.Data
{
    public class CharNames
    {
        private readonly Func<CommandSettings, IList<string>> _wordsSelector;
        public int Id { get; }
        public string DefaultName { get; }
        public IList<string> Words => _wordsSelector(ApplicationSettings.Instance.CommandSettings);

        public GameMoments AllowNamingAfter { get; }

        private CharNames(int id, string defaultName, Func<CommandSettings, IList<string>> wordsSelector, GameMoments allowNamingAfter)
        {
            _wordsSelector = wordsSelector;
            AllowNamingAfter = allowNamingAfter;
            Id = id;
            DefaultName = defaultName;
            All.Add(this);
        }

        public static readonly List<CharNames> All = new List<CharNames>();

        public static CharNames GetById(int id) => All.SingleOrDefault(x => x.Id == id);

        public static bool TryGetByName(string word, out CharNames charName)
        {
            charName = All.SingleOrDefault(x => x.Words.Any(w => w.EqualsIns(word)));
            return charName != null;
        }

        public static (bool exists, CharNames charName) GetByName(string word)
        {
            CharNames charName = All.SingleOrDefault(x => x.Words.Any(w => w.EqualsIns(word)));
            return (charName != null, charName);
        }

        public static CharNames Cloud = new CharNames(1, "Cloud", x => x.CloudCommandWords, GameMoments.AfterBarretNamed);
        public static CharNames Tifa = new CharNames(2, "Tifa", x => x.TifaCommandWords, GameMoments.AfterBarretNamed);
        public static CharNames Barret = new CharNames(3, "Barret", x => x.BarretCommandWords, GameMoments.AfterBarretNamed);
        public static CharNames Aeris = new CharNames(4, "Aeris", x => x.AerisCommandWords, GameMoments.AfterBarretNamed);
        public static CharNames Red = new CharNames(5, "Red XIII", x => x.RedCommandWords, GameMoments.AfterBarretNamed);
        public static CharNames CaitSith = new CharNames(6, "Cait Sith", x => x.CaitCommandWords, GameMoments.AfterKalmFlashback);
        public static CharNames Vincent = new CharNames(7, "Vincent", x => x.VincentCommandWords, GameMoments.AfterKalmFlashback);
        public static CharNames Yuffie = new CharNames(8, "Yuffie", x => x.YuffieCommandWords, GameMoments.AfterBarretNamed);
        public static CharNames Cid = new CharNames(9, "Cid", x => x.CidCommandWords, GameMoments.AfterBarretNamed);
    }
}