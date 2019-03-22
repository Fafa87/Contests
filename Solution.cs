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
    
    public override void GetData()
    {
    }

    List<Alien> current = new List<Alien>();

    [Serializable]
    public class Alien
    {
        public int Id;
        public int direction = 0;
        public double speed = 1;
        public int StartTime = 0;

        public double HP = 1;
        public double Loot;

        public bool IsEnd(int czas)
        {
            var postime = czas - this.StartTime;
            var pointintime = (int)(postime * speed);
            if (pointintime >= track.Count)
                return true;
            return false;
        }

        public bool IsStarted(int czas)
        {
            return this.StartTime <= czas;
        }

        public Point Position(int czas)
        {
            var postime = czas - this.StartTime;
            var pointintime = postime * speed;
            if (pointintime >= track.Count)
                return null;
            /*
                
            var inta = (int)pointintime;
            var reszta = pointintime - inta;
            var lewy = track[(int)pointintime];
            var prawy = track[(int)pointintime + 1];

            var res = new Point((lewy.X * (1 - reszta) + prawy.X * reszta), lewy.Y * (1 - reszta) + prawy.Y * reszta);
            */
            var res = track[Math.Min(track.Count - 1, (int)(postime * speed))];
            return res;
        }

        public static List<Point> track = new List<Point>();

        public static List<Point> CalcTrack(List<string> commands, Point pos)
        {
            track.Clear();
            track.Add(pos);
            int direction = 0;
            for (int i = 0; i < commands.Count(); i += 2)
            {
                if (commands[i][0] == 'F')
                {
                    var number = commands[i + 1].ParseAllTokens<int>();
                    for (int i2 = 0; i2 < number; i2++)
                    {
                        pos = All4[direction].Move(pos, 1);
                        track.Add(pos);
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

    public enum State
    {
        Seeking,
        Locked
    }

    public class Tower
    {
        public double Range;
        public double Damage;

        public Point pos;

        public State state = State.Seeking;
        public Alien lockedOn;
        public int Shoot;

        public bool InRange(Point alienpos)
        {
            return alienpos.Distance(pos) <= Range;
        }


    }

    public Alien FindClosestAlien(Tower tower, int tick)
    {
        var best = current
            .Where(p=>p.IsStarted(tick))
            .Select(a => (dist: a.Position(tick).Distance(tower.pos), a.Id, a))
            .OrderBy(a => a.dist)
            .ThenBy(a=>a.Item2)
            .FirstOrDefault(a => a.dist <= tower.Range);

        return best.Item3;
    }

    
    public override bool Act()
    {
        var size = Console.ReadLine().ParseList<int>();
        var pos = Console.ReadLine().ParseList<int>();
        Point p = new Point(pos[0], pos[1]);

        var commands = Console.ReadLine().Split(' ').ToList();

        var healthspeed = Console.ReadLine().ParseList<double>();
        var health = healthspeed[0];
        var speed = healthspeed[1];
        var loot = healthspeed[2];


        List<Alien> aliens = new List<Alien>();
        var alienn = Console.ReadLine().ParseAllTokens<int>();
        for (int i =0;i<alienn;i++)
        {

            var tiime = Console.ReadLine().ParseAllTokens<int>();
            var one = new Alien() { direction = 0, speed = speed, StartTime = tiime, Id = i, HP = health, Loot = loot };
            aliens.Add(one);
        }

        current = aliens.DeepClone();

        Alien.CalcTrack(commands, p);

        var damagerange = Console.ReadLine().ParseList<double>();
        var damage = damagerange[0];
        var range = damagerange[1];
        var cost = damagerange[2];
        var goldorg = Console.ReadLine().ParseAllTokens<double>();
        var gold = goldorg;
        var towers = new List<Tower>();

        Dictionary<Point, int> close = new Dictionary<Point, int>();
        var trackH = Alien.track.ToHashSet();
        foreach (var s in Alien.track)
        {
            foreach(var move in Moves.All8)
            {
                var cand = move.Move(s);
                if (trackH.Contains(cand) == false)
                {
                    close.Increment(cand, 1);
                }

                foreach (var move2 in Moves.All8)
                {
                    var cand2 = move2.Move(cand);
                    if (trackH.Contains(cand2) == false)
                    {
                        close.Increment(cand2, 1);
                    }
                }
            }
        }

        while (true)
        {
            towers.Clear();
            foreach (var a in aliens)
                a.HP = health;
            foreach (var a in towers)
            {
                a.state = State.Seeking;
                a.lockedOn = null;
            }
            current = aliens.DeepClone();
            gold = goldorg;

            var goodTowers = close.OrderByDescending(a => a.Value).Take(500).Randomize().ToList();
            int usedTowers = 0;
            foreach (var t in goodTowers.Take(Math.Min(500, (int)(gold / cost))))
            {
                towers.Add(new Tower() { pos = new Point(t.Key.X, t.Key.Y), Damage = damage, Range = range, Shoot = 0 });
                usedTowers++;
                gold -= cost;
            }
            goodTowers = goodTowers.Skip(usedTowers).ToList();

            //Console.Out.WriteLine(towers.Count.ToString() + " " + (int)(gold / cost));
            /*
            var towern = Console.ReadLine().ParseAllTokens<int>();
            for (int i = 0; i < towern; i++)
            {
                var x_y = Console.ReadLine().ParseAllTokens<int, int>();
                towers.Add(new Tower() { pos = new Point(x_y.Item1, x_y.Item2), Damage = damage, Range = range});
            }*/

            var curtime = 0;
            while (current.Any())
            {
                if (current.Any(a => a.IsEnd(curtime)))
                {
                    //Console.Out.WriteLine(curtime);
                    //Console.Out.WriteLine("LOSS");
                    break;
                }
                else
                {
                    if (curtime > 0)
                    {
                        foreach (var tower in towers)
                        {
                            if (tower.state == State.Locked)
                            {
                                if (!tower.InRange(tower.lockedOn.Position(curtime)) || tower.lockedOn.HP <= 0)
                                {
                                    tower.state = State.Seeking;
                                    tower.lockedOn = null;
                                }
                            }

                            if (tower.state == State.Seeking)
                            {
                                var closest = FindClosestAlien(tower, curtime);
                                if (closest != null)
                                {
                                    tower.state = State.Locked;
                                    tower.lockedOn = closest;
                                }
                            }
                        }

                        foreach (var tower in towers)
                        {
                            if (tower.state == State.Locked)
                            {
                                tower.lockedOn.HP -= tower.Damage;
                                if (tower.lockedOn.HP <= 0)
                                {
                                    if (current.Remove(tower.lockedOn))
                                        gold += tower.lockedOn.Loot;
                                }
                            }
                        }

                        usedTowers = 0;
                        while(gold >= cost && towers.Count < 500)
                        {
                            var towernew = goodTowers[usedTowers];
                            towers.Add(new Tower() { pos = new Point(towernew.Key.X, towernew.Key.Y), Damage = damage, Range = range, Shoot = curtime });
                            usedTowers++;
                            gold -= cost;
                        }
                        goodTowers = goodTowers.Skip(usedTowers).ToList();
                        usedTowers = 0;
                    }
                }


                curtime++;
            }
            if (current.Any() == false)
                break;


        }

        foreach(var t in towers)
        {
            Console.Out.WriteLine(t.pos.ToString() + " " + t.Shoot);
        }


        //Console.Out.WriteLine(curtime-1);
        //Console.Out.WriteLine("WIN");



        return true;
    }
}
