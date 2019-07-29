using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace SAToolReportGenerator
{
    public class HTMLParser
    {
        /// <summary>
        /// This method takes input of HTML file path and read the content to a string and then
        /// writes it in a htmldocument and returns the htmldocument to parse.
        /// </summary>
        /// <param name="reportFilePath"></param>
        /// <returns></returns>
        public static HtmlDocument ReadHTMLFile(string reportFilePath)
        {
            var htmlDocument = new HtmlDocument();
            string htmlContent = File.ReadAllText(reportFilePath);
            htmlDocument.LoadHtml(htmlContent);
            return htmlDocument;
        }
        /// <summary>
        /// This method takes the htmldocument and the table class name as input to search the 
        /// document with that table class name  and then returns the innertext of the table.
        /// </summary>
        /// <param name="htmlDocument"></param>
        /// <param name="nodeToSearch"></param>
        /// <returns></returns>
        public static string GetHTMLNodeInnerText(HtmlDocument htmlDocument, string nodeToSearch)
        {
            var nodes = htmlDocument.DocumentNode.SelectNodes(nodeToSearch).ToList();
            var node = nodes[1];
            string str = node.InnerText;
            return str;
        }
        /// <summary>
        /// This methods takes input as a list of issues and then modifies it by segragating them
        /// by ';' and then returns it so that it can be easily added to the final report.
        /// </summary>
        /// <param name="issues"></param>
        /// <returns></returns>
        public static List<string> FilterNDIssue(List<string> issues)
        {
            List<string> ndIssues = issues;
            for (int i = 0; i < ndIssues.Count(); i++)
            {
                for (int j = 0; j < ndIssues[i].Length; j++)
                {
                    if (Char.IsDigit(ndIssues[i][j]))
                    {
                        ndIssues[i] = ndIssues[i].Insert(j, ";");
                        ndIssues[i] = ndIssues[i].Remove(j + 1, 3);
                    }
                }
                string issuelevel = (i + 1).ToString();
                if (ndIssues[i].Contains("; types"))
                    ndIssues[i] = ndIssues[i].Replace("; types", "; " + issuelevel + "; ");
                if (ndIssues[i].Contains("; methods"))
                    ndIssues[i] = ndIssues[i].Replace("; methods", "; " + issuelevel + "; ");
                if (ndIssues[i].Contains("; type"))
                    ndIssues[i] = ndIssues[i].Replace("; type", "; " + issuelevel + "; ");
                if (ndIssues[i].Contains("; method"))
                    ndIssues[i] = ndIssues[i].Replace("; method", "; " + issuelevel + "; ");
            }
            return ndIssues;
        }
    }
}
