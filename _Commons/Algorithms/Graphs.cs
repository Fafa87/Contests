using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    // TODO propose a nice and clean and generic way to create graph for graph algorithms (composition, interface whatever)
    // below code is not good nor finished
    // TODO generyczny BFS, DFS, DIJKSTRA
    // Oparty na funkcjach? (albo implementacji interfejsu)
    // GetNeighbours(node), IsVisited(node), SetVisited(node), SetDistance(node, val) 

    public class Edge<TData> // bidirectional edge
    {
        public Node<TData> A;
        public Node<TData> B;
        public int Cost;

        public Node<TData> End(Node<TData> from)
        {
            if (from == A)
                return B;
            else
                return A;
        }

        public bool Contains(Node<TData> town)
        {
            return A == town || B == town;
        }
    }

    public class Node<TData> : object
    {
        public int Id;
        public TData Data;
        public List<Edge<TData>> Edges;
    }

    public class Graph<TData>
    {
        public List<Edge<TData>> Edges;

        public Edge<TData> FindEdgeBetween(Node<TData> a, Node<TData> b)
        {
            return Edges.FirstOrDefault(p => p.A == a && p.B == b) ?? Edges.FirstOrDefault(p => p.A == b && p.B == a);
        }

        public void PushEdgesToNodes(bool bidirectional)
        {
            foreach(var edge in Edges)
            {
                edge.A.Edges.Add(edge);
                if(bidirectional)
                    edge.B.Edges.Add(edge);
            }
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
