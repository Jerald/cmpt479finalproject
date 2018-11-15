using System.Collections.Generic;

namespace Stater
{
    
public class ClassGraph {
    Dictionary<string, Node> nodes;

    public ClassGraph() {
        nodes = new Dictionary<string, Node>();
    }

    public void addNode(string identifier) {
        Node node = new Node(identifier);
        nodes.Add(identifier, node);
    }

    public void addEdge(string fromIdentifier, string toIdentifier, System.Object data) {
        Edge edge = new Edge(fromIdentifier, toIdentifier, data);
        nodes[fromIdentifier].OutEdges.Add(toIdentifier, edge);
        nodes[toIdentifier].InEdges.Add(fromIdentifier, edge);
    }

    public void removeEdge(string fromIdentifier, string toIdentifier) {
        nodes[fromIdentifier].OutEdges.Remove(toIdentifier);
        nodes[toIdentifier].InEdges.Remove(fromIdentifier);
    }

    public Dictionary<string, Edge>.ValueCollection getIncomingEdges(string identifier) {
        return nodes[identifier].InEdges.Values;
    }

    public Dictionary<string, Edge>.ValueCollection getOutgoingEdges(string identifier) {
        return nodes[identifier].OutEdges.Values;
    }

}

}