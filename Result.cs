using Deadline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Result
{
    public long Quality;
    public GameState State;
    public List<SolutionAction> Actions = new List<SolutionAction>();

    public Result(GameState state)
    {
        this.State = state;
    }

    public Result(GameState state, List<SolutionAction> act)
    {
        this.State = state;
        Actions = act.ToList();
        // calculate quality
        CalculateQuality();
    }

    public long CalculateQuality()
    {
        Quality = 0;
        Quality -= Actions.Sum(p=>(long)p.slice.Area);
        return Quality;
    }

    public void FixAndValidate()
    {
        // created result may be invalid so this is an optional safe guard
        Actions.RemoveAll(p => p.IsValid() == false);
    }

    public void Print()
    {
        Console.Out.WriteLine(Actions.Count);

        foreach (var act in Actions)
        {
            Console.Out.WriteLine(act.ToString());
        }
    }

    public Result PickBetter(Result other)
    {
        return CalculateQuality() <= other.CalculateQuality() ? this : other;
    }
}
