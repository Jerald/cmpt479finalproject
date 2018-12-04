using System.Collections.Generic;

using Stater.Constants.Stater;

namespace Stater.Graph
{
    
public class ClassGraph {
    private readonly Dictionary<string, Node> nodes = new Dictionary<string, Node>();

    // Indexing a class graph with square brackets (like an array)
    // will return the node with the given key
    public Node this[string ID]
    {
        get { return nodes[ID]; }
    }

    public void AddNode(Node node)
    {
        // TODO: check if the provided node's incoming or outgoing dictionary has any nodes. If so, merge them
        if (nodes.ContainsKey(node.ID) == true)
        {
            return;
        }
        
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

    public void AddEdge(Edge edge)
    {
        KeyValuePair<string, string> edgeToFromPair = edge.GetToFromPair();

        // Case where destination node doesn't exist
        if (nodes.ContainsKey(edge.To) == false)
        {
            this.AddNode(new Node(edge.To));
        }
        else // Destination node _does_ exist
        {
            // Case where we want to merge the edge annotation data
            if (nodes[edge.To].ContainsEdge(edge))
            {
                nodes[edge.To].GetEdge(edge.From).MergeData(edge);
                return;
            }
        }

        nodes[edge.To].AddEdge(edge);

        // Case where the source is an external input
        if (edge.From == StaterConstants.EXTERNAL_INPUT)
        {
            return;
        } // Case where source node doesn't exist (and it's not an external input)
        else if (nodes.ContainsKey(edge.From) == false)
        {
            this.AddNode(new Node(edge.To));
        }
        else // Source node _does_ exist and isn't an external input
        {
            // Case where we want to merge the edge annotation data
            if (nodes[edge.From].ContainsEdge(edge))
            {
                nodes[edge.From].GetEdge(edge.To).MergeData(edge);
                return;
            }
        }

        nodes[edge.From].AddEdge(edge);  

    }

    public void RemoveEdge(Edge edge)
    {
        // TODO: make this safe if edge is orphaned, or doesn't exist

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