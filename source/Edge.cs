public class Edge {
    public readonly string fromIdentifier;
    public readonly string toIdentifier;
    public readonly System.Object data;

    public Edge(string fromIdentifier, string toIdentifier, System.Object data) {
        this.fromIdentifier = fromIdentifier;
        this.toIdentifier = toIdentifier;
        this.data = data;
    }
}