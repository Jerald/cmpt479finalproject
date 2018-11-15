using System;

namespace Stater
{

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World!");

        ClassGraph graph = new ClassGraph();
        graph.addNode("Node1");
        graph.addNode("Node2");
        graph.addEdge("Node1", "Node2", "Hello World");

    }
}

}
