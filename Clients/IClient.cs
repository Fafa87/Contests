using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deadline
{
    public interface IClient
    {
        void LearnState(GameState state);
        bool TakeAction(Result res);
    }
}
