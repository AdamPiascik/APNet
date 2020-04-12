using System.Collections.Generic;

namespace APNet.Core.Graphs
{
    public class Graph<T>
    {
        public Dictionary<T, GraphNode<T>> Nodes { get; set; }
            = new Dictionary<T, GraphNode<T>>();

        public void AddNode(T nodeId)
            => Nodes.Add(nodeId, new GraphNode<T>(nodeId));

        public void RemoveNode(T nodeId)
        {
            Nodes.Remove(nodeId);

            foreach (var item in Nodes.Values)
            {
                item.RemoveNeighbour(nodeId);
            }
        }

        public void AddUndirectedEdge(
            T nodeId1,
            T nodeId2,
            uint cost
        )
        {
            var node1 = new GraphNode<T>(nodeId1);
            var node2 = new GraphNode<T>(nodeId2);

            Nodes.TryAdd(nodeId1, node1);
            Nodes.TryAdd(nodeId2, node2);

            Nodes[nodeId1].AddNeighbour(node2, cost);
            Nodes[nodeId2].AddNeighbour(node1, cost);
        }
    }
}
