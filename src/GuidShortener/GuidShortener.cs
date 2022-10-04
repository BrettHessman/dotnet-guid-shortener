using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace GuidShortener
{
    public static class GuidShortener
    {
        private static char[] base32alpha =
        {
            'A','B','C','D','E','F','G','H',
            'I','J','K','L','M','N','O','P',
            'Q','R','S','T','U','V','W','X',
            'Y','Z','2','3','4','5','6','7',
        };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static byte getBase32AlphaBack(int pos)
        {
            if (pos < 0 || pos < 0 || pos > 40 || pos > 40)
            {
                return 0xff;
            }
            return base32alphaBack[pos];
        }

        private static byte[] base32alphaBack =
        {
            26, //2 at position 50 - 50 = 0
            27,
            28,
            29,
            30,
            31, //7 at position 55 - 50 = 5
            0xff,
            0xff,
            0xff,
            0xff,
            0xff, // 6-10 invalid
            0xff,
            0xff,
            0xff,
            0xff, // 11-14 invalid
            0, //A at postion 65 - 50 = 15
            1, //B 
            2, //C
            3,
            4,
            5,
            6,
            7,
            8,
            9,
            10,
            11,
            12,
            13, //N at postion 78 - 50 = 28
            14,
            15,
            16,
            17,
            18,
            19,
            20,
            21,
            22,
            23,
            24,
            25, //Z at position 90 - 50 = 40
        };


        /*
                                  Table 3: The Base 32 Alphabet

             Value Encoding  Value Encoding  Value Encoding  Value Encoding
                 0 A             9 J            18 S            27 3
                 1 B            10 K            19 T            28 4
                 2 C            11 L            20 U            29 5
                 3 D            12 M            21 V            30 6
                 4 E            13 N            22 W            31 7
                 5 F            14 O            23 X
                 6 G            15 P            24 Y         (pad) =
                 7 H            16 Q            25 Z
                 8 I            17 R            26 2

        */

        public static string ToB32String(Guid guid)
        {
            var result = new char[26];
            var byteArray = guid.ToByteArray();

            var part1 = BitConverter.ToUInt64(byteArray, 0);
            var part2 = BitConverter.ToUInt64(byteArray, 8);

            result[0] = base32alpha[part1 & 0x1f];
            result[1] = base32alpha[(part1 & 0x3E0) >> 5];
            result[2] = base32alpha[(part1 & 0x7C00) >> 10];
            result[3] = base32alpha[(part1 & 0xF8000) >> 15];
            result[4] = base32alpha[(part1 & 0x1f00000) >> 20];
            result[5] = base32alpha[(part1 & 0x3E000000) >> 25];
            result[6] = base32alpha[(part1 & 0x7C0000000) >> 30];
            result[7] = base32alpha[(part1 & 0xF800000000) >> 35];
            result[8] = base32alpha[(part1 & 0x1f0000000000) >> 40];
            result[9] = base32alpha[(part1 & 0x3E00000000000) >> 45];
            result[10] = base32alpha[(part1 & 0x7C000000000000) >> 50];
            result[11] = base32alpha[(part1 & 0xF80000000000000) >> 55];
            result[12] = base32alpha[(part1 & 0xF000000000000000) >> 60];

            result[13] = base32alpha[(part2 & 0x1f)];
            result[14] = base32alpha[(part2 & 0x3E0) >> 5];
            result[15] = base32alpha[(part2 & 0x7C00) >> 10];
            result[16] = base32alpha[(part2 & 0xF8000) >> 15];
            result[17] = base32alpha[(part2 & 0x1f00000) >> 20];
            result[18] = base32alpha[(part2 & 0x3E000000) >> 25];
            result[19] = base32alpha[(part2 & 0x7C0000000) >> 30];
            result[20] = base32alpha[(part2 & 0xF800000000) >> 35];
            result[21] = base32alpha[(part2 & 0x1f0000000000) >> 40];
            result[22] = base32alpha[(part2 & 0x3E00000000000) >> 45];
            result[23] = base32alpha[(part2 & 0x7C000000000000) >> 50];
            result[24] = base32alpha[(part2 & 0xF80000000000000) >> 55];
            result[25] = base32alpha[(part2 & 0xF000000000000000) >> 60];

            return new string(result);
        }

        public static Guid? FromB32ToGuid(string fromShortString)
        {
            if (string.IsNullOrWhiteSpace(fromShortString) || fromShortString.Length != 26)
            {
                return null;
            }

            ulong part1 = 0;
            ulong part2 = 0;

            var byteArray = new byte[16];
            var rawChars = fromShortString.ToUpperInvariant().ToCharArray();

            var byte0 = getBase32AlphaBack(((byte)rawChars[0]) - 50);
            var byte1 = getBase32AlphaBack(((byte)rawChars[1]) - 50);
            var byte2 = getBase32AlphaBack(((byte)rawChars[2]) - 50);
            var byte3 = getBase32AlphaBack(((byte)rawChars[3]) - 50);
            var byte4 = getBase32AlphaBack(((byte)rawChars[4]) - 50);
            var byte5 = getBase32AlphaBack(((byte)rawChars[5]) - 50);
            var byte6 = getBase32AlphaBack(((byte)rawChars[6]) - 50);
            var byte7 = getBase32AlphaBack(((byte)rawChars[7]) - 50);
            var byte8 = getBase32AlphaBack(((byte)rawChars[8]) - 50);
            var byte9 = getBase32AlphaBack(((byte)rawChars[9]) - 50);
            var byte10 = getBase32AlphaBack(((byte)rawChars[10]) - 50);
            var byte11 = getBase32AlphaBack(((byte)rawChars[11]) - 50);
            var byte12 = getBase32AlphaBack(((byte)rawChars[12]) - 50);

            var byte13 = getBase32AlphaBack(((byte)rawChars[13]) - 50);
            var byte14 = getBase32AlphaBack(((byte)rawChars[14]) - 50);
            var byte15 = getBase32AlphaBack(((byte)rawChars[15]) - 50);
            var byte16 = getBase32AlphaBack(((byte)rawChars[16]) - 50);
            var byte17 = getBase32AlphaBack(((byte)rawChars[17]) - 50);
            var byte18 = getBase32AlphaBack(((byte)rawChars[18]) - 50);
            var byte19 = getBase32AlphaBack(((byte)rawChars[19]) - 50);
            var byte20 = getBase32AlphaBack(((byte)rawChars[20]) - 50);
            var byte21 = getBase32AlphaBack(((byte)rawChars[21]) - 50);
            var byte22 = getBase32AlphaBack(((byte)rawChars[22]) - 50);
            var byte23 = getBase32AlphaBack(((byte)rawChars[23]) - 50);
            var byte24 = getBase32AlphaBack(((byte)rawChars[24]) - 50);
            var byte25 = getBase32AlphaBack(((byte)rawChars[25]) - 50);


            if (byte0 == 0xff || byte1 == 0xff || byte2 == 0xff || byte3 == 0xff ||
                byte4 == 0xff || byte5 == 0xff || byte6 == 0xff || byte7 == 0xff ||
                byte8 == 0xff || byte9 == 0xff || byte10 == 0xff || byte11 == 0xff ||
                byte12 == 0xff || byte13 == 0xff || byte14 == 0xff || byte15 == 0xff ||
                byte16 == 0xff || byte17 == 0xff || byte18 == 0xff || byte19 == 0xff ||
                byte20 == 0xff || byte21 == 0xff || byte22 == 0xff || byte23 == 0xff ||
                byte24 == 0xff || byte25 == 0xff) return null;


            part2 = part2 | byte25;
            part2 = part2 << 5;
            part2 = part2 | byte24;
            part2 = part2 << 5;
            part2 = part2 | byte23;
            part2 = part2 << 5;
            part2 = part2 | byte22;
            part2 = part2 << 5;
            part2 = part2 | byte21;
            part2 = part2 << 5;
            part2 = part2 | byte20;
            part2 = part2 << 5;
            part2 = part2 | byte19;
            part2 = part2 << 5;
            part2 = part2 | byte18;
            part2 = part2 << 5;
            part2 = part2 | byte17;
            part2 = part2 << 5;
            part2 = part2 | byte16;
            part2 = part2 << 5;
            part2 = part2 | byte15;
            part2 = part2 << 5;
            part2 = part2 | byte14;
            part2 = part2 << 5;
            part2 = part2 | byte13;

            part1 = part1 | byte12;
            part1 = part1 << 5;
            part1 = part1 | byte11;
            part1 = part1 << 5;
            part1 = part1 | byte10;
            part1 = part1 << 5;
            part1 = part1 | byte9;
            part1 = part1 << 5;
            part1 = part1 | byte8;
            part1 = part1 << 5;
            part1 = part1 | byte7;
            part1 = part1 << 5;
            part1 = part1 | byte6;
            part1 = part1 << 5;
            part1 = part1 | byte5;
            part1 = part1 << 5;
            part1 = part1 | byte4;
            part1 = part1 << 5;
            part1 = part1 | byte3;
            part1 = part1 << 5;
            part1 = part1 | byte2;
            part1 = part1 << 5;
            part1 = part1 | byte1;
            part1 = part1 << 5;

            part1 = part1 | byte0;


            BitConverter.GetBytes(part1).CopyTo(byteArray, 0);
            BitConverter.GetBytes(part2).CopyTo(byteArray, 8);


            return new Guid(byteArray);
        }


        /*
                          Table 1: The Base 64 Alphabet

         Value Encoding  Value Encoding  Value Encoding  Value Encoding
             0 A            17 R            34 i            51 z
             1 B            18 S            35 j            52 0
             2 C            19 T            36 k            53 1
             3 D            20 U            37 l            54 2
             4 E            21 V            38 m            55 3
             5 F            22 W            39 n            56 4
             6 G            23 X            40 o            57 5
             7 H            24 Y            41 p            58 6
             8 I            25 Z            42 q            59 7
             9 J            26 a            43 r            60 8
            10 K            27 b            44 s            61 9
            11 L            28 c            45 t            62 +
            12 M            29 d            46 u            63 /
            13 N            30 e            47 v
            14 O            31 f            48 w         (pad) =
            15 P            32 g            49 x
            16 Q            33 h            50 y
        */

        public static string ToB64String(Guid guid)
        {
            guid.ToByteArray(); // 16 bytes



            return string.Empty;
        }

        public static Guid FromB64ToGuid(string fromShortString)
        {



            return Guid.Empty;
        }



    }
}
