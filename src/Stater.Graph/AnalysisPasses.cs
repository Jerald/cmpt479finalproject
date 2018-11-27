using Mono.Cecil;
using Mono.Cecil.Cil;

using Stater.Utils;
using Stater.Constants.Dotnet;
using Stater.Constants.Unity;
using Stater.Constants.Stater;


namespace Stater.Graph
{
    public static class AnalysisPasses
    {
        // Simply adds nodes for classes found
        public class NodeAddingClassPass : IAnalysisPass<TypeDefinition>
        {
            public void analyze(TypeDefinition classDef, Builder.BuilderState builderState)
            {
                // This is some weird default class added, so we don't care about it
                if (classDef.Name == "<Module>")
                {
                    return;
                }

                Node node = new Node(classDef.Name);
                builderState.graph.AddNode(node);
            }
        }

        // Looks for usage of the unity input class and adds an edge from it if it's found
        public class InputFindingInstructionPass : IAnalysisPass<Instruction>
        {
            public void analyze(Instruction instruction, Builder.BuilderState builderState)
            {
                // If we find a call or callvirt instruction...
                if (instruction.OpCode.Name == DotnetConstants.OPCODE_CALL
                    || instruction.OpCode.Name == DotnetConstants.OPCODE_CALLVIRT)
                {
                    MethodReference callOperand = (MethodReference)instruction.Operand;
                    TypeReference declaringClass = callOperand.DeclaringType;

                    // ...and if the function being called is from the unity input class...
                    if (declaringClass.FullName == UnityConstants.INPUT_CLASS)
                    {
                        t.debugPrint("Found ", UnityConstants.INPUT_CLASS);

                        // ...then we add the input class as a parameter of the node using an edge
                        Node node = builderState.graph[builderState.classDef.Name];
                        Edge edge = new Edge(StaterConstants.EXTERNAL_INPUT, node.ID, UnityConstants.INPUT_CLASS);

                        builderState.graph.AddEdge(edge);
                    }
                }
            }
        }
    }
}