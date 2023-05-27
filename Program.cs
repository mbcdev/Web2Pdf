using System;
using System.Diagnostics;
using System.IO;

namespace Web2Pdf
{
    internal class Program
    {
        static int Main(string[] args)
        {
            var config = Web2PdfConfig.FromArgs(args);
            if (!config.IsValid())
            {
                PrintUsage();
                return 1;
            }

            var browser = new Web2PdfWebBrowser(config);
            if(!browser.WaitForCompletion())
            {
                return 1;
            }

            return 0;
        }
        private static void PrintUsage()
        {
            string exeName = Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);

            Console.WriteLine("Usage:");
            Console.WriteLine($"{exeName} <URL> [/ts] <OutputFile>");
            Console.WriteLine();
            Console.WriteLine("Parameters:");
            Console.WriteLine("/ts\tAdd Timestamp to Output File Name");
        }
    }
}
