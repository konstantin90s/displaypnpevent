using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace displaypnpevent
{
    static class Program
    {
        [STAThread]
        static int Main(string[] args)
        {
            var execPathSingle = string.Empty;
            var execPathMultiple = string.Empty;
            var argsLength = args.Length;
            switch (argsLength)
            {
                case 0:
                    return 0;
                case 1:
                    break;
                default:
                    break;
            }
            var screenLengthPrevious = Screen.AllScreens.Length;
            while (true)
            {
                var screenCountCurrent = Screen.AllScreens.Length;
                if (screenCountCurrent != screenLengthPrevious)
                {
                    Thread.Sleep(5000);
                    if (screenCountCurrent <= 1)
                    {

                    }
                    else
                    {

                    }
                }
                screenLengthPrevious = screenCountCurrent;
                Thread.Sleep(5000);
            }
        }
    }
}
