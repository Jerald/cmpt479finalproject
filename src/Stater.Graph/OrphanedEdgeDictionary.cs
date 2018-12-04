using System;
using System.Collections;
using System.Collections.Generic;


namespace Stater.Graph
{
    // Future work: maybe take the effort to make this implement IDictionary
    public class OrphanedEdgeDictionary
    {
        private class OrphanedEdgeFactory
        {
            private OrphanedEdgeDictionary dictionaryReference;

            private OrphanedEdge.ConstructorDelegate orphanedEdgeConstructor = OrphanedEdge.GetConstructor();

            public OrphanedEdgeFactory(OrphanedEdgeDictionary dictionary)
            {
                dictionaryReference = dictionary;
            }

            // The preferred way to create an OrphanedEdge
            public OrphanedEdge MakeEdge(Edge edge)
            {
                OrphanedEdge orphanedEdge = orphanedEdgeConstructor(edge);
                return orphanedEdge;
            }
        }

        private class OrphanedEdge
        {
            public bool orphanedIncoming = false;
            public bool orphanedOutgoing = false;
            
            public readonly Edge edge;

            // No one should call this directly
            private OrphanedEdge(Edge edge)
            {
                this.edge = edge;
            }

            // Needed since a delegate to a constructor isn't possible
            static private OrphanedEdge make(Edge edge)
            {
                return new OrphanedEdge(edge);
            }

            internal delegate OrphanedEdge ConstructorDelegate(Edge edge);

            internal static ConstructorDelegate GetConstructor()
            {
                return make;
            }
        }

        OrphanedEdgeFactory orphanedEdgeFactory;

        private readonly Dictionary<string, OrphanedEdge> incomingEdges = new Dictionary<string, OrphanedEdge>();
        private readonly Dictionary<string, OrphanedEdge> outgoingEdges = new Dictionary<string, OrphanedEdge>();

        public OrphanedEdgeDictionary()
        {
            orphanedEdgeFactory = new OrphanedEdgeFactory(this);
        }

        public Edge this[KeyValuePair<string, string> toFromPair]
        {
            get { return this.Get(toFromPair); }
            set { this.Set(value); }
        }

        public Edge Get(KeyValuePair<string, string> toFromPair)
        {
            // TODO: replace with real implementation!
            return new Edge("", "", new KeyValuePair<string, string>("", ""));
        }

        public void Set(Edge edge)
        {
            // TODO: give real implementation!
            OrphanedEdge orphanedEdge = orphanedEdgeFactory.MakeEdge(edge);
        }

        public void Remove(KeyValuePair<string, string> toFromPair)
        {
            // TODO: give real implementation!
        }

        public bool ContainsKey(KeyValuePair<string, string> toFromPair)
        {
            // TODO: give real implementation!Edge
        }

        public ICollection<KeyValuePair<string, string>> Keys()
        {
            return 
        }

        public ICollection<Edge> Values()
        {

        }

    }
}