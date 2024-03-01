namespace Asp.NetCore.Shared.Helpers
{
    public static class RomanNumerals
    {
        public static (bool, int) IsRomanNumeralAndConvertToInt(string input)
        {
            //var result = false;
            //int num = 0;
            //switch (input)
            //{
            //    case "I": { result = true; num = 1; } break;
            //    case "II": { result = true; num = 2; } break;
            //    case "III": { result = true; num = 3; } break;
            //    case "IV": { result = true; num = 4; } break;
            //    case "V": { result = true; num = 5; } break;
            //    case "VI": { result = true; num = 6; } break;
            //    case "VII": { result = true; num = 7; } break;
            //    case "VIII": { result = true; num = 8; } break;
            //    case "IX": { result = true; num = 9; } break;
            //    case "X": { result = true; num = 10; } break;
            //    case "XI": { result = true; num = 11; } break;
            //    case "XII": { result = true; num = 12; } break;
            //    case "XIII": { result = true; num = 13; } break;
            //    case "XIV": { result = true; num = 14; } break;
            //    case "XV": { result = true; num = 15; } break;
            //    case "XVI": { result = true; num = 16; } break;
            //    case "XVII": { result = true; num = 17; } break;
            //    case "XVIII": { result = true; num = 18; } break;
            //    case "XIX": { result = true; num = 19; } break;
            //    case "XX": { result = true; num = 20; } break;

            //    case "XXI": { result = true; num = 21; } break;
            //    case "XXII": { result = true; num = 22; } break;
            //    case "XXIII": { result = true; num = 23; } break;
            //    case "XXIV": { result = true; num = 24; } break;
            //    case "XXV": { result = true; num = 25; } break;
            //    case "XXVI": { result = true; num = 26; } break;

            //    case "XXVII": { result = true; num = 27; } break;
            //    case "XXVIII": { result = true; num = 28; } break;
            //    case "XXIX": { result = true; num = 29; } break;
            //    case "XXX": { result = true; num = 30; } break;
            //    case "XXXI": { result = true; num = 31; } break;
            //    case "XXXII": { result = true; num = 32; } break;
            //    case "XXXIII": { result = true; num = 33; } break;

            //    case "XXXIV": { result = true; num = 34; } break;
            //    case "XXXV": { result = true; num = 35; } break;
            //    case "XXXVI": { result = true; num = 36; } break;
            //    case "XXXVII": { result = true; num = 37; } break;
            //    case "XXXVIII": { result = true; num = 38; } break;
            //    case "XXXIX": { result = true; num = 39; } break;

            //    case "XL": { result = true; num = 40; } break;
            //    case "XLI": { result = true; num = 41; } break;
            //    case "XLII": { result = true; num = 42; } break;
            //    case "XLIII": { result = true; num = 43; } break;
            //    case "XLIV": { result = true; num = 44; } break;
            //    case "XLV": { result = true; num = 45; } break;
            //    case "XLVI": { result = true; num = 46; } break;
            //    case "XLVII": { result = true; num = 47; } break;
            //    case "XLVIII": { result = true; num = 48; } break;
            //    case "XLIX": { result = true; num = 49; } break;
            //    case "L": { result = true; num = 50; } break;
            //}

            return RomanToInteger(input);
        }

        public static string ConvertRomanNumeralToInt(int input)
        {
            var result = string.Empty;
            switch (input)
            {
                case 1: { result = "I"; } break;
                case 2: { result = "II"; } break;
                case 3: { result = "III"; } break;
                case 4: { result = "III"; } break;
                case 5: { result = "V"; } break;
                case 6: { result = "VI"; } break;
                case 7: { result = "VII"; } break;
                case 8: { result = "VIII"; } break;
                case 9: { result = "IX"; } break;
                case 10: { result = "X"; } break;
            }
            return result;
        }

        private static Dictionary<char, int> RomanMap = new Dictionary<char, int>()
            {
                {'I', 1},
                {'V', 5},
                {'X', 10},
                {'L', 50},
                {'C', 100},
                {'D', 500},
                {'M', 1000}
            };

        public static (bool, int) RomanToInteger(string roman)
        {
            if (roman.Any(c => !RomanMap.ContainsKey(c)))
                return (false, 0);

            int number = 0;
            for (int i = 0; i < roman.Length; i++)
            {
                if (i + 1 < roman.Length && RomanMap[roman[i]] < RomanMap[roman[i + 1]])
                {
                    number -= RomanMap[roman[i]];
                }
                else
                {
                    number += RomanMap[roman[i]];
                }
            }
            return (true, number);
        }

        public static (bool, int) RomanToIntegerV2(string roman)
        {
            if (roman.Any(c => !RomanMap.ContainsKey(c)))
                return (false, 0);

            int number = 0;
            char previousChar = roman[0];
            foreach (char currentChar in roman)
            {
                number += RomanMap[currentChar];
                if (RomanMap[previousChar] < RomanMap[currentChar])
                {
                    number -= RomanMap[previousChar] * 2;
                }
                previousChar = currentChar;
            }
            return (true, number);
        }

        public static string ToRoman(int number)
        {
            if ((number < 0) || (number > 3999)) throw new ArgumentOutOfRangeException("insert value betwheen 1 and 3999");
            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + ToRoman(number - 1000);
            if (number >= 900) return "CM" + ToRoman(number - 900);
            if (number >= 500) return "D" + ToRoman(number - 500);
            if (number >= 400) return "CD" + ToRoman(number - 400);
            if (number >= 100) return "C" + ToRoman(number - 100);
            if (number >= 90) return "XC" + ToRoman(number - 90);
            if (number >= 50) return "L" + ToRoman(number - 50);
            if (number >= 40) return "XL" + ToRoman(number - 40);
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            if (number >= 1) return "I" + ToRoman(number - 1);
            throw new ArgumentOutOfRangeException("something bad happened");
        }
    }
}
