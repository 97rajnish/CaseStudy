using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SAToolReportGenerator
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string s1 = @"C:\Users\320053937\Documents\TICSReport.txt";
            List<string> listoffiles = new List<string>();
            List<string> listOfFiles = new List<string>();
            string reportPath = @"C:\Users\320053937\Documents";
            listoffiles.Add(s1);
            
            listOfFiles = Program.GetReportFiles(reportPath);
            for(int i = 0; i < 1; i++)
            Assert.AreEqual(listoffiles[i], listOfFiles[i]);           
        }
        [TestMethod]
        public void TestMethod2()
        {
            string s1 = "html";
            string s2 = "txt";
            string filename = @"C:\Users\320063801\OneDrive - Philips\TRAINING.NET\SampleConsoleApp\NDependOut1\NDependReport.html";
            string actual = Program.GetFileFormat(filename);
            Assert.IsTrue(actual.Equals(s1) || actual.Equals(s2));
        }

    }
}

