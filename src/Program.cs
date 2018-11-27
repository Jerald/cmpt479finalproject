using System;
using Mono.Cecil;
using System.Collections.Generic;

using Stater.Graph;

namespace Stater
{

class Program
{
    static void Main(string[] args)
    {
        Builder builder = new Builder();

        builder.AddClassAnalysisPass(
            new AnalysisPasses.NodeAddingClassPass());

        builder.AddInstructionAnalysisPass(
            new AnalysisPasses.InputFindingInstructionPass());

        ClassGraph graph = builder.Build();

        Console.WriteLine(graph.ToString());

        // CecilTests("Assembly-CSharp.dll");
        // GraphTests();
    }

//     static void GraphTests()
//     {
//         SceneManager.CreateScene("Scene1");
//         SceneManager.CreateScene("Scene2");

//         List<string> scenes = new List<string>();
//         scenes.Add("Scene1");
//         scenes.Add("Scene2");
//         SceneManager.CreateNodeInScenes("Node_1", scenes);
//         SceneManager.CreateNodeInScenes("Node_2", scenes);
//         scenes.Remove("Scene2");
//         SceneManager.CreateNodeInScenes("Node_3", scenes);

//         Edge edge1 = new Edge("Node_1", "Node_2", "data1");
//         Edge edge2 = new Edge("Node_2", "Node_1", "data2");
//         Edge edge3 = new Edge("Node_2", "Node_3", "data3");
//         Edge edge4 = new Edge("Node_3", "Node_1", "data4");

//         SceneManager.AddEdgeToAllValidScenes(edge1);
//         SceneManager.AddEdgeToAllValidScenes(edge3);
//         SceneManager.AddEdgeToScenes(edge4, scenes);
//         scenes.Add("Scene2");
//         SceneManager.AddEdgeToScenes(edge2, scenes);

//         Console.WriteLine("Scene 1:");
//         ClassGraph scene1 = SceneManager.GetSceneGraph("Scene1");
//         foreach(Node node in scene1.GetNodes())
//         {
//             ICollection<Edge> inEdges = node.GetIncoming();
//             Console.WriteLine(node.ID+" Incoming edges");
//             foreach (var edge in inEdges)
//             {
//                 Console.WriteLine(edge.ToString());
//             }
//         }

//         Console.WriteLine("Scene 2:");
//         ClassGraph scene2 = SceneManager.GetSceneGraph("Scene2");
//         foreach(Node node in scene2.GetNodes())
//         {
//             ICollection<Edge> inEdges = node.GetIncoming();
//             Console.WriteLine(node.ID+" Incoming edges");
//             foreach (var edge in inEdges)
//             {
//                 Console.WriteLine(edge.ToString());
//             }
//         }
//     }

//     static void CecilTests(string fileName)
//     {
//         SceneManager.RemoveAllScenes();
//         SceneManager.CreateScene("SampleScene");
//         ClassGraph sampleScene = SceneManager.GetSceneGraph("SampleScene");

//         ModuleDefinition module = ModuleDefinition.ReadModule(fileName);

//         Console.WriteLine("Writing types from file '" + fileName + "'");
//         foreach (TypeDefinition type in module.Types)
//         {
//             if (type.IsPublic == false)
//             {
//                 continue;
//             }

//             Console.WriteLine("Type: '" + type.FullName + "'");
            
//             foreach (var fieldDef in type.Fields)
//             {
//                 Console.WriteLine("    Field: '" + fieldDef + "'");
//             }

//             foreach (var eventDef in type.Events)
//             {
//                 Console.WriteLine("    Event: '" + eventDef + "'");
//             }

//             MethodDefinition updateDef = null;
//             foreach (var methodDef in type.Methods)
//             {
//                 if (methodDef.Name == "Update")
//                 {
//                     updateDef = methodDef;
//                 }

//                 // Stater.Graph.TagDetection.FindTagUsage(methodDef);   

//                 Console.WriteLine("    Method: '" + methodDef + "'");
//             }

//             sampleScene.AddNode(new Node(type.Name));
//         }

//         sampleScene.AddEdge(new Edge("FollowTarget", "Player", "hello world"));
//         Console.WriteLine(sampleScene.ToString());

//     }
}

}
