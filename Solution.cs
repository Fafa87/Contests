using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Diagnostics;
using Deadline;
using System.Threading;
using Algorithms;

public class Solution : SolutionBase
{
    public static Moves4[] All4 = new[] { Moves4.Right, Moves4.Down, Moves4.Left, Moves4.Up};
    public Solution(IClient client, int time = 0)
        : base(client, time)
    { }

    List<int> values;
    public override void GetData()
    {
    }

    class Alien
    {
        public Point pos;
        public Point curPos;
        public int direction = 0;
        public double speed = 1;
        public int StartTime = 0;

        public List<Point> track = new List<Point>();

        public List<Point> CalcTrack(List<string> commands)
        {
            curPos = pos;
            track.Clear();
            track.Add(curPos);
            for (int i = 0; i < commands.Count(); i += 2)
            {
                if (commands[i][0] == 'F')
                {
                    var number = commands[i + 1].ParseAllTokens<int>();
                    for (int i2 = 0; i2 < number; i2++)
                    {
                        curPos = All4[direction].Move(curPos, 1);
                        track.Add(curPos);
                    }
                }
                else
                {
                    var number = commands[i + 1].ParseAllTokens<int>();
                    for (int i2 = 0; i2 < number; i2++)
                    {
                        direction = (direction + 1) % 4;
                        //track.Add(curPos);
                    }
                }
            }
            return track;
        }
    }



    public override bool Act()
    {
        var size = Console.ReadLine().ParseList<int>();
        var pos = Console.ReadLine().ParseList<int>();
        Point p = new Point(pos[0], pos[1]);

        var commands = Console.ReadLine().Split(' ').ToList();

        var speed = Console.ReadLine().ParseAllTokens<double>();

        

        List<Alien> aliens = new List<Alien>();
        var alienn = Console.ReadLine().ParseAllTokens<int>();
        for (int i =0;i<alienn;i++)
        {

            var tiime = Console.ReadLine().ParseAllTokens<int>();
            var one = new Alien() { pos = p, direction = 0, speed = speed, StartTime = tiime };
            aliens.Add(one);
        }
        var positions = aliens[0].CalcTrack(commands);

        var q = Console.ReadLine().ParseAllTokens<int>();
        for (int i=0;i<q;i++)
        {
            var czas_id = Console.ReadLine().ParseAllTokens<int, int>();
            var czas = czas_id.Item1;
            var id = czas_id.Item2;

            var postime = czas - aliens[id].StartTime;
            var res = positions[Math.Min(positions.Count-1, (int)(postime * speed))];
            Console.Out.WriteLine(czas.ToString() + " " + id.ToString() + " " + ((int)(res.X)).ToString() + " " + ((int)(res.Y)).ToString());
        }


        //Console.Out.WriteLine(p.X.ToString() + " " + p.Y.ToString());
        return true;
    }
}
