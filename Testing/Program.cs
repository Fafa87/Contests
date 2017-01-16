using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Deadline;

namespace Testing
{
    public class Program
    {
        ClientMock client = new ClientMock();

        Func<IServer, MockSolverBase> newSolver;

        void ProcessSolver(MockSolverBase solver, int id)
        {
            client.SetConnection(id);
            solver.NewTurn();
            client.Wait();
            if (client.TurnsLeft() == 0)
            {
                client.Wait();
                solver = new MockSolverBase(client);
                solver.Setup();
            }
        }

        public void Run(Func<IServer, MockSolverBase> newSolver)
        {
            this.newSolver = newSolver;

            client.SetConnection(0);
            MockSolverBase solver = this.newSolver(client);

            System.Threading.Thread.Sleep(1000); // so SOLVER random is different

            client.SetConnection(1);
            MockSolverBase solver2 = this.newSolver(client);
            while (true)
            {
                client.WhatsUp();

                ProcessSolver(solver, 0);
                ProcessSolver(solver2, 1);

                client.ProcessTurn();
                System.Threading.Thread.Sleep(800);
            }
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run((client) =>
            {
                MockSolverBase solver = new MockSolverBase(client);
                solver.Setup();
                return solver;
            });
        }
    }
}
