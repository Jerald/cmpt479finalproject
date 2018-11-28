using System.Collections;
using System.Collections.Generic;
using System;

using Stater.Utils;

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

    public void MergeData(Edge newEdge)
    {
        foreach(KeyValuePair<string, object> pair in newEdge.Data)
        {
            if(this.Data.ContainsKey(pair.Key))
            {
                //TODO merge data object

                t.SetColor(ConsoleColor.Red);
                Console.WriteLine("TODO: actually merge data objects!");
                t.ResetColor();
            }
            else
            {
                this.Data.Add(pair);
            }
        }
    }

    public override string ToString()
    {
        // TODO: make this actually print all the dictionary info!

        var keys = Data.Keys.GetEnumerator();
        var values = Data.Values.GetEnumerator();
        
        keys.MoveNext();
        values.MoveNext();

        string output = "{ from: '" + this.From + "', to: '" + this.To + "', data: { keys: '" + keys.Current + "', values: '" + values.Current + "' } }";
        return output;
    }
}

}