using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Deadline;

namespace Testing
{
    public class ClientMock : IServer
    {
        int current;
        Player[] players = new [] {new Player(), new Player()};
        class Player 
        {
            
        }

        public ClientMock()
        {
            current = 0;
        }

        public void SetConnection(int player)
        {
            current = player;
        }

        public void WhatsUp()
        {

        }

        public void ProcessTurn() 
        {

        }

        public List<decimal> DescribeWorld()
        {
            throw new NotImplementedException();
        }

        public int TurnsLeft()
        {
            throw new NotImplementedException();
        }

        public void Wait()
        {
            throw new NotImplementedException();
        }
    }
}
