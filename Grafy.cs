namespace Grafy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var graph = new Graph();

            var node0 = new NodeG(0);
            var node1 = new NodeG(1);
            var node2 = new NodeG(2);
            var node3 = new NodeG(3);
            var node4 = new NodeG(4);
            var node5 = new NodeG(5);
            var node6 = new NodeG(6);
            var node7 = new NodeG(7);

            var nodes = new List<NodeG>
            {
                node0, 
                node1, 
                node2, 
                node3,
                node4,
                node5, 
                node6, 
                node7
            };

            var edges = new List<Edge>
            {
                new Edge(node0, node1, 5),
                new Edge(node0, node3, 9),
                new Edge(node0, node6, 3),

                new Edge(node1, node2, 9),
                new Edge(node1, node4, 8),
                new Edge(node1, node5, 6),
                new Edge(node1, node7, 7),
               
                new Edge(node2, node3, 9),
                new Edge(node2, node4, 4),
                new Edge(node2, node6, 4),
                new Edge(node2, node7, 3),

                new Edge(node3, node6, 8),

                new Edge(node4, node5, 2),
                new Edge(node4, node6, 1),

                new Edge(node5, node6, 6),

                new Edge(node6, node7, 9),
            };


            graph.nodes = nodes;
            graph.edges = edges;

            MDR(graph);
        }

        public static void MDR(Graph graph)
        {
            List<Edge> T;
            List<Edge> L = graph.edges.OrderBy(x => x.weight).ToList();

            foreach (Edge edge in L)
            {
                
            }
        }
    }

    class NodeG
    {
        public int data;
        public NodeG(int data)
        {
            this.data = data;
        }
    }

    class Edge
    {
        public NodeG start;
        public NodeG end;
        public int weight;

        public Edge(NodeG start, NodeG end, int weight)
        {
            this.start = start;
            this.end = end;
            this.weight = weight;
        }
    }

    class Graph
    {
        public List<NodeG> nodes;
        public List<Edge> edges;

        public Graph()
        {
            this.nodes = new List<NodeG> ();
            this.edges = new List<Edge> ();
        }

        public void AddEdge(Edge edge)
        {
            this.edges.Add(edge);
        }
    }
}
