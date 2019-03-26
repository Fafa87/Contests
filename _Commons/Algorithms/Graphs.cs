using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    // There is QuickGraph library which should be used if possible.
    // In this file we will create a simpler version for contest where it is forbidden to use external dlls.

    // TODO propose a nice and clean and generic way to create graph for graph algorithms (composition, interface whatever)
    // below code is not good nor finished
    // TODO generyczny BFS, DFS, DIJKSTRA

    
    public class Edge<TData>
    {
        public TData Target;
        public double distance;
    }

    public class Graph<TData>
    {
        public Func<TData, IEnumerable<Edge<TData>>> GetNeighbours;

    }

    public class UnionFind<T>
        where T : class
    {
        Dictionary<T, T> Parent = new Dictionary<T,T>();

        public T Find(T elem)
        {
            if (Parent.ContainsKey(elem) == false)
                Parent[elem] = elem;

            if (Parent[Parent[elem]] != Parent[elem]) Parent[elem] = Find(Parent[elem]);
            return Parent[elem];
        }

        public void Union(T elem1, T elem2)
        {
            Parent[Find(elem1)] = Find(elem2);
        }

        public List<T> Components()
        {
            return Parent.Where(p => p.Key == p.Value).Select(p => p.Key).ToList();
        }

    }

    public static class GraphUtils
    {
        public static readonly GridPoint[] STEPS4 = new GridPoint[] { new GridPoint(1, 0), new GridPoint(-1, 0), new GridPoint(0, 1), new GridPoint(0, -1) };
        public static readonly GridPoint[] STEPS8 = new GridPoint[] { new GridPoint(1, 1), new GridPoint(1, -1), new GridPoint(-1, 1), new GridPoint(-1, -1), new GridPoint(1, 0), new GridPoint(-1, 0), new GridPoint(0, 1), new GridPoint(0, -1) };

        public const int INF = 1000000000;
        public static int[,] Warshall(int n, List<Tuple<int,int,int>> edges)
        {
            // TODO dodaj zwracanie najkrótszych ścieżek i testy
            var res = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int i2 = 0; i2 < n; i2++)
                {
                    if (i == i2)
                        res[i, i2] = 0;
                    else
                    {
                        res[i, i2] = INF;
                    }
                }
            }

            foreach (var ed in edges)
            {
                res[ed.Item1, ed.Item2] = ed.Item3;
                res[ed.Item2, ed.Item1] = ed.Item3;
            }

            for (int k = 0; k < n; k++)
            {
                for (int a = 0; a < n; a++)
                {
                    for (int b = 0; b < n; b++)
                    {
                        res[a, b] = Math.Min(res[a, b], res[a, k] + res[k, b]);
                    }
                }
            }

            return res;
        }
        // Prosty DFS 

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

        //6. Prosty BFS - lekki, bez potrzeby używania QuickGraph:
        //    private void UpdateStability(GameState state, Map<bool> edges, Map<int> stability, GridPoint start, int startStab)
        //    {
        //        HashSet<GridPoint> visited = new HashSet<GridPoint>();
        //        Queue<Tuple<int, GridPoint>> queue = new Queue<Tuple<int, GridPoint>>();
        //        queue.Enqueue(Tuple.Create(startStab, start));
        //        visited.Add(start);

        //        while(queue.Any())
        //        {
        //            var d = queue.Dequeue();
        //            var point = d.Item2;
        //            var current = d.Item1;

        //            if (current > state.Stability)
        //                break;

        //            visited.Add(point);

        //            foreach(var m in Moves.All4)
        //            {
        //                var newPos = m.Move(point);
        //                if (edges.IsInside(newPos) && visited.Contains(newPos) == false)
        //                {
        //                    if (stability[newPos] > current + 1)
        //                    {
        //                        queue.Enqueue(Tuple.Create(current + 1, newPos));
        //                        stability[newPos] = current + 1;
        //                    }
        //                }
        //            }
        //        }
        //    }


        //private bool Visited(GridPoint gp)
        //{
        //    return kolors[gp.Y, gp.X] == kolor;
        //}


        //public double FindChange(GridPoint start, Action<int, int, double> add = null)
        //{
        //    kolor++;
        //    bfs = new Queue<GridPoint>();
        //    bfs.Enqueue(start);
        //    double zysk = 0;
        //    while (bfs.Any())
        //    {
        //        var point = bfs.Dequeue();
        //        if (!Visited(point) && point.ManhattanDistance(start) < this.Size / oczekiwani * 3)
        //        {
        //            zysk += FindChange(point.Y, point.X, start);
        //            if (add != null)
        //                add(point.Y, point.X, GetValue(point.X, point.Y, start.ManhattanDistance(point.Y, point.X)));
        //        }
        //    }

        //    return zysk;
        //}

        //Queue<GridPoint> bfs;

        //public double FindChange(int y, int x, GridPoint start)
        //{
        //    double zysk = 0;
        //    kolors[y, x] = kolor;
        //    foreach (var step in steps)
        //    {
        //        var newY = step.Y + y;
        //        var newX = step.X + x;
        //        if (newX >= 0 && newX < Bounds.X && newY >= 0 && newY < Bounds.Y)
        //        {
        //            decimal newValue = (decimal)GetValue(newX, newY, start.ManhattanDistance(newY, newX));
        //            var change = newValue - (decimal)Costs[newY, newX];
        //            if (change < 0 && kolors[newY, newX] != kolor)
        //            {
        //                zysk += (double)change;
        //                bfs.Enqueue(new GridPoint(newX, newY));
        //                //zysk += FindChange(newY, newX, start);//, add);
        //                //if (add != null)
        //                //    add(newY, newX, (double)newValue);
        //            }
        //        }
        //    }
        //    return zysk;
        //}

    }
}
