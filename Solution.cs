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
    public Solution(IClient client, int time = 0)
        : base(client, time)
    { }

    public override void GetData()
    {
        base.GetData();
    }

    public override bool Act()
    {
        int H = int.Parse(Console.ReadLine());
        int S = int.Parse(Console.ReadLine());
        int[] regions = new int[S];
        for (int i = 0; i < S; ++i)
        {
            regions[i] = int.Parse(Console.ReadLine());
        }
        int R = int.Parse(Console.ReadLine());
        int[] oldColors = new int[R];
        for (int i = 0; i < R; ++i)
        {
            oldColors[i] = int.Parse(Console.ReadLine());
        }

        MapRecoloring mr = new MapRecoloring();
        int[] ret = mr.recolor(H, regions, oldColors);

        Console.WriteLine(ret.Length);
        for (int i = 0; i < ret.Length; ++i)
        {
            Console.WriteLine(ret[i]);
        }
        return true;
    }

    public bool SolveMini(int seed)
    {
        best = new Result(state);

        // solution

        ImproveAfter(best);
        best.CalculateQuality();
        return true;
    }

    public void ImproveAfter(Result result)
    {
        return;
    }
}
