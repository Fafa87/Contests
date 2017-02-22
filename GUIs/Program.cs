using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Deadline;
using System.Threading;

namespace GUIs
{
    static class Program
    {
        public static SolverUI solverUI;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            var thread = new Thread(new ThreadStart(() =>
            {
                SolverUI solver = new SolverUI(new IOClient());
                solver.GetData();
                solver.Act();
            }));
            thread.Start();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new UI());
        }
    }
}
