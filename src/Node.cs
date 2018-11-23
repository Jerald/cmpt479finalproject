using System.Collections.Generic;

namespace Stater
{

public class Node {

    private Dictionary<string, Edge> incoming = new Dictionary<string, Edge>();
    private Dictionary<string, Edge> outgoing = new Dictionary<string, Edge>();
    public readonly string ID;

    public Node(string ID) {
        this.ID = ID;
    }

    public Dictionary<string, Edge>.ValueCollection GetIncoming()
    {
        return this.incoming.Values;
    }

    public Dictionary<string, Edge>.ValueCollection GetOutgoing()
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

   
}

}