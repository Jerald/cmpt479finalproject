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
        string output = "From: '" + this.From + "'    To: '" + this.To + "'    Data: '" + Data.ToString() + "'";
        return output;
    }
}

}