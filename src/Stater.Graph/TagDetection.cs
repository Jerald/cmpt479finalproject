using System;
using Mono.Cecil;

using Stater.Constants.Dotnet;
using Stater.Constants.Unity;

namespace Stater.Graph
{
static class TagDetection
{
    public static void FindTagUsage(MethodDefinition methodDef)
    {
        MethodReference getTag = null;

        Console.WriteLine("    Method: '" + methodDef.FullName + "'");

        foreach (var instruction in methodDef.Body.Instructions)
        {
            if (instruction.OpCode.ToString() == DotnetConstants.OPCODE_CALLVIRT)
            {
                if (instruction.Operand.ToString() == UnityConstants.GET_TAG_METHOD)
                {
                    getTag = (MethodReference)instruction.Operand;
                }
            }
        }

        if (getTag != null)
        {
            Console.WriteLine("Get tag name: " + getTag.FullName);
        }

        
    }
}
}