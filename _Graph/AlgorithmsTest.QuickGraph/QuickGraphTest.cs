using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph;
using System.Linq;
using System.IO;
using QuickGraph.Algorithms.ShortestPath;
using QuickGraph.Algorithms;

namespace AlgorithmsTest.QuickGraph
{
    [TestClass]
    public class QuickGraphTest
    {
        class V
        {
            public string Name;

            public override string ToString()
            {
                return Name;
            }
        }

        class E : IEdge<V>
        {
            public int Cost;

            public V Source { get; set; }

            public V Target { get; set; }

            public override string ToString()
            {
                return string.Format("{0} -> {1}, cost = {2}", Source, Target, Cost);
            }
        }

        private static Random rand = new Random();

        [TestMethod]
        public void UsagePresentation()
        {
            int verticesCount = 10;
            int edgesCount = 40;
            var vertices = new V[verticesCount];

            for (int i = 0; i < verticesCount; i++)
                vertices[i] = new V() { Name = Path.GetRandomFileName().Substring(0, 5) };

            var graph = new AdjacencyGraph<V, E>();
            graph.AddVertexRange(vertices);
            graph.AddEdgeRange(Enumerable.Range(1, edgesCount)
                .Select(i => new E() { Cost = rand.Next(100), Source = vertices[rand.Next(verticesCount)], Target = vertices[rand.Next(verticesCount)] }));

            foreach (var v in graph.Vertices)
            {
                Console.WriteLine(v);
                foreach (var edge in graph.OutEdges(v))
                    Console.WriteLine(edge);
            }

            // Visualisation
            //var g = graph.ToIdentifiableGleeGraph(a => a.Name, null, null);
            //var viewer = new GViewer() { Graph = g };
            //viewer.Parent = this;
            //viewer.Dock = DockStyle.Fill;

            // ------ Dijkstra
            var dijkstra = new DijkstraShortestPathAlgorithm<V, E>(graph, e => e.Cost);
            dijkstra.Compute(vertices[0]);
            Console.WriteLine(string.Join(", ", dijkstra.Distances.Select(d => string.Format("{0}: {1}", d.Key, d.Value))));

            // ------ Maximum Flow
            // we need a graph, a source and a sink
            V source = vertices[0];
            V sink = vertices[9];

            // A function with maps an edge to its capacity
            Func<E, double> capacityFunc = (edge => edge.Cost);

            // A function which takes a vertex and returns the edge connecting to its predecessor in the flow network
            TryFunc<V, E> flowPredecessors;

            // A function used to create new edges during the execution of the algorithm.  These edges are removed before the computation returns
            EdgeFactory<V, E> edgeFactory = (s, t) => new E { Source = s, Target = t };

            // computing the maximum flow using Edmonds Karp.
            double flow = AlgorithmExtensions.MaximumFlowEdmondsKarp<V, E>(
                graph,
                capacityFunc,
                source, sink,
                out flowPredecessors,
                edgeFactory);
            Console.WriteLine(flow);


        }
    }
}
