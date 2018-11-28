using System;
using System.Collections.Generic;

using Stater.Utils;

using Mono.Cecil;
using Mono.Cecil.Cil;


namespace Stater.Graph
{
    public class Builder
    {
        private List<IAnalysisPass<TypeDefinition>> classAnalysisPasses = new List<IAnalysisPass<TypeDefinition>>();
        private List<IAnalysisPass<FieldDefinition>> fieldAnalysisPasses = new List<IAnalysisPass<FieldDefinition>>();
        private List<IAnalysisPass<MethodDefinition>> methodAnalysisPasses = new List<IAnalysisPass<MethodDefinition>>();
        private List<IAnalysisPass<Instruction>> instructionAnalysisPasses = new List<IAnalysisPass<Instruction>>();

        private string assemblyName;

        private ClassGraph graph;
        private TypeDefinition classDef;
        private FieldDefinition fieldDef;
        private MethodDefinition methodDef;
        private Instruction instruction;

        public Builder(string fileName = "Assembly-CSharp.dll")
        {
            assemblyName = fileName;
            graph = new ClassGraph();
        }

        // Represents the state of a builder. Used by passes so they have information
        // about where the builder is and to have access to the graph being built.
        public class BuilderState
        {
            public readonly ClassGraph graph;
            public readonly TypeDefinition classDef;
            public readonly FieldDefinition fieldDef;
            public readonly MethodDefinition methodDef;
            public readonly Instruction instruction;

            internal BuilderState(Builder builder)
            {
                graph = builder.graph;
                classDef = builder.classDef;
                fieldDef = builder.fieldDef;
                methodDef = builder.methodDef;
                instruction = builder.instruction;
            }    
        }

        public ClassGraph Build()
        {
            ModuleDefinition assembly = ModuleDefinition.ReadModule(assemblyName);

            foreach (TypeDefinition _classDef in assembly.Types)
            {
                classDef = _classDef;

                t.SetColor(ConsoleColor.Cyan);
                t.DebugPrint("Class: ", classDef.ToString());
                t.ResetColor();

                classAnalysis(classDef);

                foreach (FieldDefinition _fieldDef in classDef.Fields)
                {
                    fieldDef = _fieldDef;

                    t.SetColor(ConsoleColor.Magenta);
                    t.DebugPrint(t.tab(1) + "Field: ", fieldDef.ToString());
                    t.ResetColor();

                    fieldAnalysis(fieldDef);
                }

                foreach (MethodDefinition _methodDef in classDef.Methods)
                {
                    methodDef = _methodDef;

                    t.SetColor(ConsoleColor.Magenta);
                    t.DebugPrint(t.tab(1) + "Method: ", methodDef.ToString());
                    t.ResetColor();

                    methodAnalysis(methodDef);

                    foreach (Instruction _instruction in methodDef.Body.Instructions)
                    {
                        instruction = _instruction;

                        instructionAnalysis(instruction);
                    }
                }
            }

            return graph;
        }

        public void AddClassAnalysisPass (IAnalysisPass<TypeDefinition> pass)
        {
            classAnalysisPasses.Add(pass);
        }

        public void AddFieldAnalysisPass(IAnalysisPass<FieldDefinition> pass)
        {
            fieldAnalysisPasses.Add(pass);
        }

        public void AddMethodAnalysisPass(IAnalysisPass<MethodDefinition> pass)
        {
            methodAnalysisPasses.Add(pass);
        }

        public void AddInstructionAnalysisPass(IAnalysisPass<Instruction> pass)
        {
            instructionAnalysisPasses.Add(pass);
        }

        private void classAnalysis(TypeDefinition classDef)
        {
            BuilderState state = new BuilderState(this);

            foreach (var pass in classAnalysisPasses)
            {
                pass.analyze(classDef, state);
            }
        }

        private void fieldAnalysis(FieldDefinition fieldDef)
        {
            BuilderState state = new BuilderState(this);

            foreach (var pass in fieldAnalysisPasses)
            {
                pass.analyze(fieldDef, state);
            }
        }

        private void methodAnalysis(MethodDefinition methodDef)
        {
            BuilderState state = new BuilderState(this);

            foreach (var pass in methodAnalysisPasses)
            {
                pass.analyze(methodDef, state);
            }
        }

        private void instructionAnalysis(Instruction instruction)
        {
            BuilderState state = new BuilderState(this);

            foreach (var pass in instructionAnalysisPasses)
            {
                pass.analyze(instruction, state);
            }
        }
    }
}