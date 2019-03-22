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
