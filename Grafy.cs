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
            List<Edge> L = graph.edges.OrderBy(x => x.weight).ToList();
            List<Graph> graphs = new List<Graph>();

            foreach (Edge edge in L)
            {
                if (graph.NewNodesCount(edge) == 1 || graph.NewNodesCount(edge) == 2)
                {
                    graph.AddEdge(edge);
                }
                else
                {
                    graphs.Add(new Graph(edge));
                }
            }

            foreach(Graph g in graphs)
            {
                graph.Join(g);
            }
        }
    }

    class Graph
    {
        public List<NodeG> nodes;
        public List<Edge> edges;

        public Graph()
        {
            this.nodes = new List<NodeG>();
            this.edges = new List<Edge>();
        }

        public Graph(Edge edge)
        {
            this.nodes = new List<NodeG>();
            AddEdge(edge);
        }

        public void AddEdge(Edge edge)
        {
            if (!this.edges.Contains(edge))
            {
                this.edges.Add(edge);
            }
            if (!this.nodes.Contains(edge.start))
            {
                this.nodes.Add(edge.start);
            }
            if (!this.nodes.Contains(edge.end))
            {
                this.nodes.Add(edge.end);
            }
        }

        public int NewNodesCount(Edge edge)
        {
            int count = 0;
            if (!this.nodes.Contains(edge.start))
            {
                count++;
            }
            if (!this.nodes.Contains(edge.end))
            {
                count++;
            }

            return count;
        }

        public void Join(Graph other)
        {
            foreach (Edge edge in other.edges)
            {
                this.AddEdge(edge);
            }
        }

        public List<Element> Dijkstra(NodeG start)
        {
            // Przygotuj tabelke
            var table = new List<Element>();
            foreach (NodeG node in this.nodes)
            {
                // Ustaw startowy element na 0
                if (node == start)
                {
                    table.Add(new Element(start, 0, null));
                }
                var element = new Element(node);
                table.Add(element);
            }

            // Lista ze sprawodznymi węzłami
            var S = new List<NodeG>();
            S.Add(start);
            
            foreach (Element element in table)
            {
                // bierzemy pierwszego najmniejszego z tabelki
                Element candidate = table.Where(e => !S.Contains(e.node))
                                         .OrderBy(e => e.weight)
                                         .First();
                S.Add(candidate.node);

                // Szukamy sąsiadów
                // W przypadku zera -> zaczynają się na 0, ale kończą na innym elemencie
                var neighbours = this.edges.Where(e => e.start == candidate.node && !S.Contains(e.end)).ToList();

                // Iterujemy po sąsiadach
                // Np 0 - 1, waga 3
                foreach (var neighbour in neighbours)
                {
                    var nextElement = table.Find(e => e.node == neighbour.end);
                    nextElement.weight = candidate.weight + neighbour.weight;
                }
            }

            return table;
        }
    }

    class Element
    {
        public NodeG node;
        public int weight;
        public NodeG? before;

        public Element(NodeG node)
        {
            this.node = node;
            this.weight = int.MaxValue;
            this.before = null;
        }

        public Element(NodeG node, int weight, NodeG before)
        {
            this.node = node;
            this.weight = weight;
            this.before = before;
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
}
