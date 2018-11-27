using System.Collections;
using System.Collections.Generic;

namespace Stater.Graph
{
    
public class Edge {
    public readonly string From;
    public readonly string To;
    public readonly IDictionary<string, object> Data = new Dictionary<string, object>();

    public Edge(string from, string to, KeyValuePair<string, object> data) {
        this.From = from;
        this.To = to;

        this.Data.Add(data);
    }

    public void AddData(KeyValuePair<string, object> data)
    {
        Data.Add(data);
    }

    public override string ToString()
    {
        string output = "{ from: '" + this.From + "', to: '" + this.To + "', data: '" + Data.ToString() + "' }";
        return output;
    }
}

}