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

        private CharNames(int id, string defaultName, Func<CommandSettings, IList<string>> wordsSelector)
        {
            _wordsSelector = wordsSelector;
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

        public static CharNames Cloud = new CharNames(1, "Cloud", x => x.CloudCommandWords);
        public static CharNames Tifa = new CharNames(2, "Tifa", x => x.TifaCommandWords);
        public static CharNames Barret = new CharNames(3, "Barret", x => x.BarretCommandWords);
        public static CharNames Aeris = new CharNames(4, "Aeris", x => x.AerisCommandWords);
        public static CharNames Red = new CharNames(5, "Red XIII", x => x.RedCommandWords);
        public static CharNames CaitSith = new CharNames(6, "Cait Sith", x => x.CaitCommandWords);
        public static CharNames Vincent = new CharNames(7, "Vincent", x => x.VincentCommandWords);
        public static CharNames Yuffie = new CharNames(8, "Yuffie", x => x.YuffieCommandWords);
        public static CharNames Cid = new CharNames(9, "Cid", x => x.CidCommandWords);
    }
}