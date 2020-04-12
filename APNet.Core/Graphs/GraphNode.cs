using System.Collections.Generic;
using System.Linq;

namespace APNet.Core.Graphs
{
    public class GraphNode<T>
    {
        public T NodeID { get; set; }

        public Dictionary<GraphNode<T>, uint> Neighbours { get; private set; }
            = new Dictionary<GraphNode<T>, uint>();

        public GraphNode(T nodeId)
            => NodeID = nodeId;

        public void AddNeighbour(GraphNode<T> node, uint cost)
        {
            if (!IsNeighbour(node))
                Neighbours.Add(node, cost);
        }

        public void RemoveNeighbour(T nodeId)
        {
            Neighbours =
                Neighbours.Where(x => !x.Key.NodeID.Equals(nodeId))
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public void RemoveNeighbour(GraphNode<T> node)
            => Neighbours.Remove(node);

        private bool IsNeighbour(GraphNode<T> node)
            => Neighbours.ContainsKey(node);
    }
}
