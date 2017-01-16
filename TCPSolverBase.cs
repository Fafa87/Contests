using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deadline
{
    public class TCPSolverBase
    {
        public TCPClient Client;
        public TCPSolverBase(TCPClient client)
        {
            this.Client = client;
        }

        public virtual void GetData()
        {
            throw new NotImplementedException();
        }

        public virtual bool Act()
        {
            throw new NotImplementedException();
        }
    }
}
