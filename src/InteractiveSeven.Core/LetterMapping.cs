using System.Collections.Generic;
using System.Linq;

namespace InteractiveSeven.Core
{
    public static class LetterMapping
    {
        private const int MAX_CHARS = 10;

        private static readonly Dictionary<char,byte> CharToByteMap = new Dictionary<char, byte>
        {
            [' '] = 0,
            ['!'] = 1,
            ['"'] = 2,
            ['#'] = 3,
            ['$'] = 4,
            ['%'] = 5,
            ['&'] = 6,
            ['('] = 8,
            [')'] = 9,
            ['*'] = 10,
            ['+'] = 11,
            [','] = 12,
            ['-'] = 13,
            ['.'] = 14,
            ['0'] = 16,
            ['1'] = 17,
            ['2'] = 18,
            ['3'] = 19,
            ['4'] = 20,
            ['5'] = 21,
            ['6'] = 22,
            ['7'] = 23,
            ['8'] = 24,
            ['9'] = 25,
            [':'] = 26,
            [';'] = 27,
            ['<'] = 28,
            ['='] = 29,
            ['>'] = 30,
            ['?'] = 31,
            ['@'] = 32,
            ['A'] = 33,
            ['B'] = 34,
            ['C'] = 35,
            ['D'] = 36,
            ['E'] = 37,
            ['F'] = 38,
            ['G'] = 39,
            ['H'] = 40,
            ['I'] = 41,
            ['J'] = 42,
            ['K'] = 43,
            ['L'] = 44,
            ['M'] = 45,
            ['N'] = 46,
            ['O'] = 47,
            ['P'] = 48,
            ['Q'] = 49,
            ['R'] = 50,
            ['S'] = 51,
            ['T'] = 52,
            ['U'] = 53,
            ['V'] = 54,
            ['W'] = 55,
            ['X'] = 56,
            ['Y'] = 57,
            ['Z'] = 58,
            ['['] = 59,
            [']'] = 61,
            ['^'] = 62,
            ['_'] = 63,
            ['a'] = 65,
            ['b'] = 66,
            ['c'] = 67,
            ['d'] = 68,
            ['e'] = 69,
            ['f'] = 70,
            ['g'] = 71,
            ['h'] = 72,
            ['i'] = 73,
            ['j'] = 74,
            ['k'] = 75,
            ['l'] = 76,
            ['m'] = 77,
            ['n'] = 78,
            ['o'] = 79,
            ['p'] = 80,
            ['q'] = 81,
            ['r'] = 82,
            ['s'] = 83,
            ['t'] = 84,
            ['u'] = 85,
            ['v'] = 86,
            ['w'] = 87,
            ['x'] = 88,
            ['y'] = 89,
            ['z'] = 90,
        };

        private static readonly Dictionary<byte, char> ByteToCharMap = new Dictionary<byte, char>
        {
            [0] = ' ',
            [1] = '!',
            [2] = '"',
            [3] = '#',
            [4] = '$',
            [5] = '%',
            [6] = '&',
            [8] = '(',
            [9] = ')',
            [10] = '*',
            [11] = '+',
            [12] = ',',
            [13] = '-',
            [14] = '.',
            [16] = '0',
            [17] = '1',
            [18] = '2',
            [19] = '3',
            [20] = '4',
            [21] = '5',
            [22] = '6',
            [23] = '7',
            [24] = '8',
            [25] = '9',
            [26] = ':',
            [27] = ';',
            [28] = '<',
            [29] = '=',
            [30] = '>',
            [31] = '?',
            [32] = '@',
            [33] = 'A',
            [34] = 'B',
            [35] = 'C',
            [36] = 'D',
            [37] = 'E',
            [38] = 'F',
            [39] = 'G',
            [40] = 'H',
            [41] = 'I',
            [42] = 'J',
            [43] = 'K',
            [44] = 'L',
            [45] = 'M',
            [46] = 'N',
            [47] = 'O',
            [48] = 'P',
            [49] = 'Q',
            [50] = 'R',
            [51] = 'S',
            [52] = 'T',
            [53] = 'U',
            [54] = 'V',
            [55] = 'W',
            [56] = 'X',
            [57] = 'Y',
            [58] = 'Z',
            [59] = '[',
            [61] = ']',
            [62] = '^',
            [63] = '_',
            [65] = 'a',
            [66] = 'b',
            [67] = 'c',
            [68] = 'd',
            [69] = 'e',
            [70] = 'f',
            [71] = 'g',
            [72] = 'h',
            [73] = 'i',
            [74] = 'j',
            [75] = 'k',
            [76] = 'l',
            [77] = 'm',
            [78] = 'n',
            [79] = 'o',
            [80] = 'p',
            [81] = 'q',
            [82] = 'r',
            [83] = 's',
            [84] = 't',
            [85] = 'u',
            [86] = 'v',
            [87] = 'w',
            [88] = 'x',
            [89] = 'y',
            [90] = 'z',
        };

        public static string MapFf7BytesToString(this byte[] bytes)
        {
            var chars = new char[MAX_CHARS];
            for (int i = 0; i < MAX_CHARS; i++)
            {
                if (!ByteToCharMap.TryGetValue(bytes.ElementAtOrDefault(i), out chars[i]))
                {
                    break;
                }
            }
            return new string(chars.TakeWhile(x => x != (char)255).ToArray());
        }

        public static byte[] MapStringToFf7Bytes(this string text)
        {
            var bytes = new byte[MAX_CHARS];
            for (int i = 0; i < text.Length; i++)
            {
                if (!CharToByteMap.TryGetValue(text.ElementAtOrDefault(i), out bytes[i]))
                {
                    break;
                }
            }
            for (int i = text.Length; i < MAX_CHARS; i++)
            {
                bytes[i] = 255;
            }
            return bytes;
        }
    }
}