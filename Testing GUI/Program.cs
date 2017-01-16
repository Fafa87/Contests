using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using Testing;
using Deadline;
using GUIs;

namespace TestingGUI
{
    static class Program
    {
        public static MockSolverUI solverUI;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            ClientMock client = new ClientMock();
            solverUI = null;

            var thread = new Thread(new ThreadStart(() =>
            {
                var test = new Testing.Program();
                test.Run((server) =>
                    {
                        var solver = new MockSolverUI(server);
                        if (solverUI == null)
                            solverUI = solver;
                        return (object)solver as MockSolverBase;
                    });
            }));
            thread.Start();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new UI());
        }
    }
}
