using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cyrl2ToteConverter.NetCoreWeb.Example
{
   public class Cyrl2ToteHelper
    {

        enum Sound
        {
            Vowel, //Дауысты дыбыс
            Consonant, //Дауыссыз дыбыс
            Unknown //Белгісіз
        }
        private readonly static string[] cyrlChars = { "А", "Ә", "Ə", "Б", "В", "Г", "Ғ", "Д", "Е", "Ё", "Ж", "З", "И", "Й", "К", "Қ", "Л", "М", "Н", "Ң", "О", "Ө", "Ɵ", "П", "Р", "С", "Т", "У", "Ұ", "Ү", "Ф", "Х", "Һ", "Ц", "Ч", "Ш", "Щ", "Ъ", "Ы", "І", "Ь", "Э", "Ю", "Я", "-" };

        private static Dictionary<string, string> dialectWordsDic = new Dictionary<string, string>()
        {
           {"قر","ق ر"}, {"جحر","ج ح ر"},{"جشس","ج ش س"},{"شۇار","ش ۇ ا ر"},{"باق","ب ا ق"},{"ءباسپاسوز","باسپا ءسوز"},
        };

        #region Кирилшені төте жазуға сәйкестендіру
        public static string Cyrl2Tote(string cyrlText)
        {
            cyrlText = CopycatCyrlToOriginalCyrl(cyrlText);
            cyrlText += ".";
            cyrlText = System.Net.WebUtility.HtmlDecode(cyrlText); //Сайт сәйкестендіруге қолданбаса алып тастауға болады
            string[] chars = cyrlText.ToCharArray().Select(x => x.ToString()).ToArray();
            int length = chars.Length;
            string[] toteStrs = new string[length];
            Sound prevSound = Sound.Unknown;
            string cyrlWord = string.Empty;
            for (int i = 0; i < length; i++)
            {
                if (!cyrlChars.Contains(chars[i].ToUpper()))
                {
                    if (!string.IsNullOrEmpty(cyrlWord))
                    {
                        int wordLength = cyrlWord.Length;
                        string[] toteChars = new string[wordLength];
                        int j = (i - wordLength);
                        int tIndex = 0;
                        for (; j < i; j++, tIndex++)
                        {
                            if (j + 1 < length)
                            {
                                string key = string.Concat(chars[j], chars[j + 1]);
                                switch (key.ToLower())
                                {
                                    case "ия":
                                        { toteChars[tIndex] = "يا"; j += 1; continue; }
                                    case "йя":
                                        { toteChars[tIndex] = "ييا"; j += 1; continue; }
                                    case "ию":
                                        { toteChars[tIndex] = "يۋ"; j += 1; continue; }
                                    case "йю":
                                        { toteChars[tIndex] = "يۋ"; j += 1; continue; }
                                    case "сц":
                                        { toteChars[tIndex] = "س"; j += 1; continue; }
                                    case "тч":
                                        { toteChars[tIndex] = "چ"; j += 1; continue; }
                                    case "ий":
                                        { toteChars[tIndex] = "ي"; j += 1; continue; }
                                    case "ХХ": { toteChars[tIndex] = "ХХ"; j += 1; continue; }
                                }
                            }

                            switch (chars[j].ToLower())
                            {
                                case "я": { toteChars[tIndex] = prevSound == Sound.Consonant ? "ءا" : "يا"; } break;
                                case "ю": { toteChars[tIndex] = prevSound == Sound.Consonant ? "ءۇ" : "يۋ"; } break;
                                case "щ": { toteChars[tIndex] = "شش"; } break;
                                case "э": { toteChars[tIndex] = "ە"; } break;
                                case "а": { toteChars[tIndex] = "ا"; } break;
                                case "б": { toteChars[tIndex] = "ب"; } break;
                                case "ц": { toteChars[tIndex] = "س"; } break;
                                case "д": { toteChars[tIndex] = "د"; } break;
                                case "е": { toteChars[tIndex] = "ە"; } break;
                                case "ф": { toteChars[tIndex] = "ف"; } break;
                                case "г": { toteChars[tIndex] = "گ"; } break;
                                case "х": { toteChars[tIndex] = "ح"; } break;
                                case "Һ":
                                case "һ": { toteChars[tIndex] = "ھ"; } break;
                                case "І":
                                case "і": { toteChars[tIndex] = "ءى"; } break;
                                case "и":
                                case "й": { toteChars[tIndex] = "ي"; } break;
                                case "к": { toteChars[tIndex] = "ك"; } break;
                                case "л": { toteChars[tIndex] = "ل"; } break;
                                case "м": { toteChars[tIndex] = "م"; } break;
                                case "н": { toteChars[tIndex] = "ن"; } break;
                                case "о": { toteChars[tIndex] = "و"; } break;
                                case "п": { toteChars[tIndex] = "پ"; } break;
                                case "қ": { toteChars[tIndex] = "ق"; } break;
                                case "р": { toteChars[tIndex] = "ر"; } break;
                                case "с": { toteChars[tIndex] = "س"; } break;
                                case "т": { toteChars[tIndex] = "ت"; } break;
                                case "ұ": { toteChars[tIndex] = "ۇ"; } break;
                                case "в": { toteChars[tIndex] = "ۆ"; } break;
                                case "у": { toteChars[tIndex] = "ۋ"; } break;
                                case "ы": { toteChars[tIndex] = "ى"; } break;
                                case "з": { toteChars[tIndex] = "ز"; } break;
                                case "ә": { toteChars[tIndex] = "ءا"; } break;
                                case "ё":
                                case "ө": { toteChars[tIndex] = "ءو"; } break;
                                case "ү": { toteChars[tIndex] = "ءۇ"; } break;
                                case "ч": { toteChars[tIndex] = "چ"; } break;
                                case "ғ": { toteChars[tIndex] = "ع"; } break;
                                case "ш": { toteChars[tIndex] = "ش"; } break;
                                case "ж": { toteChars[tIndex] = "ج"; } break;
                                case "ң": { toteChars[tIndex] = "ڭ"; } break;
                                case "ь": { toteChars[tIndex] = ""; } break;
                                case "Ь": { toteChars[tIndex] = ""; } break;
                                case "ъ": { toteChars[tIndex] = ""; } break;
                                case "Ъ": { toteChars[tIndex] = ""; } break;
                                case "¬": { toteChars[tIndex] = ""; } break;
                                default: { toteChars[tIndex] = chars[j] != "" ? chars[j] : ""; } break;
                            }
                        }
                        string toteWord = string.Concat(toteChars);
                        if (toteWord.Contains("ء"))
                        {
                            toteWord = toteWord.Replace("ء", "");
                            if (!(toteWord.Contains("ك") || toteWord.Contains("گ") || toteWord.Contains("ە")))
                            {
                                toteWord = "ء" + toteWord;
                            }
                        }
                        toteWord = ReplaceDialectWords(toteWord);
                        toteStrs[i - wordLength] = toteWord;
                        cyrlWord = string.Empty;
                    }
                    switch (chars[i])
                    {
                        case ",": { toteStrs[i] = "،"; } break;
                        case "?": { toteStrs[i] = "؟"; } break;
                        case ";": { toteStrs[i] = "؛"; } break;
                        default: { toteStrs[i] = chars[i]; } break;
                    }
                    prevSound = Sound.Unknown;
                    continue;
                }
                cyrlWord += chars[i];
                prevSound = Sound.Unknown;
            }
            toteStrs[length - 1] = "";
            return string.Concat(toteStrs);
        }
        #endregion

        #region Жат кирлл әріптерін төл кирлларыпыне айналдыру +CopycatCyrlToOriginalCyrl(string cyrlText)
        private static string CopycatCyrlToOriginalCyrl(string cyrlText)
        {
            return new StringBuilder(cyrlText)
                .Replace("Ə", "Ә")
                .Replace("ə", "ә")
                .Replace("Ɵ", "Ө")
                .Replace("ɵ", "ө").ToString();
        }
        #endregion

        #region Диалект сөздерді аустыру +ReplaceDialectWords(string word)
        private static string ReplaceDialectWords(string word)
        {
            if (dialectWordsDic.ContainsKey(word)) return dialectWordsDic[word];
            word = System.Text.RegularExpressions.Regex.Replace(word, $@"\w(ۇلى)\s|\w(ۇلى$)", m => string.Format("{0}", m.Groups[0].Value.Replace("ۇلى", " ۇلى")), System.Text.RegularExpressions.RegexOptions.RightToLeft);
            word = System.Text.RegularExpressions.Regex.Replace(word, $@"\w(ۇلىنىڭ)\s|\w(ۇلىنىڭ$)", m => string.Format("{0}", m.Groups[0].Value.Replace("ۇلىنىڭ", " ۇلىنىڭ")), System.Text.RegularExpressions.RegexOptions.RightToLeft);
            word = System.Text.RegularExpressions.Regex.Replace(word, $@"\w(قىزى)\s|\w(قىزى$)", m => string.Format("{0}", m.Groups[0].Value.Replace("قىزى", " قىزى")), System.Text.RegularExpressions.RegexOptions.RightToLeft);
            word = System.Text.RegularExpressions.Regex.Replace(word, $@"\w(قىزىنىڭ)\s|\w(قىزىنىڭ$)", m => string.Format("{0}", m.Groups[0].Value.Replace("قىزىنىڭ", " قىزىنىڭ")), System.Text.RegularExpressions.RegexOptions.RightToLeft);
            word = System.Text.RegularExpressions.Regex.Replace(word, $@"\w(ەۆ)\s|\w(ەۆ)", m => string.Format("{0}", m.Groups[0].Value.Replace("ەۆ", "يەۆ")), System.Text.RegularExpressions.RegexOptions.RightToLeft);
            return word;
        }
        #endregion
    }
}
