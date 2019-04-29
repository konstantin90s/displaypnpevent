using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace displaypnpevent
{
    static class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOWMINIMIZED = 2;
        const int SW_SHOW = 5;

        [STAThread]
        static int Main(string[] args)
        {
            var screenCountPrevious = SystemInformation.MonitorCount;
            var argsLength = args.Length;
            if (argsLength == 0)
            {
                Console.WriteLine(screenCountPrevious);
                return 0;
            }
            var execPathMultiple = string.Empty;
            var execPathMultipleArgs = string.Empty;
            var execPathSingle = string.Empty;
            var execPathSingleArgs = string.Empty;
            if (argsLength >= 1)
            {
                execPathMultiple = args[0];
                if (execPathMultiple.CompareTo("/h") == 0 ||
                    execPathMultiple.CompareTo("-h") == 0 ||
                    execPathMultiple.CompareTo("--help") == 0)
                {
                    Console.WriteLine("usage: " + System.AppDomain.CurrentDomain.FriendlyName +
                                      " [path to executable] [path to executable]");
                    Console.WriteLine(@"
       /h or -h or --help           display this usage reference
       [no arguments specified]     output the number of displays currently connected
       [1st argument specified]     this path will be executed on connection of each additional display: 2nd, 3rd, etc
       [2nd argument specified]     this path will be executed on display disconnection and only one left as a result");
                    return 0;
                }
                var execPathMultipleArr = execPathMultiple.Split(new[] { "\"" }, StringSplitOptions.RemoveEmptyEntries);
                execPathMultiple = execPathMultipleArr[0];
                if (execPathMultipleArr.Length > 1)
                {
                    execPathMultipleArgs = execPathMultipleArr[1].Trim();
                }
            }
            if (argsLength == 2)
            {
                execPathSingle = args[1];
                var execPathSingleArr = execPathSingle.Split(new[] { "\"" }, StringSplitOptions.RemoveEmptyEntries);
                execPathSingle = execPathSingleArr[0];
                if (execPathSingleArr.Length > 1)
                {
                    execPathSingleArgs = execPathSingleArr[1].Trim();
                }
            }
            else
            {
                Console.WriteLine("error: argument count cannot exceed 2, probably missing quotes in file path");
                return 1;
            }
            ShowWindow(GetConsoleWindow(), SW_HIDE);
            while (true)
            {
                var screenCountCurrent = SystemInformation.MonitorCount;
                if (screenCountCurrent != screenCountPrevious)
                {
                    if (screenCountCurrent > 1)
                    {
                        try
                        {
                            Process.Start(new ProcessStartInfo()
                            {
                                FileName = execPathMultiple,
                                Arguments = execPathMultipleArgs,
                            });
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        try
                        {
                            Process.Start(new ProcessStartInfo()
                            {
                                FileName = execPathSingle,
                                Arguments = execPathSingleArgs,
                            });
                        }
                        catch
                        {
                        }
                    }
                }
                screenCountPrevious = screenCountCurrent;
                Thread.Sleep(1000);
            }
        }
    }
}
