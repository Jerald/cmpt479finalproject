using System.Collections.Generic;

using Stater.Constants.Stater;

namespace Stater.Graph
{
    
public class ClassGraph {
    private readonly Dictionary<string, Node> nodes = new Dictionary<string, Node>();

    private readonly Dictionary<string, Edge> orphanedIncomingEdges = new Dictionary<string, Edge>();
    private readonly Dictionary<string, Edge> orphanedOutgoingEdges = new Dictionary<string, Edge>();

    // Indexing a class graph with square brackets (like an array)
    // will return the node with the given key
    public Node this[string ID]
    {
        get { return nodes[ID]; }
    }

    public void AddNode(Node node)
    {
        // TODO: check if node already exist and do something safe then
        
        nodes.Add(node.ID, node);

        // Case if there is an orphaned incoming edge
        if (orphanedIncomingEdges.ContainsKey(node.ID))
        {
            var orphanedEdge = orphanedIncomingEdges[node.ID];
            orphanedIncomingEdges.Remove(node.ID);

            this.AddEdge(orphanedEdge);
        }

        // Case if there is an orphaned outgoing edge
        if (orphanedOutgoingEdges.ContainsKey(node.ID))
        {
            var orphanedEdge = orphanedOutgoingEdges[node.ID];
            orphanedOutgoingEdges.Remove(node.ID);

            this.AddEdge(orphanedEdge);
        }
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
        // Case where destination node doesn't exist
        if (nodes.ContainsKey(edge.To) == false)
        {
            // TODO: _properly_ handle orphaned edges with duplicate key, including if exact edge already exists
            if (orphanedIncomingEdges.ContainsKey(edge.To) == false)
            {
                orphanedIncomingEdges.Add(edge.To, edge);
            }
        }
        else // Destination node _does_ exist
        {
            /* 
            * WARNING: this case may have flawed logic.
            *
            * The issue may be that by returning early there isn't a chance for it to attach to the source node.
            *
            * It _should_ be fine, since any orphaned edges would've attached when the node was added,
            * meaning if one side is aware of the edge the other would as well.
            */

            // Case where we want to merge the edge annotation data
            if (nodes[edge.To].ContainsEdge(edge))
            {
                nodes[edge.To].GetEdge(edge.From).MergeData(edge);
                return;
            }

            nodes[edge.To].AddEdge(edge);
        }

        // Case where the source is an external input
        if (edge.From == StaterConstants.EXTERNAL_INPUT)
        {
            return;
        } // Case where source node doesn't exist (and it's not an external input)
        else if (nodes.ContainsKey(edge.From) == false)
        {
            // TODO: _properly_ handle orphaned edges with duplicate key, including if exact edge already exists
            if (orphanedOutgoingEdges.ContainsKey(edge.From) == false)
            {
                orphanedOutgoingEdges.Add(edge.From, edge);
            }
        }
        else // Source node _does_ exist and isn't an external input
        {
            // Case where we want to merge the edge annotation data
            if (nodes[edge.From].ContainsEdge(edge))
            {
                nodes[edge.From].GetEdge(edge.To).MergeData(edge);
                return;
            }

             nodes[edge.From].AddEdge(edge);  
        }
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