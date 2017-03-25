using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace agent
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
                return;
            Thread.Sleep(5000);
            do
            {
                var java = Process.GetProcessesByName("java")
                    .FirstOrDefault(p => args.Any(a => p.MainWindowTitle.Equals(a, StringComparison.InvariantCultureIgnoreCase)));
                if (java == null)
                    return;

                if (java.ParentProcess() == null)
                {
                    java.Kill();
                    return;
                }

                Thread.Sleep(100);
            } while (true);
        }
    }
}
