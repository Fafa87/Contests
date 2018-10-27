using QuickGraph;
using QuickGraph.Algorithms.ShortestPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.QuickGraph.Map
{
    public abstract class Map2DGraph<T> : Map<T>
    {
        List<List<V>> vertices = new List<List<V>>();
        AdjacencyGraph<V, E> graph = new AdjacencyGraph<V, E>();
        DijkstraShortestPathAlgorithm<V, E> dijkstra;

        public Map2DGraph(int rows, int cols) : base(rows, cols)
        {
        }

        public virtual double Cost(GridPoint start, GridPoint step, T fieldType)
        {
            return 1;
        }


        public abstract bool IsEmptyField(T field);


        public V this[GridPoint point]
        {
            get
            {
                return vertices[point.Y][point.X];
            }
        }

        public void CreateVertices()
        {
            foreach (var row in this.WithIndex())
            {
                var verts = row.Item2.WithIndex().Select(p => new V() { Point = new GridPoint(p.Item1, row.Item1) }).ToList();
                vertices.Add(verts);
            }

            graph.AddVertexRange(vertices.SelectMany(p => p));
        }

        public void CreateEdges(GridPoint[] steps) // GraphUtils.STEPS8
        {
            for (int i = 0; i < this.Count; i++)
            {
                for (int i2 = 0; i2 < this[0].Count; i2++)
                {
                    GridPoint point = new GridPoint(i2, i);
                    foreach (var step in steps)
                    {
                        var newPoint = point + step;
                        if (0 <= newPoint.Y && newPoint.Y < this.Count && 
                            0 <= newPoint.X && newPoint.X < this[0].Count &&
                            IsEmptyField(base[newPoint.Y][newPoint.X]))
                        {
                            var Er = new E()
                            {
                                Cost = Cost(point, newPoint, base[newPoint]),
                                Source = vertices[point.Y][point.X],
                                Target = vertices[newPoint.Y][newPoint.X]
                            };
                            graph.AddEdge(Er);
                        }
                    }
                }
            }

            dijkstra = new DijkstraShortestPathAlgorithm<V, E>(graph, e => e.Cost);
        }

        /// <summary>
        /// Find next step in closest path. Yup it is thaaaat slow.
        /// </summary>
        public Tuple<GridPoint, double> GetClosestPath(GridPoint a, GridPoint b) 
        {
            dijkstra.Compute(this[b]);
            double abDist = dijkstra.Distances[this[a]];

            V nextGood = null;
            var distance = double.MaxValue;
            foreach (var edge in graph.OutEdges(this[a]))
            {
                var newDist = dijkstra.Distances[edge.Target]; // +edge.Cost;
                if (distance > newDist)
                {
                    distance = newDist;
                    nextGood = edge.Target;
                }
            }

            if (nextGood == null)
                return null;
            return Tuple.Create(nextGood.Point, abDist);
        }
    }

    public class V : IComparable
    {
        public GridPoint Point;

        public int CompareTo(object obj)
        {
            var p2 = (V)obj;
            var cx = Point.X.CompareTo(p2.Point.X);
            if (cx == 0)
                return Point.Y.CompareTo(p2.Point.Y);
            return cx;
        }

        public override string ToString()
        {
            return Point.ToString();
        }
    }

    public class E : IEdge<V>
    {
        public double Cost = 1;
        public V Source { get; set; }
        public V Target { get; set; }

        public override string ToString()
        {
            return string.Format("{0} -> {1}, cost = {2}", Source, Target, Cost);
        }
    }
}
