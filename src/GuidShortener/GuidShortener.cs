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

            for (int i = 0; i < 13; i++)
            {
                result[i] = base32alpha[part1 & 31];
                result[i + 13] = base32alpha[part2 & 31];
                part1 = part1 >> 5;
                part2 = part2 >> 5;
            }

            return new string(result);
        }

        public static Guid? FromB32ToGuid(string fromShortString)
        {
            if (string.IsNullOrWhiteSpace(fromShortString) || fromShortString.Length != 26)
            {
                //throw new ArgumentOutOfRangeException("String provided in invalid format.");
                return null;
            }

            ulong part1 = 0;
            ulong part2 = 0;

            var byteArray = new byte[16];
            var rawChars = fromShortString.ToUpperInvariant().ToCharArray();

            // loop stuff
            char char1;
            char char2;
            int pos1;
            int pos2;
            byte ammount1;
            byte ammount2;

            for (int i = 0; i < 12; i++)
            {
                char1 = rawChars[i];
                char2 = rawChars[i + 13];

                pos1 = ((byte)char1 - 50);
                pos2 = ((byte)char2 - 50);

                if (pos1 < 0 || pos2 < 0 || pos1 > 40 || pos2 > 40)
                {
                    return null;
                }

                ammount1 = base32alphaBack[pos1];
                ammount2 = base32alphaBack[pos2];

                if (ammount1 == 0xff || ammount2 == 0xff)
                {
                    return null;
                }

                part1 = part1 << 5;
                part2 = part2 << 5;

                part1 = part1 | ammount1;
                part2 = part2 | ammount2;
            }

            // the last one is only 4 bits
            char1 = rawChars[12];
            char2 = rawChars[25];//12 + 13

            pos1 = ((byte)char1 - 50);
            pos2 = ((byte)char2 - 50);

            if (pos1 < 0 || pos2 < 0 || pos1 > 40 || pos2 > 40)
            {
                return null;
            }

            ammount1 = base32alphaBack[pos1];
            ammount2 = base32alphaBack[pos2];

            if (ammount1 == 0xff || ammount2 == 0xff)
            {
                return null;
            }

            part1 = part1 << 4;
            part2 = part2 << 4;

            part1 = part1 | ammount1;
            part2 = part2 | ammount2;


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
