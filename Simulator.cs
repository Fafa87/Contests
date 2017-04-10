using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deadline
{
    public class Simulator
    {
        public GameState GameState;

        /// <summary>
        /// maps drone id into DronePosition (current simulation position)
        /// </summary>
        //public Dictionary<int, DronePosition> Drones = new Dictionary<int, DronePosition>();
        public int Turn = 0;

        public Simulator(GameState gamestate)
        {
            this.GameState = gamestate;
        }

        /// <summary>
        /// Processes all actions. Returns list of actions which have been processed and list of ignored actions
        /// </summary>
        public Tuple<List<Action>, List<Action>> Process(List<Action> actions)
        {
            return NextTurn(actions, true);
        }

        /// <summary>
        /// Processes actions. Returns list of actions which have been processed and list of ignored actions.
        /// Move to the next event
        /// </summary>
        public Tuple<List<Action>, List<Action>> NextTurn(List<Action> actions, bool processAllInGivenOrder = false)
        {
            var actionsIgnored = new List<Action>();
            var actionsProcessed = new List<Action>();

            foreach (var action in actions)
            {
                // process or ignore actions
                actionsProcessed.Add(action);
            }

            if (!processAllInGivenOrder)
            {
                // update turns
                int turnsToSkip = ComputeTurnsToSkip();
                Turn += turnsToSkip;
            }

            return Tuple.Create(actionsProcessed, actionsIgnored);
        }

        public int ComputeTurnsToSkip()
        {
            return 1; 
        }

        public int GetScore()
        {
            // return current score
            return 0;
        }
    }
}
