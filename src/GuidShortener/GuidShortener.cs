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
            if (pos < 0 || pos > 40)
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


        private static char[] base64alpha =
{
            'A','B','C','D','E','F','G','H',
            'I','J','K','L','M','N','O','P',
            'Q','R','S','T','U','V','W','X',
            'Y','Z','a','b','c','d','e','f',
            'g','h','i','j','k','l','m','n',
            'o','p','q','r','s','t','u','v',
            'w','x','y','z','0','1','2','3',
            '4','5','6','7','8','9','+','-'
        };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static byte getBase64AlphaBack(int pos)
        {
            if (pos < 0 || pos > 79)
            {
                return 0xff;
            }
            return base64alphaBack[pos];
        }

        private static byte[] base64alphaBack =
        {
            62, //+ at position 43 - 43 = 0
            0xff,
            63, //- at position 45 - 43 = 2
            0xff,
            0xff,
            52, //0 at position 48 - 43 = 5
            53,
            54,
            55,
            56,
            57, //5 at position 53 - 43 = 10
            58, 
            59, 
            60, 
            61, //9 at position 57 - 43 = 14
            0xff,
            0xff,
            0xff,
            0xff,
            0xff,
            0xff,
            0xff, // no chars at 15 -> 21
            0, //A at postion 65 - 43 = 22
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
            13, //N at postion 78 - 43 = 35
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
            25, //Z at position 90 - 43 = 47
            0xff,
            0xff,
            0xff,
            0xff,
            0xff,
            0xff,
            26, //a at position 97 - 43 = 54
            27,
            28,
            29,
            30, //e at position 101 - 43 = 58
            31,
            32,
            33,
            34,
            35,
            36,
            37,
            38,
            39, //n at position 110 - 43 = 67
            40,
            41,
            42,
            43,
            44,
            45,
            46,
            47,
            48,
            49,
            50,
            51, //z at position 122 - 43 = 79
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
                 6 G            15 P            24 Y         (pad) =  not used
                 7 H            16 Q            25 Z
                 8 I            17 R            26 2

        */

        /// <summary>
        /// Converts a GUID into a string that is exactly 26 chars long and is comprised of the base32 encoding alphabet.
        /// ABCDEFGHIJKLMNOPQRSTUVWZYZ234567
        /// Useful in a case insensitive setting where 'A' cannot be distinguished from 'a'
        /// </summary>
        /// <param name="guid"></param>
        /// <returns>A string that is exactly 26 chars long.</returns>
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

        /// <summary>
        /// Convert a 26 char long shortened guid
        /// </summary>
        /// <param name="fromShortString"></param>
        /// <returns>Your GUID or upon failure NULL</returns>
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
            12 M            29 d            46 u            63 / swap out with '-' since its so problamatic 
            13 N            30 e            47 v
            14 O            31 f            48 w         (pad) =  not used
            15 P            32 g            49 x
            16 Q            33 h            50 y
        */

        /// <summary>
        /// Converts a GUID into a string that is exactly 22 chars long and is comprised of the base64 encoding alphabet.
        /// '/' char is replaced in the encoding with '-' for compatibility reasons
        /// ABCDEFGHIJKLMNOPQRSTUVWZYZabcdefghijklmnopqrstuvwxyz0123456789+-
        /// Useful in a case sensitive setting where 'A' can be distinguished from 'a'
        /// </summary>
        /// <param name="guid"></param>
        /// <returns>A string that is exactly 22 chars long.</returns>

        public static string ToB64String(Guid guid)
        {
            var result = new char[22];
            var byteArray = guid.ToByteArray();

            var part1 = BitConverter.ToUInt64(byteArray, 0);
            var part2 = BitConverter.ToUInt64(byteArray, 8);

            result[0] = base64alpha[part1 & 0x3F];
            result[1] = base64alpha[(part1 & 0xFC0) >> 6];
            result[2] = base64alpha[(part1 & 0x3F000) >> 12];
            result[3] = base64alpha[(part1 & 0xFC0000) >> 18];
            result[4] = base64alpha[(part1 & 0x3F000000) >> 24];
            result[5] = base64alpha[(part1 & 0xFC0000000) >> 30];
            result[6] = base64alpha[(part1 & 0x3F000000000) >> 36];
            result[7] = base64alpha[(part1 & 0xFC0000000000) >> 42];
            result[8] = base64alpha[(part1 & 0x3F000000000000) >> 48];
            result[9] = base64alpha[(part1 & 0xFC0000000000000) >> 54];
            result[10] = base64alpha[(part1 & 0xF000000000000000) >> 60];

            result[11] = base64alpha[part2 & 0x3F];
            result[12] = base64alpha[(part2 & 0xFC0) >> 6];
            result[13] = base64alpha[(part2 & 0x3F000) >> 12];
            result[14] = base64alpha[(part2 & 0xFC0000) >> 18];
            result[15] = base64alpha[(part2 & 0x3F000000) >> 24];
            result[16] = base64alpha[(part2 & 0xFC0000000) >> 30];
            result[17] = base64alpha[(part2 & 0x3F000000000) >> 36];
            result[18] = base64alpha[(part2 & 0xFC0000000000) >> 42];
            result[19] = base64alpha[(part2 & 0x3F000000000000) >> 48];
            result[20] = base64alpha[(part2 & 0xFC0000000000000) >> 54];
            result[21] = base64alpha[(part2 & 0xF000000000000000) >> 60];

            return new string(result);
        }

        /// <summary>
        /// Convert a 22 char long shortened guid
        /// </summary>
        /// <param name="fromShortString"></param>
        /// <returns>Your GUID or upon failure NULL</returns>
        public static Guid? FromB64ToGuid(string fromShortString)
        {
            if (string.IsNullOrWhiteSpace(fromShortString) || fromShortString.Length != 22)
            {
                return null;
            }

            ulong part1 = 0;
            ulong part2 = 0;

            var byteArray = new byte[16];
            var rawChars = fromShortString.ToCharArray();

            var byte0 = getBase64AlphaBack(((byte)rawChars[0]) - 43);
            var byte1 = getBase64AlphaBack(((byte)rawChars[1]) - 43);
            var byte2 = getBase64AlphaBack(((byte)rawChars[2]) - 43);
            var byte3 = getBase64AlphaBack(((byte)rawChars[3]) - 43);
            var byte4 = getBase64AlphaBack(((byte)rawChars[4]) - 43);
            var byte5 = getBase64AlphaBack(((byte)rawChars[5]) - 43);
            var byte6 = getBase64AlphaBack(((byte)rawChars[6]) - 43);
            var byte7 = getBase64AlphaBack(((byte)rawChars[7]) - 43);
            var byte8 = getBase64AlphaBack(((byte)rawChars[8]) - 43);
            var byte9 = getBase64AlphaBack(((byte)rawChars[9]) - 43);
            var byte10 = getBase64AlphaBack(((byte)rawChars[10]) - 43);

            var byte11 = getBase64AlphaBack(((byte)rawChars[11]) - 43);
            var byte12 = getBase64AlphaBack(((byte)rawChars[12]) - 43);
            var byte13 = getBase64AlphaBack(((byte)rawChars[13]) - 43);
            var byte14 = getBase64AlphaBack(((byte)rawChars[14]) - 43);
            var byte15 = getBase64AlphaBack(((byte)rawChars[15]) - 43);
            var byte16 = getBase64AlphaBack(((byte)rawChars[16]) - 43);
            var byte17 = getBase64AlphaBack(((byte)rawChars[17]) - 43);
            var byte18 = getBase64AlphaBack(((byte)rawChars[18]) - 43);
            var byte19 = getBase64AlphaBack(((byte)rawChars[19]) - 43);
            var byte20 = getBase64AlphaBack(((byte)rawChars[20]) - 43);
            var byte21 = getBase64AlphaBack(((byte)rawChars[21]) - 43);

            if (byte0 == 0xff || byte1 == 0xff || byte2 == 0xff || byte3 == 0xff ||
                byte4 == 0xff || byte5 == 0xff || byte6 == 0xff || byte7 == 0xff ||
                byte8 == 0xff || byte9 == 0xff || byte10 == 0xff || byte11 == 0xff ||
                byte12 == 0xff || byte13 == 0xff || byte14 == 0xff || byte15 == 0xff ||
                byte16 == 0xff || byte17 == 0xff || byte18 == 0xff || byte19 == 0xff ||
                byte20 == 0xff || byte21 == 0xff ) return null;

            part2 = part2 | byte21;
            part2 = part2 << 6;
            part2 = part2 | byte20;
            part2 = part2 << 6;
            part2 = part2 | byte19;
            part2 = part2 << 6;
            part2 = part2 | byte18;
            part2 = part2 << 6;
            part2 = part2 | byte17;
            part2 = part2 << 6;
            part2 = part2 | byte16;
            part2 = part2 << 6;
            part2 = part2 | byte15;
            part2 = part2 << 6;
            part2 = part2 | byte14;
            part2 = part2 << 6;
            part2 = part2 | byte13;
            part2 = part2 << 6;
            part2 = part2 | byte12;
            part2 = part2 << 6;
            part2 = part2 | byte11;

            part1 = part1 | byte10;
            part1 = part1 << 6;
            part1 = part1 | byte9;
            part1 = part1 << 6;
            part1 = part1 | byte8;
            part1 = part1 << 6;
            part1 = part1 | byte7;
            part1 = part1 << 6;
            part1 = part1 | byte6;
            part1 = part1 << 6;
            part1 = part1 | byte5;
            part1 = part1 << 6;
            part1 = part1 | byte4;
            part1 = part1 << 6;
            part1 = part1 | byte3;
            part1 = part1 << 6;
            part1 = part1 | byte2;
            part1 = part1 << 6;
            part1 = part1 | byte1;
            part1 = part1 << 6;
            part1 = part1 | byte0;

            BitConverter.GetBytes(part1).CopyTo(byteArray, 0);
            BitConverter.GetBytes(part2).CopyTo(byteArray, 8);

            return new Guid(byteArray);
        }
    }
}
