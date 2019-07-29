using System;
using System.Collections.Generic;
using System.IO;
using HtmlAgilityPack;
using System.Configuration;

namespace SAToolReportGenerator
{

    public class Program
    {
        # region Global variables
        public static List<string> NDIssues = new List<string>() { };
        public static List<string> TicsIssues = new List<string> { };
        public static int flag = 0;
        # endregion Global variables
    
        static void Main(string[] args)
        {
            string reportPath = ConfigurationManager.AppSettings["ReportPath"];
            List<string> listOfReportFiles = GetReportFiles(reportPath);
            ReportParserAndGenerator(listOfReportFiles);
        }

        #region private functions
        /// <summary>
        /// This method takes path as input in which all reports are placed and returns only
        /// the required list of report files.
        /// </summary>
        /// <param name="reportPath"></param>
        /// <returns></returns>
        public static List<string> GetReportFiles(string reportPath)
        {
            List<string> listOfFiles = new List<string>();
            string s1 = "TICSReport.txt"; string s2 = "NDependReport.html";
            DirectoryInfo DirectoryPath = new DirectoryInfo(reportPath);
            FileInfo[] FilesInDirectory = DirectoryPath.GetFiles("*.*"); //Getting All files
            foreach (FileInfo file in FilesInDirectory)
            {
                if (s1 == file.Name || s2 == file.Name)
                {
                    listOfFiles.Add(file.FullName);
                }
            }
            return listOfFiles;
        }

        /// <summary>
        /// This method Parses all the reports with the help of ParserHTML and TextFileParser
        /// and Generates Report using ReportGenerator Class.
        /// </summary>
        /// <param name="listOfReports"></param>
        private static void ReportParserAndGenerator(List<string> listOfReports)
        {
            foreach (string reportFileName in listOfReports)
            {
                string fileFormat = GetFileFormat(reportFileName);
                switch (fileFormat)
                {
                    case "html":
                        NDIssues = ParserHTML(reportFileName);
                        flag = ReportGenerator.UpdateHTMLFileReportToFinalReport(NDIssues, flag);
                        break;
                    case "txt":
                        TicsIssues = TextFileParser(reportFileName);
                        flag = ReportGenerator.UpdateTextFileReportToFinalReport(TicsIssues, flag);
                        break;
                    default:
                        Console.WriteLine($"{fileFormat} cannot be parsed");
                        break;
                }
            }
        }

        /// <summary>
        /// This Method takes filename as input and returns the extension of the file
        /// i,e; html or txt.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFileFormat(string fileName)
        {
            string extension = string.Empty;
            extension = fileName.Substring(fileName.LastIndexOf('.') + 1);
            return extension;
        }

        /// <summary>
        /// This method takes the Html report file path as input and parses it with the help of 
        /// HTMLParser class and stores the top 3 issues in a list and returns the list.
        /// </summary>
        /// <param name="reportFilePath"></param>
        /// <returns></returns>
        static List<string> ParserHTML(string reportFilePath)
        {
            Console.WriteLine("Html File Parsing");
            HtmlDocument htmlDocument = HTMLParser.ReadHTMLFile(reportFilePath);
            string innerText = HTMLParser.GetHTMLNodeInnerText(htmlDocument, ConfigurationManager.AppSettings["NDReportIssueTableClass"]);
            string[] issueArray = innerText.Split(new[] { "&nbsp;&nbsp;&nbsp;" }, StringSplitOptions.None);
            NDIssues.Add(issueArray[1]);
            NDIssues.Add(issueArray[2]);
            NDIssues.Add(issueArray[3]);
            NDIssues = HTMLParser.FilterNDIssue(NDIssues);
            return NDIssues;
        }
        /// <summary>
        /// This method takes the Text report file path as input and parses it with the help of
        /// TextParser class and stores the top 3 level issues and returns the list.
        /// </summary>
        /// <param name="reportFilePath"></param>
        /// <returns></returns>
        static List<string> TextFileParser(string reportFilePath)
        {
            Console.WriteLine("Text File Parsing");
            string text = File.ReadAllText(reportFilePath);
            int Level = 1; int level1startindex;
            string nosuchlevel = "Doesn't exist"; string s = "";
            List<string> str = TextParser.StringsOfDifferentLevels();
            while (Level <= 3)
            {
                level1startindex = text.IndexOf(str[Level]);
                if (level1startindex == -1)
                {
                    string nosuchlevel1 = nosuchlevel + "; " + Level.ToString() + "; invalid ";
                    TicsIssues.Add(nosuchlevel1);
                }
                else
                {
                    s = TextParser.GetSameLevelIssues(Level, text, str);
                    s = TextParser.ConvertIssuesIntoTicsIssuesFormat(s);
                    TicsIssues.Add(s);
                }
                Level++;
            }
            return TicsIssues;
        }

        #endregion private functions
    }
}
