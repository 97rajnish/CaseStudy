using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAToolReportGenerator
{
    public class ReportGenerator
    {
        /// <summary>
        /// This Function takes the list of Ndepend Issues and writes it into a string(temp3),
        ///  same as the format of the template which the final report
        ///   has to be and then calls GenerateFinalReport method.
        /// </summary>
        /// <param name="issues"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static int UpdateHTMLFileReportToFinalReport(List<string> issues, int flag)
        {
            Console.WriteLine("Creating Report");
            var path1 = @"C:\Users\320053937\OneDrive - Philips\desktop\template.html";
            string htmlFormat = File.ReadAllText(path1);
            for (int i = 1; i <= 3; i++)
                htmlFormat = WriteIssuesToString(issues, htmlFormat, i);
            string path2 = @"C:\Temp\Report.html";
            return GenerateFinalReport(ref flag, htmlFormat, path2);
        }
        private static string WriteIssuesToString(List<string> issues, string htmlFormat, int i)
        {
            string s = i.ToString();
            string issue1 = issues[i - 1].Split(';')[0];
            string issue2 = issues[i - 1].Split(';')[1];
            string issue3 = issues[i - 1].Split(';')[2];
            string temp1 = htmlFormat.Replace("***NDNAME" + s + "***", issue1);
            string temp2 = temp1.Replace("***NDLEVEL" + s + "***", issue2);
            string temp3 = temp2.Replace("***NDGROUP" + s + "***", issue3);
            htmlFormat = temp3;
            return htmlFormat;
        }
        /// <summary>
        /// This Function takes the list of TicsIssues and writes it into a string(temp3),
        /// same as the format of the template which the final report 
        /// has to be and then calls GenerateFinalReport method.
        /// </summary>
        /// <param name="TicsIssues"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static int UpdateTextFileReportToFinalReport(List<string> TicsIssues, int flag)
        {
            Console.WriteLine("creating Report");
            var path1 = @"C:\Users\320053937\OneDrive - Philips\desktop\template1.html";
            string txtFormat = File.ReadAllText(path1);
            for (int i = 1; i <= 10; i++)
                txtFormat = WriteTicsIssuesToString(TicsIssues, txtFormat, i);
            string path2 = @"C:\Temp\Report.html";
            return GenerateFinalReport(ref flag, txtFormat, path2);
        }
        private static string WriteTicsIssuesToString(List<string> TicsIssues, string txtFormat, int i)
        {
            string issue1 = TicsIssues[i - 1].Split(';')[0];
            string issue2 = TicsIssues[i - 1].Split(';')[1];
            string issue3 = TicsIssues[i - 1].Split(';')[2];
            string temp1 = ""; string temp2 = ""; string temp3 = "";
            string s = i.ToString();
    
                temp1 = txtFormat.Replace("***TCNAME" + s + "***", issue1);
                temp2 = temp1.Replace("***TCLEVEL" + s + "***", issue2);
                temp3 = temp2.Replace("***TCGROUP" + s + "***", issue3);
            
            txtFormat = temp3;
            return txtFormat;
        }

        /// <summary>
        /// This method creates a file if the file doesn't exist and if it does, it writes the 
        /// string(temp3) into the file by checking it with flag i,e;
        ///  if the file has some content it just appends, if not it just writes.
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="temp3"></param>
        /// <param name="path2"></param>
        /// <returns></returns>
        public static int GenerateFinalReport(ref int flag, string temp3, string path2)
        {
            if (!File.Exists(path2))
            {
                var myfile = File.Create(path2);
                myfile.Close();
            }
            if (flag == 0)
            {
                StreamWriter tw = new StreamWriter(path2);
                tw.WriteLine(temp3);
                tw.Close();
                flag = 1;
            }
            else
            {
                StreamWriter tw = new StreamWriter(path2, true);
                tw.WriteLine(temp3);
                tw.Close();
            }
            return flag;
        }
    }
}
