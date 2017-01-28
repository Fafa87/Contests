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

    public override bool Act()
    {
        Result real;
        SolveMini(0);
        real = best;
        //for(int i=1;i<10;i++)
        //{
        //    SolveMini(i);
        //    real = real.PickBetter(best);
        //}
        best = real;
        return true;
    }

    public bool SolveMini(int seed)
    {
        best = new Result(state);

        // solution

        ImproveAfter(best);
        return true;
    }

    public void ImproveAfter(Result result)
    {
        return;
    }
}
