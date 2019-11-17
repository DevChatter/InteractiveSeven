using InteractiveSeven.Core.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using InteractiveSeven.Core.FinalFantasy;

namespace InteractiveSeven.Core.Data
{
    public class CharNames
    {
        private readonly Func<CommandSettings, IList<string>> _wordsSelector;
        public int Id { get; }
        public string DefaultName { get; }
        public int SaveMapRecordOffset { get; }
        public string SanitizedDefaultName => DefaultName.ToLower().Replace(' ', '-');
        public IList<string> Words => _wordsSelector(ApplicationSettings.Instance.CommandSettings);

        public GameMoments AllowNamingAfter { get; }

        private CharNames(int id, string defaultName, Func<CommandSettings, IList<string>> wordsSelector,
            int saveMapRecordOffset,
            GameMoments allowNamingAfter = GameMoments.AfterBarretNamed,
            bool included = true)
        {
            _wordsSelector = wordsSelector;
            AllowNamingAfter = allowNamingAfter;
            Id = id;
            DefaultName = defaultName;
            SaveMapRecordOffset = saveMapRecordOffset;
            if (included)
            {
                All.Add(this);
            }
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

        public static CharNames Cloud = new CharNames(0x0, "Cloud", x => x.CloudCommandWords, SaveMapOffsets.CloudRecord);
        public static CharNames Barret = new CharNames(0x1, "Barret", x => x.BarretCommandWords, SaveMapOffsets.BarretRecord);
        public static CharNames Tifa = new CharNames(0x2, "Tifa", x => x.TifaCommandWords, SaveMapOffsets.TifaRecord);
        public static CharNames Aeris = new CharNames(0x3, "Aeris", x => x.AerisCommandWords, SaveMapOffsets.AerisRecord);
        public static CharNames Red = new CharNames(0x4, "Red XIII", x => x.RedCommandWords, SaveMapOffsets.RedXIIIRecord);
        public static CharNames Yuffie = new CharNames(0x5, "Yuffie", x => x.YuffieCommandWords, SaveMapOffsets.YuffieRecord);
        public static CharNames CaitSith = new CharNames(0x6, "Cait Sith", x => x.CaitCommandWords, SaveMapOffsets.CaitSithRecord, GameMoments.AfterKalmFlashback);
        public static CharNames Vincent = new CharNames(0x7, "Vincent", x => x.VincentCommandWords, SaveMapOffsets.VincentRecord, GameMoments.AfterKalmFlashback);
        public static CharNames Cid = new CharNames(0x8, "Cid", x => x.CidCommandWords, SaveMapOffsets.CidRecord);

        // TODO: Exclude these from most stuff we do. They're non-standard.
        public static CharNames YoungCloud = new CharNames(0x9, "Young Cloud", x => new List<string>(), SaveMapOffsets.YoungCloudRecord, included: false);
        public static CharNames Sephiroth = new CharNames(0xA, "Sephiroth", x => new List<string>(), SaveMapOffsets.SephirothRecord, included: false);
        public static CharNames Chocobo = new CharNames(0xB, "Chocobo", x => new List<string>(), SaveMapOffsets.Invalid, included: false);
        public static CharNames None = new CharNames(0xFF, "", x => new List<string>(), SaveMapOffsets.Invalid, included: false);
    }
}