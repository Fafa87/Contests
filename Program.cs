using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading;
using Deadline;


namespace Deadline
{
    public static partial class Program
    {
        public static void RunNCases()
        {
            int num;
            var line = Console.ReadLine();
            line = line.ParseToken(out num);

            for (int i = 0; i < num; i++)
            {
                RunCase();
            }
        }

        public static void RunCase()
        {
            Solution solver = new Solution(null, 1);
            solver.GetData();
            solver.Act();
        }

        static void Main(string[] args)
        {
            Console.OpenStandardInput();
            Console.OpenStandardOutput();

            RunCase();
            //RunNCases();
            // RunClient(args[0], Int32.Parse(args[1]));
            // RunClientAndRestartAutomatically(args[0], Int32.Parse(args[1]));
            // RunClientWithSimulator();
        }
    }
}

