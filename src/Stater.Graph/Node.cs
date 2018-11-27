using System.Collections.Generic;

using Stater.Utils;

namespace Stater.Graph
{

public class Node {

    public readonly string ID;

    private Dictionary<string, Edge> incoming = new Dictionary<string, Edge>();
    private Dictionary<string, Edge> outgoing = new Dictionary<string, Edge>();

    public Node(string ID) {
        this.ID = ID;
    }

    public ICollection<Edge> GetIncoming()
    {
        return this.incoming.Values;
    }

    public ICollection<Edge> GetOutgoing()
    {
        return this.outgoing.Values;
    }

    public void AddIncoming(Edge edge)
    {
        this.incoming.Add(edge.From, edge);
    }

    public void RemoveIncoming(Edge edge)
    {
        this.incoming.Remove(edge.From);
    }

    public void AddOutgoing(Edge edge)
    {
        this.outgoing.Add(edge.To, edge);
    }

    public void RemoveOutgoing(Edge edge)
    {
        this.outgoing.Remove(edge.To);
    }


    public override string ToString()
    {
        string output = "{\n" + t.tab(1) + "ID: " + ID + ",\n" + t.tab(1) + "incoming: {\n";

        foreach (var edgePair in incoming)
        {
            output += t.tab(2) + edgePair.Value.ToString() + ",\n";
        }
        
        output += t.tab(1) + "},\n" + t.tab(1) + "outgoing: {\n";

        foreach (var edgePair in outgoing)
        {
            output += t.tab(2) + edgePair.Value.ToString() + ",\n";
        }

        output += t.tab(1) + "}\n}";

        return output;
    }
}

}