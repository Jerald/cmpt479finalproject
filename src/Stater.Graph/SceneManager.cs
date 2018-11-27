using System.Collections.Generic;
using Stater.Graph;

namespace Stater
{
static class SceneManager
{
    private static Dictionary<string, ClassGraph> sceneGraphs = new Dictionary<string, ClassGraph>();

    public static void CreateScene(string sceneId)
    {
        sceneGraphs.Add(sceneId, new ClassGraph());
    }

    public static void RemoveScene(string sceneId)
    {
        sceneGraphs.Remove(sceneId);
    }

    public static void RemoveAllScenes()
    {
        sceneGraphs.Clear();
    }

    public static ClassGraph GetSceneGraph(string sceneId)
    {
        return sceneGraphs[sceneId];
    }

    public static void CreateNodeInScenes(string nodeId, IList<string> sceneIds)
    {
        foreach(string sceneId in sceneIds)
        {
            sceneGraphs[sceneId].AddNode(new Node(nodeId));
        }
    }

    public static void AddEdgeToScenes(Edge edge, IList<string> sceneIds)
    {
        string fromNode = edge.From;
        string toNode = edge.To;

        foreach(string sceneId in sceneIds)
        {
            ClassGraph sceneGraph = sceneGraphs[sceneId];
            if(sceneGraph.ContainsNode(fromNode) && sceneGraph.ContainsNode(toNode))
            {
                sceneGraph.AddEdge(edge);
            }
        }
    }

    public static void AddEdgeToAllValidScenes(Edge edge)
    {
        string fromNode = edge.From;
        string toNode = edge.To;

        foreach(ClassGraph sceneGraph in sceneGraphs.Values)
        {
            if(sceneGraph.ContainsNode(fromNode) && sceneGraph.ContainsNode(toNode))
            {
                sceneGraph.AddEdge(edge);
            }
        }
    }


}

}