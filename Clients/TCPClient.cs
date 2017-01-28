using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Xml;

namespace Deadline
{
    public partial class TCPClient : IClient
    {
        #region Get information

        public void LearnState(GameState state)
        {
            //var line = ReadLine().Trim();
            //while (line != "update")
            //{
            //    state.Update(line);
            //    line = ReadLine().Trim();
            //}
        }

        #endregion

        #region Take actions

        public bool TakeAction(Result c)
        {
            //WriteLineAndFlush("brake " + c.Brake.ToString());
            //WriteLineAndFlush("throttle " + c.Throttle.ToString());
            return false;
        }

        public void Exit()
        {
            //WriteLineAndFlush("EXIT");
            //var res = ReadLine();
            //if (res.Trim() != "OK")
            //    throw new InvalidOperationException("baad input");
        }

        #endregion

        
    }
}
