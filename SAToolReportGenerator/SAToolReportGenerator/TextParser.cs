using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAToolReportGenerator
{
    public class TextParser
    {

        public static List<string> StringsOfDifferentLevels()
        {
            List<string> str = new List<string>() { };
            str.Add(" ");
            str.Add("Level: 1)");
            str.Add("Level: 2)");
            str.Add("Level: 3)");
            return str;
        }
        /// <summary>
        /// This method returns a string which contains issues of same level seperated by '$'.
        /// </summary>
        /// <param name="Level"></param>
        /// <param name="text"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetSameLevelIssues(int Level, string text, List<string> str)
        {
            string category1 = "";
            string s1 = ""; string strlevel = "";
            string s = ""; int level1startindex1;
            for (int i = 0; i < str[Level].Length; i++)
            {
                if (Char.IsDigit(str[Level][i]))
                    strlevel = strlevel + str[Level][i];
            }
            int level1startindex = text.IndexOf(str[Level]); ;
            while (level1startindex != -1)
            {
                level1startindex1 = level1startindex;
                level1startindex = level1startindex + str[Level].Length + 1;
                int level1endindex = text.IndexOf("\r\n", level1startindex);
                s1 = text.Substring(level1startindex, level1endindex - level1startindex);
                if (s1[0] == '\n')
                    s1 = s1.Remove(0, 1);
                while (s1[0] == ' ')
                    s1 = s1.Remove(0, 1);
                s1 = s1 + "@";
                s1 = s1.Replace(".@", " ; " + strlevel + " ;@");
                level1startindex1 = level1startindex1 - 3;
                while (text[level1startindex1] != ':')
                {
                    category1 = category1 + text[level1startindex1];
                    level1startindex1--;
                }
                category1 = new string(category1.Reverse().ToArray());
                s1 = s1.Replace("@", category1 + " ; " + "$");
                category1 = category1.Replace(category1, string.Empty);
                s = s + s1;
                level1startindex = text.IndexOf(str[Level], level1endindex);
            }
            return s;
        }
        /// <summary>
        /// This methods takes the output of GetSameLevelIssues as input and then converts
        /// it into the format of Ticsissue and returns it.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ConvertIssuesIntoTicsIssuesFormat(string s)
        {
            List<string> Name1 = new List<string>() { };
            List<string> Level1 = new List<string>() { };
            List<string> Category1 = new List<string>() { };
            string[] items1 = s.Split('$');
            s = s.Replace(s, string.Empty);
            for (int i = 0; i < items1.Length - 1; i++)
            {
                Name1.Add(items1[i].Split(';')[0]);
                Name1[i] = Name1[i] + ",\n";
                Level1.Add(items1[i].Split(';')[1]);
                Level1[i] = Level1[i] + ",\n";
                Category1.Add(items1[i].Split(';')[2]);
                Category1[i] = Category1[i] + ",\n";
            }
            foreach (string st in Name1)
            { s = s + st; }
            s = s + ";\n";
            foreach (string st in Level1)
            { s = s + st; }
            s = s + ";\n";
            foreach (string st in Category1)
            { s = s + st; }
            s = s + ";";

            return s;
        }
    }
}
