namespace Tseng.Constants
{
    public static class Addresses
    {
        public const int SaveMapStart = 0xDBFD38;
        public const int ActiveBattleState = 0x9A8AF8;
        public const int BattleMapStart = 0x9AB0DC;
        
        // Live window color data - both this and save map are used at different times
        public const int WindowColorBlockStart = 0x91EFC8;
        public const int WindowColorTopLeft = 0x91EFC8;
        public const int WindowColorTopRight = 0x91EFCB;
        public const int WindowColorBottomLeft = 0x91EFCE;
        public const int WindowColorBottomRight = 0x91EFD1;
    }
}