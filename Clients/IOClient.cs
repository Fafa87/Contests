using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deadline
{
    public class IOClient : IClient
    {
        public void LearnState(GameState game)
        {
            var data = Console.ReadLine().ParseList<int>();
            for (int i = 0; i < data[0]; i++)
                game.Rows.Add(Console.ReadLine().Select(p=>p=='M' ? 0 : 1).ToList());
            game.MinBoth = data[2];
            game.MaxArea = data[3];
        }

        public bool TakeAction(Result r)
        {
            r.Print();
            return true;
        }
    }
}
