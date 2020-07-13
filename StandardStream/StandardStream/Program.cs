using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardStream
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Process proc = new Process())
            {
                proc.StartInfo.FileName = @"C:\Users\jhpark\source\repos\tct\StandardStream\EchoClient\bin\Debug\EchoClient.exe";
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;

                proc.Start();

                StreamWriter sw = proc.StandardInput;
                StreamReader sr = proc.StandardOutput;

                string inputText;
                int numLines = 0;

                do
                {
                    Console.WriteLine("Enter a line of text (or press the Enter key to stop):");

                    inputText = Console.ReadLine();

                    if (inputText.Length > 0)
                    {
                        numLines++;
                        sw.WriteLine(inputText);
                    }
                } while (inputText.Length > 0);

                sw.Close();

                string msg = sr.ReadToEnd();
                proc.WaitForExit();

                Console.WriteLine(msg);
                Console.WriteLine("Press any key to exit!");
                Console.ReadLine();
            }
        }
    }
}
