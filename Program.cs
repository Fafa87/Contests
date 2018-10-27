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
            Solution solver = new Solution(new IOClient(), 1);
            solver.GetData();
            solver.Act();
        }

        public static void RunClientWithSimulator()
        {
            RunSimulator();
            RunClient("127.0.0.1", 500);
        }

        public static void RunClient(TCPClient client, Func<TCPClient, SolutionBase> newSolver)
        {
            SolutionBase solver = newSolver(client);
            while (true)
            {
                //if (client.TurnLeftIsNewGame().Item2)
                //    solver.Restart();

                solver.GetData();
                if (solver.Act() == false)
                    break;

                //client.Wait();
            }
        }

        public static void RunClient(string server, int port)
        {
            var client = new TCPClient(server, port);
            client.Login();
            RunClient(client, (c) => new Solution(client));
            client.Exit();
        }

        public static void RunClientAndRestartAutomatically(string server, int port)
        {
            while (true)
            {
                try
                {
                    RunClient(server, port);
                }
                catch (IOException e)
                {
                    Console.Error.WriteLine("Error occured: " + e.Message);
                    Console.Error.WriteLine("Will try to reconnect");
                }
                Thread.Sleep(1000); // wait one second, not to spam server in case of errors
            }
        }

        public static void RunSimulator()
        {
            var simulatorInfo = new ProcessStartInfo()
            {
                CreateNoWindow = false,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WorkingDirectory = "Simulator",
                FileName = "java",
                Arguments = string.Format(@"-jar ""simulator.jar"" --level=..\Input\level{0}.in --tcp=500", GameState.LevelNumber)
            };
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

            var agentInfo = new ProcessStartInfo()
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                FileName = "agent",
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = string.Format("'Simulator ..\\Input\\level{0}.in'", GameState.LevelNumber).Replace('\'', '"')
            };
            Process.Start(agentInfo);
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

