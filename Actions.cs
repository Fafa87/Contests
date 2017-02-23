using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deadline
{
    public class SolutionAction // or decision or choice
    {
        public Rectangle slice;
        GameState gameState;

        int numberOfOneType;
        public SolutionAction(GameState state, int r1, int r2, int c1, int c2)
        {
            gameState = state;
            numberOfOneType = 0;
            slice = new Rectangle(new Point(c1, r1), new Point(c2, r2));
            for (int i = r1; i < r2; i++)
                numberOfOneType += gameState.Rows[i].GetRange(c1, c2 - c1).Sum();
        }

        public bool IsValid()
        {
            return slice.Area <= gameState.MaxArea && numberOfOneType >= gameState.MinBoth && slice.Area - numberOfOneType >= gameState.MinBoth;
        }

        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3}", (int)slice.Bottom, (int)slice.Left, (int)slice.Top-1, (int)slice.Right-1);
        }
    }

}
