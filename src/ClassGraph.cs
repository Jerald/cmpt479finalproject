using System.Collections.Generic;

namespace Stater
{
    
public class ClassGraph {
    private Dictionary<string, Node> nodes { get; } = new Dictionary<string, Node>();

    public void AddNode(Node node) {
        nodes.Add(node.ID, node);
    }

    public void RemoveNode(string ID)
    {
        nodes.Remove(ID);
    }

    public void AddEdge(Edge edge) {
        // Attach edge to its nodes
        nodes[edge.From].AddOutgoing(edge);
        nodes[edge.To].AddIncoming(edge);
        
    }

    public void RemoveEdge(Edge edge) {
        // Remove edge from its node
        nodes[edge.From].RemoveOutgoing(edge);
        nodes[edge.To].RemoveIncoming(edge);
    }

    public Dictionary<string, Edge>.ValueCollection GetIncoming(string ID) {
        return nodes[ID].GetIncoming();
    }

    public Dictionary<string, Edge>.ValueCollection GetOutgoing(string ID) {
        return nodes[ID].GetOutgoing();
    }

}

}