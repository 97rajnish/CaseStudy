using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Helloworld
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputString = "Hello World";
            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            Print(inputString);
            StringBuilder str = stringWriter.GetStringBuilder();
            string consoleOutput = str.ToString();
            consoleOutput = consoleOutput.Replace(System.Environment.NewLine, string.Empty);
            stringWriter.Close();
            if (string.Equals(consoleOutput, inputString))
                MessageBox.Show("Execution Successful");
            else
                MessageBox.Show("Execution UnSuccessful");


        }
        public static void Print(string input)
        {
            Console.WriteLine(input);

        }
    }
}