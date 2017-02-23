using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Diagnostics;
using Deadline;
using System.Threading;

public class Solution : SolutionBase
{
    public Solution(IClient client, int time = 0) : base(client, time)
    {}

    public override void GetData()
    {
        base.GetData();
    }

    public override bool Act()
    {
        Result real;
        SolveMini(0);
        real = best;
        for (int i = 1; i < 10; i++)
        {
            SolveMini(i);
            real = real.PickBetter(best);
        }
        
        best = real;
        TakeBestAction();
        return true;
    }

    public bool SolveMini(int seed)
    {
        seed += 2;
        best = new Result(state);

        if (seed < state.Rows.First().Count)
        {
            for (int i = 0; i < state.Rows.Count - seed; i += seed)
            {
                var proposedSolution = new SolutionAction(state, i, i + seed, 0, seed);
                if(proposedSolution.IsValid())
                    best.Actions.Add(proposedSolution);
            }
        }
        best.FixAndValidate();
        best.CalculateQuality();
        // solution

        ImproveAfter(best);
        return true;
    }

    public void ImproveAfter(Result result)
    {
        return;
    }
}
