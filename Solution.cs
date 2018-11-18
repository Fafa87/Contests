using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Diagnostics;
using Deadline;
using System.Threading;

// TODO 
// - mapa z oddzielonych spacjami liczb, a nie tylko stringi => może ustawianie całego rzędu? ale to normalne przypisanie
// - iterowanie po wszystkich pozycjach

/*
 * int kolorBud = -1;
    public void dfs2Bud(HotSpot hot, GridPoint start, int startbasin)
    {
        if (budyn[start] == kolorBud)
            return;
        budyn[start] = kolorBud;
        hot.points.Add(start);

        foreach (var move in Moves.All4)
        {
            var newpos = move.Move(start);
            if (mapa.IsInside(newpos))
            {
                if (mapa[newpos] == startbasin)
                {
                    budyn[start] = kolorBud;
                    dfs2Bud(hot, newpos, startbasin);
                }
            }
        }
    }
 * */

public class Solution : SolutionBase
{
    public Solution(IClient client, int time = 0)
        : base(client, time)
    { }

    List<int> values;
    public override void GetData()
    {
        values = Console.ReadLine().ParseList<int>();
    }

    public override bool Act()
    {
        int sum = values.Sum();

        Console.Out.WriteLine(sum);
        return true;
    }
}
