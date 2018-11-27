using System.Collections.Generic;

using Stater.Constants.Stater;

namespace Stater.Graph
{
    
public class ClassGraph {
    private Dictionary<string, Node> nodes { get; } = new Dictionary<string, Node>();

    // Indexing a class graph with square brackets (like an array)
    // will return the node with the given key
    public Node this[string ID]
    {
        get { return nodes[ID]; }
    }

    public void AddNode(Node node) {
        nodes.Add(node.ID, node);
    }

    public void RemoveNode(string ID)
    {
        nodes.Remove(ID);
    }

    public ICollection<Node> GetNodes()
    {
        return nodes.Values;
    }

    public void AddEdge(Edge edge) {
        if(nodes[edge.To].ContainsEdge(edge))
        {
            nodes[edge.To].GetEdge(edge.From).MergeData(edge);
            return;
        }
        nodes[edge.To].AddEdge(edge);

        // If the input is from external, there's nothing to attach the other end to
        if (edge.From == StaterConstants.EXTERNAL_INPUT)
        {
            return;
        }

        nodes[edge.From].AddEdge(edge);        
    }

    public void RemoveEdge(Edge edge) {
        // Remove edge from its node
        nodes[edge.From].RemoveEdge(edge);
        nodes[edge.To].RemoveEdge(edge);
    }

    public ICollection<Edge> GetIncoming(string ID) {
        return nodes[ID].GetIncoming();
    }

    public ICollection<Edge> GetOutgoing(string ID) {
        return nodes[ID].GetOutgoing();
    }

    public bool ContainsNode(string ID){
        return nodes.ContainsKey(ID);
    }
    

    public override string ToString()
    {
        string output = "";

        foreach (var node in GetNodes())
        {
            output += "Node:\n" + node.ToString();
            output += "\n";
        }

        return output;
    }

}

}