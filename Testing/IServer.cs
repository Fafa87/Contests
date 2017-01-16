using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deadline
{
    public interface IServer
    {
        #region Get information

        List<decimal> DescribeWorld();

        int TurnsLeft();

        #endregion 

        #region Take actions

        void Wait();

        #endregion
    }

    /// <summary>
    /// Mock just a stub, it should be in TCPSolverBase somehow
    /// </summary>
    public class MockSolverBase : TCPSolverBase
    {
        private IServer client;

        public MockSolverBase(IServer client) : base(client as TCPClient)
        {
            // TODO: Complete member initialization
            this.client = client;
        }

        internal void NewTurn()
        {
            throw new NotImplementedException();
        }

        internal void Setup()
        {
            throw new NotImplementedException();
        }
    }
}
