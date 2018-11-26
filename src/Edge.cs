namespace Stater
{
    
public class Edge {
    public readonly string From;
    public readonly string To;
    public readonly System.Object Data;

    public Edge(string from, string to, System.Object data) {
        this.From = from;
        this.To = to;
        this.Data = data;
    }

    public override string ToString()
    {
        string output = "{ from: '" + this.From + "', to: '" + this.To + "', data: '" + Data.ToString() + "' }";
        return output;
    }
}

}