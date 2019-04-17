namespace InteractiveSeven.UI.Models
{
    public class MenuCornerColor
    {
        private MenuCornerColor(byte blue, byte green, byte red)
        {
            Blue = blue;
            Green = green;
            Red = red;
        }

        /// <summary>
        /// Create from direct memory read in FF7.
        /// </summary>
        /// <param name="bytes">Corner of 3 bytes in order Blue, Green, Red.</param>
        public MenuCornerColor(byte[] bytes) 
            : this(bytes[0], bytes[1], bytes[2])
        {
        }

        /// <summary>
        /// Create from int number strings.
        /// </summary>
        /// <param name="blue"></param>
        /// <param name="green"></param>
        /// <param name="red"></param>
        public MenuCornerColor(string blue, string green, string red) 
            : this(blue.ToByte(), green.ToByte(), red.ToByte())
        {
        }

        public byte Blue { get; set; }
        public byte Green { get; set; }
        public byte Red { get; set; }

        public byte[] AsArray()
        {
            return new[] { Blue, Green, Red };
        }
    }
}