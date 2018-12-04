using System;
using System.Collections.Generic;

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
        public class AddNodeClassPass : IAnalysisPass<TypeDefinition>
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
                        t.DebugPrint("Found ", UnityConstants.INPUT_CLASS);

                        // ...then we add the input class as a parameter of the node using an edge
                        Node node = builderState.graph[builderState.classDef.Name];
                        
                        Edge edge = new Edge(StaterConstants.EXTERNAL_INPUT, node.ID,
                            new KeyValuePair<string, string>(UnityConstants.INPUT_CLASS, "I'm a unity input class!"));

                        // t.DebugPrint("Input instruction: ", instruction.ToString());

                        builderState.graph.AddEdge(edge);
                    }
                }
            }
        }
    
        // Prints ALL instructions with no extra information
        public class PrintAllInstructionPass : IAnalysisPass<Instruction>
        {
            public void analyze(Instruction instruction, Builder.BuilderState builderState)
            {
                t.SetColor(ConsoleColor.Yellow);
                t.DebugPrint(t.tab(1) + "Inst: ", instruction.ToString());
                t.ResetColor();
            }
        }

        // Prints a bunch of information on the instructions of a method specified
        public class PrintMethodInfoInstructionPass : IAnalysisPass<Instruction>
        {
            private string methodName;

            public PrintMethodInfoInstructionPass(string methodName)
            {
                this.methodName = methodName;
            }

            public void analyze(Instruction instruction, Builder.BuilderState builderState)
            {
                if (builderState.methodDef.FullName != methodName)
                {
                    return;
                }
                
                t.SetColor(ConsoleColor.Yellow);
                t.DebugPrint(t.tab(1) + "Inst: ", instruction.ToString());

                t.SetColor(ConsoleColor.Blue);
                t.DebugPrint(t.tab(2) + "Opcode: ", instruction.OpCode.ToString());

                if (instruction.Operand != null)
                {
                    var objOperand = instruction.Operand;
                    var operandType = objOperand.GetType();

                    t.DebugPrint(t.tab(2) + "Operand: ", objOperand.ToString());      
                    t.DebugPrint(t.tab(2) + "Operand type: ", operandType.ToString());

                    t.SetColor(ConsoleColor.DarkGreen);

                    if (operandType == typeof(Mono.Cecil.FieldDefinition))
                    {
                        FieldDefinition operand = (FieldDefinition)objOperand;
                        
                        t.DebugPrint(t.tab(3) + "Field declaring type: ", operand.DeclaringType.FullName);
                        t.DebugPrint(t.tab(3) + "Field field type: ", operand.FieldType.FullName);
                    }
                    else if (operandType == typeof(Mono.Cecil.MethodReference))
                    {
                        MethodReference operand = (MethodReference)objOperand;

                        t.DebugPrint(t.tab(3) + "Method ref return type: ", operand.ReturnType.FullName);
                    }
                    else if (operandType == typeof(Mono.Cecil.MethodDefinition))
                    {
                        MethodDefinition operand = (MethodDefinition)objOperand;

                        t.DebugPrint(t.tab(3) + "Method def declaring type: ", operand.DeclaringType.FullName);
                        t.DebugPrint(t.tab(3) + "Method def return type: ", operand.ReturnType.FullName);
                    }
                }

                t.ResetColor();
            }
        }
    
        public class FindNodeUsageInstructionPass : IAnalysisPass<Instruction>
        {
            private bool fieldLoadedLast = false;
            private string fieldNodeID = "";
            public void analyze(Instruction instruction, Builder.BuilderState builderState)
            {
                if (instruction.Operand == null)
                {
                    return;
                }

                var opcode = instruction.OpCode;
                var operand = instruction.Operand;

                if (fieldLoadedLast == true)
                {
                    // var operandFieldDef = (FieldDefinition)operand;
                    // var fieldType = operandFieldDef.FieldType;

                    ClassGraph graph = builderState.graph;
                    Edge edge = new Edge(builderState.classDef.FullName, fieldNodeID, new KeyValuePair<string, string>(operand.ToString(), "This is from somewhere else!"));
                    graph.AddEdge(edge);

                    fieldLoadedLast = false;
                    fieldNodeID = "";
                }

                if (opcode.Name == DotnetConstants.OPCODE_LDFLD
                    && operand.GetType() == typeof(Mono.Cecil.FieldDefinition))
                {
                    var operandFieldDef = (FieldDefinition)operand;
                    var fieldType = operandFieldDef.FieldType;
                    var nodeID = fieldType.FullName;

                    fieldNodeID = nodeID;
                    fieldLoadedLast = true;

                    t.SetColor(ConsoleColor.White);
                    t.DebugPrint("AnalysisPasses.FindNodeUsageInstructionPass -- field loaded:", fieldType.FullName);
                    t.ResetColor();
                }
            }
        }
    }
}