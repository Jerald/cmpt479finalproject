using System.Collections.Generic;
using System.Diagnostics;

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

    public void AddEdge(Edge edge)
    {
        if (edge.To == ID)
        {
            incoming.Add(edge.From, edge);
        }
        else if (edge.From == ID)
        {
            outgoing.Add(edge.To, edge);
        }
        else
        {
            throw new System.ArgumentException("Provided edge's To and From fields don't match node!");
        }
    }

    public void RemoveEdge(Edge edge)
    {
        if (edge.To == ID)
        {
            incoming.Remove(edge.From);
        }
        else if (edge.From == ID)
        {
            outgoing.Remove(edge.To);
        }
        else
        {
            throw new System.ArgumentException("Provided edge's To and From fields don't match node!");
        }
    }

    public bool ContainsEdge(Edge edge)
    {
        if (edge.To == ID)
        {
            return incoming.ContainsKey(edge.From);
        }
        else if (edge.From == ID)
        {
            return outgoing.ContainsKey(edge.To);
        }
        else
        {
            return false;
        }
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