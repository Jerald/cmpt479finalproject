using System;
using Mono.Cecil;

namespace Stater
{

class Program
{
    static void Main(string[] args)
    {
        ClassGraph graph = new ClassGraph();

        Node node1 = new Node("Node1");
        Node node2 = new Node("Node2");

        graph.AddNode(node1);
        graph.AddNode(node2);
        graph.AddNode(new Node("Node3"));

        graph.AddEdge(new Edge("Node1", "Node2", "Hello World"));
        graph.AddEdge(new Edge("Node3", "Node2", "Goodbye world"));

        var incoming = graph.GetIncoming("Node2");
        
        Console.WriteLine("Incoming edges");
        foreach (var edge in incoming)
        {
            Console.WriteLine(edge.ToString());
        }

        PrintTypes("Assembly-CSharp.dll");

    }

    static void PrintTypes(string fileName)
    {
        ModuleDefinition module = ModuleDefinition.ReadModule(fileName);

        Console.WriteLine("Writing types from file '" + fileName + "'");
        foreach (TypeDefinition type in module.Types)
        {
            if (type.IsPublic == false)
            {
                continue;
            }

            Console.WriteLine(type.FullName);
        }
    }
}

}
