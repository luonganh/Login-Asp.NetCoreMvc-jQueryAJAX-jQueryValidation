namespace Asp.NetCore.Shared.Helpers
{
    public static class TextHelper
    {
        public static string ToUnsignString(string input)
        {
            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            input = input.Replace(".", "-");
            input = input.Replace(" ", "-");
            input = input.Replace(",", "-");
            input = input.Replace(";", "-");
            input = input.Replace(":", "-");
            input = input.Replace("  ", "-");
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            while (str2.Contains("--"))
            {
                str2 = str2.Replace("--", "-").ToLower();
            }
            return str2;
        }

        public static string ToFileNameString(string input)
        {
            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            input = input.Replace(".", "_");
            input = input.Replace(" ", "_");
            input = input.Replace(",", "_");
            input = input.Replace(";", "_");
            input = input.Replace(":", "_");
            input = input.Replace("  ", "_");
            input = input.Replace("-", "_");
            input = input.Replace("@", "_");
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            while (str2.Contains("--"))
            {
                str2 = str2.Replace("--", "__").ToLower();
            }
            return str2;
        }
       
        public static string[] DateStringFormats()
        {
            string[] dateStringformats = {
                "dd/MM/yyyy", "dd/M/yyyy", "d/M/yyyy", "d/MM/yyyy", "dd/MM/yy", "dd/M/yy", "d/M/yy", "d/MM/yy",
                "dd-MM-yyyy", "dd-M-yyyy", "d-M-yyyy", "d-MM-yyyy", "dd-MM-yy", "dd-M-yy", "d-M-yy", "d-MM-yy",
                "yyyy-mm-dd", "d/M/yyyy",
                "dd-MM-yyyy hh:mm:ss", "dd/MM/yyyy hh:mm:ss", "dd/M/yyyy hh:mm:ss", "d/MM/yyyy hh:mm:ss", "d/M/yyyy hh:mm:ss"
            };
            return dateStringformats;
        }

        #region Convert format datetime
        /// <summary>
        /// Ddmmyyyy_to_mmddyyyys the specified STR value.
        /// </summary>
        /// <param name="strValue">The STR value.</param>
        /// <returns></returns>
        public static string ddmmyyyy_to_mmddyyyy(string strValue)
        {
            if (strValue != "")
            {
                string[] strArray = strValue.Split(new char[] { '/' });
                string str = strArray[0];
                string str2 = strArray[1];
                string str3 = strArray[2];
                return (str2 + "/" + str + "/" + str3);
            }
            return "";
        }

        public static string mmddyyyy_to_ddmmyyyy(string strValue)
        {
            if (strValue != "")
            {
                string[] strArray = strValue.Split(new char[] { '/' });
                string str = strArray[0];
                string str2 = strArray[1];
                string str3 = strArray[2];
                return (str2 + "/" + str + "/" + str3);
            }
            return "";
        }
        #endregion

        private static readonly Random Random = new Random();
        const string ALPHABET_NUMBER = "ABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
        /// <summary>
        /// Generate random string
        /// </summary>
        /// <param name="random"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateRandomString(string random, int length)
        {
            return new string(Enumerable.Repeat(random, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }

        const string CHARACTERS = "abcdefghijklmnopqursuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string NUMBER = "1234567890";
        const string CHAR_SPECIAL = "!@£$%^&*()#€";
        const string CHAR_UPPER = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static string GenerateRandomPassword()
        {
            StringBuilder sb = new StringBuilder();
            Random rnd = new Random();

            for (int i = 0; i < 6; i++)
            {
                int index = rnd.Next(CHARACTERS.Length);
                sb.Append(CHARACTERS[index]);
            }
            sb.Append(NUMBER[rnd.Next(NUMBER.Length)]);
            sb.Append(CHAR_SPECIAL[rnd.Next(CHAR_SPECIAL.Length)]);
            sb.Append(CHAR_UPPER[rnd.Next(CHAR_UPPER.Length)]);

            return sb.ToString();
        }
        
        /// <summary>
        /// Generate random code
        /// </summary>
        /// <returns></returns>
        public static string GenerateRandomCode()
        {
            var randomAlphabet = GenerateRandomString(CHAR_UPPER, 4);

            var randomNumber = Enumerable.Range(0, 9)
                .OrderBy(x => Random.Next())
                .Take(4);
            return $"{string.Join(string.Empty, randomNumber)}{randomAlphabet}";
            //return $"{randomAlphabet}{string.Join(string.Empty, randomNumber)}";
        }

        public static bool IsValidEmail(string str)
        {
            string pattern = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            Regex check = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
            return check.IsMatch(str.Trim());
        }

        /// <summary>
        /// Replace HTML template with values
        /// </summary>
        /// <param name="template">Template content HTML</param>
        /// <param name="replacements">Dictionary with key/value</param>
        /// <returns></returns>
        public static string Parse(this string template, Dictionary<string, string> replacements)
        {
            if (replacements.Count > 0)
            {
                template = replacements.Keys
                            .Aggregate(template, (current, key) => current.Replace(key, replacements[key]));
            }
            return template;
        }

        /// <summary>
        /// Loại bỏ các ký tự đặc biệt và chuyển sang các ký tự và chữ số
        /// </summary>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string Encode(string encode)
        {
            try
            {
                //Remove VietNamese character
                encode = encode.Trim().ToLower();
                encode = Regex.Replace(encode, "[áàảãạâấầẩẫậăắằẳẵặ]", "a");
                encode = Regex.Replace(encode, "[éèẻẽẹêếềểễệ]", "e");
                encode = Regex.Replace(encode, "[iíìỉĩị]", "i");
                encode = Regex.Replace(encode, "[óòỏõọơớờởỡợôốồổỗộ]", "o");
                encode = Regex.Replace(encode, "[úùủũụưứừửữự]", "u");
                encode = Regex.Replace(encode, "[yýỳỷỹỵ]", "y");
                encode = Regex.Replace(encode, "[đ]", "d");

                //Remove space
                encode = encode.Replace(" ", "-");

                //Remove special character
                encode = Regex.Replace(encode, "[:\"`~!@#$%^&*()-+=?/>.<,{}[]|]\\'“”]", "");
                encode = encode.Replace("'", "");
                encode = encode.Replace("“", "");
                encode = encode.Replace("”", "");
                encode = encode.Replace("]", "");
                encode = encode.Replace("[", "");

                if (encode.EndsWith("-"))
                {
                    encode = encode.Substring(0, encode.LastIndexOf('-'));
                }

                //Remove dupplicate - character
                string exp = "[-]{2,}";
                encode = Regex.Replace(encode, exp, "-");

                string tmp = Regex.Replace(encode, "[^a-zA-Z0-9]+", "-");

                tmp = tmp.Replace("---", "--");
                tmp = tmp.Replace("--", "-");

                if (tmp.StartsWith("-"))
                    tmp = tmp.Substring(1);

                if (tmp.EndsWith("-"))
                    tmp = tmp.Substring(0, tmp.Length - 1);

                return tmp;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string Encode(string encode, EncodeSeperator seperator)
        {
            try
            {
                //Remove VietNamese character
                encode = encode.Trim().ToLower();
                encode = Regex.Replace(encode, "[áàảãạâấầẩẫậăắằẳẵặạ]", "a");
                encode = Regex.Replace(encode, "[éèẻẽẹêếềểễệ]", "e");
                encode = Regex.Replace(encode, "[iíìỉĩị]", "i");
                encode = Regex.Replace(encode, "[óòỏõọơớờởỡợôốồổỗộ]", "o");
                encode = Regex.Replace(encode, "[úùủũụưứừửữự]", "u");
                encode = Regex.Replace(encode, "[yýỳỷỹỵ]", "y");
                encode = Regex.Replace(encode, "[đ]", "d");
                encode = encode.Replace("ạ", "a");
                if (encode.IndexOf("ạ") > 0)
                {
                    throw new ArgumentException();
                }
                //Remove space
                encode = encode.Replace(" ", "-");

                //Remove special character
                encode = Regex.Replace(encode, "[:\"`~!@#$%^&*()-+=?/>.<,{}[]|]\\']", "");

                if (encode.EndsWith("-"))
                {
                    encode = encode.Substring(0, encode.LastIndexOf('-'));
                }

                //Remove dupplicate - character
                string exp = "[-]{2,}";
                encode = Regex.Replace(encode, exp, "-");

                if (seperator == EncodeSeperator.Space)
                {
                    encode = encode.Replace('-', ' ');
                }

                return encode;
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// Escape a value in a HSQL (ex: LIKE '%\%' ESCAPE '\').
        /// </summary>
        /// <param name="str">String value to escape.</param>
        /// <returns>An escaped string.</returns>
        public static string EscapeInHSQL(string str)
        {
            if (str == null) throw new ArgumentNullException("str");
            str = str
                .Replace("\\_", "_")
                .Replace("_", "\\_")
                .Replace("\\%", "%")
                .Replace("%", "\\%")
                .Replace("?", "_")
                .Replace("*", "%");
            return str;
        }

        /// <summary>
        /// Loại bỏ thẻ Html khỏi văn bản html
        /// </summary>
        /// <param name="htmlDocument"></param>
        /// <returns></returns>
        public static string StripHtml(string htmlDocument)
        {
            String result = Regex.Replace(htmlDocument, @"<[^>]*>", String.Empty);
            return result;
        }

        /// <summary>
        /// Shorten a sentence by maximum length.
        /// </summary>
        /// <param name="sentence"></param>
        /// <param name="len"></param>
        /// <returns>A short sentence (padded by '...').</returns>
        public static string ShortenByWord(string sentence, int len)
        {
            if (sentence == null) return string.Empty;
            if (sentence.Length > len)
            {
                sentence = sentence.Substring(0, len);
                // cut a word
                int pos = sentence.LastIndexOf(' ');
                if (pos > 0) sentence = sentence.Substring(0, pos);
                return sentence + "...";
            }
            return sentence;
        }

        public static string ReplaceBadString(string strSource)
        {
            string str = strSource;
            if (!string.IsNullOrEmpty(str))
            {
                return str.Replace("'", "&#39;").Replace("\"", "&#34;").Replace("<", "&lt;").Replace(">", "&gt;");
            }
            else return str;
        }

        public static string ReplaceLineFeed(string strContent)
        {
            if (!string.IsNullOrEmpty(strContent))
            {
                return strContent.Replace("" + (char)13, "<br />");
            }
            else
            {
                return strContent;
            }
        }

        /// <summary>
        /// Compares two objects for equivalence, ignoring the case of strings.
        /// </summary>
        [Serializable]
        public class CaseInsensitiveComparer<T> : IComparer<T>
        {
            private CompareInfo _compareInfo;
            private static CaseInsensitiveComparer<T> _invariantCaseInsensitiveComparer;

            /// <summary>
            /// Initializes a new instance of the CaseInsensitiveComparer class using the CurrentCulture of the current thread.
            /// </summary>
            public CaseInsensitiveComparer() : this(CultureInfo.CurrentCulture) { }

            /// <summary>
            /// Initializes a new instance of the CaseInsensitiveComparer class using the specified <see cref="CultureInfo" />.
            /// </summary>
            /// <param name="culture">The <see cref="CultureInfo" /> to use for the new CaseInsensitiveComparer.</param>
            /// <exception cref="ArgumentNullException">culture is null.</exception>
            public CaseInsensitiveComparer(CultureInfo culture)
            {
                if (culture == null) throw new ArgumentNullException("culture");
                this._compareInfo = culture.CompareInfo;
            }

            /// <summary>
            /// Performs a case-insensitive comparison of two objects of the same type and returns a value indicating whether
            /// one is less than, equal to or greater than the other.
            /// </summary>
            /// <returns>Value Condition Less than zero x is less than y, with casing ignored.
            /// Zero x equals y, with casing ignored. Greater than zero x is greater than y, with casing ignored.</returns>
            /// <param name="x">The first object to compare.</param>
            /// <param name="y">The second object to compare.</param>
            /// <exception cref="ArgumentException">Neither x nor y implements the <see cref="IComparable" /> interface.
            /// -or- x and y are of different types.</exception>
            public int Compare(T x, T y)
            {
                string str = x as string, str2 = y as string;
                if ((str != null) && (str2 != null)) return this._compareInfo.Compare(str, str2, CompareOptions.IgnoreCase);
                return Comparer<T>.Default.Compare(x, y);
            }

            /// <summary>
            /// Gets an instance of CaseInsensitiveComparer that is associated with the CurrentCulture of the current thread
            /// and that is always available.</summary>
            /// <returns>An instance of CaseInsensitiveComparer that is associated with the CurrentCulture of the current thread.</returns>
            public static CaseInsensitiveComparer<T> Default
            {
                get { return new CaseInsensitiveComparer<T>(); }
            }

            /// <summary>
            /// Gets an instance of CaseInsensitiveComparer that is associated with InvariantCulture and that is always available.
            /// </summary>
            /// <returns>An instance of CaseInsensitiveComparer that is associated with InvariantCulture.</returns>
            public static CaseInsensitiveComparer<T> DefaultInvariant
            {
                get
                {
                    if (_invariantCaseInsensitiveComparer == null)
                        _invariantCaseInsensitiveComparer = new CaseInsensitiveComparer<T>(CultureInfo.InvariantCulture);
                    return _invariantCaseInsensitiveComparer;
                }
            }
        }
    }
}
