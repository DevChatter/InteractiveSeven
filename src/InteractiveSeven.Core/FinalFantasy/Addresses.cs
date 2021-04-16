using InteractiveSeven.Core.Diagnostics.Memory;

namespace InteractiveSeven.Core.FinalFantasy
{
    public static class Addresses
    {
        public static readonly MemLoc MenuColorAll = new(0x91EFC8, 16); // 4 bytes each in order blue, green, red, 128
        public static readonly MemLoc MenuTopLeft = new(0x91EFC8, 3); // order blue, green, red
        public static readonly MemLoc MenuBotLeft = new(0x91EFCC, 3); // order blue, green, red
        public static readonly MemLoc MenuTopRight = new(0x91EFD0, 3); // order blue, green, red
        public static readonly MemLoc MenuBotRight = new(0x91EFD4, 3); // order blue, green, red

        public static readonly MemLoc MenuColorAllSave = new(0x91EFD8, 12); // order red, green, blue
        public static readonly MemLoc MenuTopLeftSave = new(0x91EFD8, 3); // order red, green, blue
        public static readonly MemLoc MenuBotLeftSave = new(0x91EFDB, 3); // order red, green, blue
        public static readonly MemLoc MenuTopRightSave = new(0x91EFDE, 3); // order red, green, blue
        public static readonly MemLoc MenuBotRightSave = new(0x91EFE1, 3); // order red, green, blue

        // GS Offset 7F2D C9AC
        // GS XP After Battles = 8009D7D8
        // GS Gil = 8009D260

        // Guess for XP after battle = DC 0E2C

        public static readonly MemLoc Gil = new(0xDC08B4, 4); // 4 bytes
        public static readonly MemLoc Gp = new(0xDC0A26, 2); // Max of 10,000?
        public static readonly MemLoc GameMoment = new(0xDC08DC, 2);

        public static readonly MemLoc MenuVisibility = new(0xDC08F8, 2);
        public static readonly MemLoc MenuLock = new(0xDC08FA, 2);

        public static readonly MemLoc SaveMapStart = new(0xDBFD38, 4342);
        public static readonly MemLoc BattleMapStart = new(0x9AB0DC, 1872);
        public static readonly MemLoc ActiveBattleState = new(0x9A8AF8);
        public static readonly MemLoc SceneMapStart = new(0x9A8E9C, 552);

        // Battle Rewards
        public static readonly MemLoc BattleExperienceGain = new(0xDC0E2C, 2);
        public static readonly MemLoc BattleApGain = new(0xDC0E30, 2);

        public static readonly MemLoc RewardItem1 = new(0x99E2F0, 2);
        public static readonly MemLoc RewardItem1Amount = new(0x99E2F2, 2);

        public static readonly MemLoc RewardItem2 = new(0x99E2F6, 2);
        public static readonly MemLoc RewardItem2Amount = new(0x99E2F8, 2);

        public static readonly MemLoc RewardItem3 = new(0x99E2FC, 2);
        public static readonly MemLoc RewardItem3Amount = new(0x99E2FE, 2);

        public static readonly MemLoc RewardItem4 = new(0x99E302, 2);
        public static readonly MemLoc RewardItem4Amount = new(0x99E304, 2);
    }
}
