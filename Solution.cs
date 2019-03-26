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

    HashSet<Alien> current = new HashSet<Alien>();

    [Serializable]
    public class Alien
    {

        public CType type;
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

    public class TowerType
    {
        public double Damage;
        public double Range;
        public double Cost;
        public string Name;

    }

    [Serializable]
    public class CType
    {
        public double health;
        public double speed;
        public double slow;
        public double aeoresist;
        public double loot;
        public string Name;

    }

    public Alien FindClosestAlien(Tower tower, int tick)
    {
        var best = current
            .Where(p=>p.IsStarted(tick))
            .Select(a => (dist: a.Position(tick).Distance(tower.pos), a.Id, a))
            .Where(a => a.dist <= tower.Range)
            .OrderBy(a => a.dist)
            .ThenBy(a=>a.Item2)
            .FirstOrDefault();

        return best.Item3;
    }


    public override bool Act()
    {
        var size = Console.ReadLine().ParseList<int>();
        var pos = Console.ReadLine().ParseList<int>();
        Point p = new Point(pos[0], pos[1]);

        var commands = Console.ReadLine().Split(' ').ToList();



        var typen = Console.ReadLine().ParseAllTokens<int>();
        List<CType> typenAlien = new List<CType>();
        for (int i = 0; i < typen; i++)
        {
            var rest = Console.ReadLine().ParseToken(out string name);//<double>();
            var healthspeed = rest.ParseList<double>();
            var health = healthspeed[0];
            var speed = healthspeed[1];
            var slow = healthspeed[2];
            var aeoresist = healthspeed[3];
            var loot = healthspeed[4];
            typenAlien.Add(new CType() { aeoresist = aeoresist, health = health, Name = name, loot = loot, slow = slow, speed = speed });
        }

        List<Alien> aliens = new List<Alien>();
        var alienn = Console.ReadLine().ParseAllTokens<int>();
        for (int i = 0; i < alienn; i++)
        {
            var rest = Console.ReadLine().ParseToken(out string name);
            var tiime = rest.ParseAllTokens<int>();
            var type = typenAlien.First(a => a.Name == name);
            var one = new Alien() { direction = 0, speed = type.speed, StartTime = tiime, Id = i, HP = type.health, Loot = type.loot, type = type };
            aliens.Add(one);
        }

        var towertypen = Console.ReadLine().ParseAllTokens<int>();
        var towertypes = new List<TowerType>();
        for (int i = 0; i < towertypen; i++)
        {
            var rest = Console.ReadLine().ParseToken(out string name);//<double>();
            var data = rest.ParseList<double>();
            var damage = data[0];
            var rangea = data[1];
            var slow = data[2];
            var aeo = data[3];
            var cost = data[4];
            towertypes.Add(new TowerType() { Name = name,  Cost = cost, Damage = damage, Range = rangea });
        }

        var chosenType = towertypes.FirstOrDefault(a => a.Name == "LASER");

        current = aliens.ToList().DeepClone().ToHashSet();

        Alien.CalcTrack(commands, p);

        
        var goldorg = Console.ReadLine().ParseAllTokens<double>();
        var gold = goldorg;
        var towers = new List<Tower>();

        Dictionary<Point, int> close = new Dictionary<Point, int>();
        var trackH = Alien.track.ToHashSet();

        var range = chosenType.Range;// towertypes.Max(t => t.Range);
        foreach (var s in Alien.track)
        {
            for(int i=-(int)range;i<range;i++)
            for(int i2=-(int)range;i2<range;i2++)
                {
                    Point point = new Point(s.X + i, s.Y + i2);
                    if (point.Distance(s) <= range)
                    {
                        if (trackH.Contains(point) == false)
                        {
                            close.Increment(point, 1);
                        }

                    }

                    /*
                }
                    foreach (var move in Moves.All8)
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
                }*/
            }
        }

        Console.Error.WriteLine("Game ready");

        while (true)
        {
            towers.Clear();
            foreach (var a in aliens)
                a.HP = a.type.health;
            foreach (var a in towers)
            {
                a.state = State.Seeking;
                a.lockedOn = null;
            }
            current = aliens.ToList().DeepClone().ToHashSet();
            gold = goldorg;

            var goodTowers = close.OrderByDescending(a => a.Value).ToList();
            //goodTowers = goodTowers.Take(100).Randomize().Concat(
            //    goodTowers.Skip(100).Take(100).Randomize().Concat(
            //        goodTowers.Skip(200).Take(100).Randomize().Concat(
            //            goodTowers.Skip(300).Take(100).Randomize().Concat(
            //                goodTowers.Skip(400).Take(100).Randomize())))).ToList();
            int usedTowers = 0;
            foreach (var t in goodTowers.Take(Math.Min(500, (int)(gold / chosenType.Cost))))
            {
                towers.Add(new Tower() { pos = new Point(t.Key.X, t.Key.Y), Damage = chosenType.Damage, Range = range, Shoot = 0 });
                usedTowers++;
                gold -= chosenType.Cost;
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
                    Console.Error.Write("LOSS "); Console.Error.WriteLine(current.Count());
                    break;
                }
                else
                {
                    if (curtime % 100 == 0)
                        Console.Error.Write(curtime + " ");
                    if (curtime > 0)
                    {
                        foreach (var tower in towers)
                        {
                            if (tower.state == State.Locked)
                            {
                                if (tower.lockedOn.HP <= 0 || !tower.InRange(tower.lockedOn.Position(curtime)))
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
                        while(gold >= chosenType.Cost && towers.Count < 500)
                        {
                            var towernew = goodTowers[usedTowers];
                            towers.Add(new Tower() { pos = new Point(towernew.Key.X, towernew.Key.Y),
                                Damage = chosenType.Damage, Range = range, Shoot = curtime });
                            usedTowers++;
                            gold -= chosenType.Cost;
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
