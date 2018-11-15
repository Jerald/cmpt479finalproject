using System.Collections.Generic;

public class Node {

    private Dictionary<string, Edge> inEdges;
    private Dictionary<string, Edge> outEdges;
    private string identifier;

    public Node(string indentifier) {
        inEdges = new Dictionary<string, Edge>();
        outEdges = new Dictionary<string, Edge>();
        this.Identifier = indentifier;
    }

    public Dictionary<string, Edge> OutEdges {
        get {
            return outEdges;
        }
        set {
            outEdges = value;
        }
    }

    public Dictionary<string, Edge> InEdges {
        get {
            return inEdges;
        }
        set {
            inEdges = value;
        }
    }

    public string Identifier {
        get {
            return identifier;
        }
        set {
            identifier = value;
        }
    }
}