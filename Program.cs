using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
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
            Solution solver = new Solution(1);
            solver.GetData();
            solver.Solve();
            solver.Print();
        }

        public static void RunClientWithSimulator()
        {
            RunSimulator();
            RunClient("127.0.0.1", 500);
        }

        public static void RunClient(TCPClient client, Func<TCPClient, TCPSolverBase> newSolver)
        {
            TCPSolverBase solver = newSolver(client);
            while (true)
            {
                solver.GetData();
                if (solver.Act() == false)
                    break;
            }
        }

        public static void RunClient(string server, int port)
        {
            var client = new TCPClient(server, port);
            client.Login();
            RunClient(client, (c) => new TCPSolverBase(client));
            client.Exit();
        }

        public static void RunSimulator()
        {
            var simulatorInfo = new ProcessStartInfo();
            simulatorInfo.CreateNoWindow = false;
            simulatorInfo.UseShellExecute = false;
            simulatorInfo.RedirectStandardOutput = true;
            simulatorInfo.RedirectStandardError = true;
            simulatorInfo.WorkingDirectory = "Simulator";
            simulatorInfo.FileName = "java";
            simulatorInfo.Arguments = string.Format(@"-jar ""simulator.jar"" --level=..\Input\level{0}.in --tcp=500", GameState.LevelNumber);
            var simulatorProcess = new Process
            {
                StartInfo = simulatorInfo,
                EnableRaisingEvents = true
            };
            simulatorProcess.Exited += (sender, args) =>
            {
                Console.WriteLine("Symulator został zamknięty. Output:");
                Console.WriteLine(((Process)sender).StandardOutput.ReadToEnd());
                Console.WriteLine(((Process)sender).StandardError.ReadToEnd());
                Environment.Exit(1);
            };
            simulatorProcess.Start();

            var agentInfo = new ProcessStartInfo();
            agentInfo.CreateNoWindow = true;
            agentInfo.UseShellExecute = false;
            agentInfo.FileName = "agent";
            agentInfo.WindowStyle = ProcessWindowStyle.Hidden;
            agentInfo.Arguments = string.Format("'Simulator ..\\Input\\level{0}.in'", GameState.LevelNumber).Replace('\'', '"');
            Process.Start(agentInfo);
        }

        static void Main(string[] args)
        {
            Console.OpenStandardInput();
            Console.OpenStandardOutput();

            RunNCases();
            // RunClient(args[0], Int32.Parse(args[1]));
        }
    }
}

