using System;
using Mono.Cecil;
using System.Collections.Generic;

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
        


        SceneManager.CreateScene("Scene1");
        SceneManager.CreateScene("Scene2");

        List<string> scenes = new List<string>();
        scenes.Add("Scene1");
        scenes.Add("Scene2");
        SceneManager.CreateNodeInScenes("Node_1", scenes);
        SceneManager.CreateNodeInScenes("Node_2", scenes);
        scenes.Remove("Scene2");
        SceneManager.CreateNodeInScenes("Node_3", scenes);

        Edge edge1 = new Edge("Node_1", "Node_2", "data1");
        Edge edge2 = new Edge("Node_2", "Node_1", "data2");
        Edge edge3 = new Edge("Node_2", "Node_3", "data3");
        Edge edge4 = new Edge("Node_3", "Node_1", "data4");

        SceneManager.AddEdgeToAllValidScenes(edge1);
        SceneManager.AddEdgeToAllValidScenes(edge3);
        SceneManager.AddEdgeToScenes(edge4, scenes);
        scenes.Add("Scene2");
        SceneManager.AddEdgeToScenes(edge2, scenes);

        Console.WriteLine("Scene 1:");
        ClassGraph scene1 = SceneManager.GetSceneGraph("Scene1");
        foreach(Node node in scene1.GetNodes())
        {
            ICollection<Edge> inEdges = node.GetIncoming();
            Console.WriteLine(node.ID+" Incoming edges");
            foreach (var edge in inEdges)
            {
                Console.WriteLine(edge.ToString());
            }
        }

        Console.WriteLine("Scene 2:");
        ClassGraph scene2 = SceneManager.GetSceneGraph("Scene2");
        foreach(Node node in scene2.GetNodes())
        {
            ICollection<Edge> inEdges = node.GetIncoming();
            Console.WriteLine(node.ID+" Incoming edges");
            foreach (var edge in inEdges)
            {
                Console.WriteLine(edge.ToString());
            }
        }
        
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
