using System.Collections;
using System.Collections.Generic;
using System;

using Stater.Utils;

namespace Stater.Graph
{
    
public class Edge {
    public readonly string From;
    public readonly string To;
    public readonly IDictionary<string, string> Data = new Dictionary<string, string>();

    public Edge(string from, string to, KeyValuePair<string, string> data)
    {
        this.From = from;
        this.To = to;
        this.Data.Add(data);
    }

    public Edge(string from, string to)
    {
        this.From = from;
        this.To = to;
    }

    public void AddData(KeyValuePair<string, string> data)
    {
        Data.Add(data);
    }

    public KeyValuePair<string, string> GetToFromPair()
    {
        return new KeyValuePair<string, string>(this.To, this.From);
    }

    public void MergeData(Edge newEdge)
    {
        foreach (KeyValuePair<string, string> pair in newEdge.Data)
        {
            if (this.Data.ContainsKey(pair.Key))
            {
                //TODO merge data object

                t.SetColor(ConsoleColor.Red);
                // Console.WriteLine("TODO: actually merge data objects!");
                t.DebugPrint("Attempting a shitty merge of: ", pair.ToString());
                t.ResetColor();
                this.Data[pair.Key] += "\n" + pair.Value.ToString();
            }
            else
            {
                this.Data.Add(pair);
            }
        }
    }

    public override string ToString()
    {
        var keys = Data.Keys.GetEnumerator();
        var values = Data.Values.GetEnumerator();
        
        keys.MoveNext();
        values.MoveNext();

        string output = t.tab(2) + "{ from: '" + this.From + "', to: '" + this.To + "', data: {\n";

        foreach (var pair in Data)
        {
            output += t.tab(4) + "{ key: '" + pair.Key + "', value: '" + pair.Value + "' },\n";
        }

        output += t.tab(3) + "}\n";
        output += t.tab(2) + "}";
        
        return output;
    }
}

}