using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Diagnostics;
using Deadline;
using System.Threading;

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
        return Quality;
    }

    public void FixAndValidate(GameState game)
    {
        // created result may be invalid so this is an optional safe guard
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

public class Solution : SolverBase // TCPSolverBase
{
    GameState State;
    Result Best;

    public Solution(int time) : base(time)
    {
        //state = new GameState();
    }

    public override void GetData()
    {
        State = new GameState();
        //Client.LearnState(state);
    }

    // public override bool Act()
    public override bool Solve()
    {
        Result real;
        SolveMini(0);
        real = Best;
        //for(int i=1;i<10;i++)
        //{
        //    SolveMini(i);
        //    real = real.PickBetter(best);
        //}
        Best = real;
        return true;
    }

    public bool SolveMini(int seed)
    {
        Best = new Result(State);

        // solution

        ImproveAfter(Best);
        return true;
    }

    public void ImproveAfter(Result result)
    {
        return;
    }

    public void Print()
    {
        Best.Print();
    }
}
