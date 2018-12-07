using Stater.Graph;
using Stater.Constraints;
using Stater.Operations;
using System.Collections.Generic;

public static class Validator
{
    public static void validate(ClassGraph scene)
    {
        foreach(Node node in scene.GetNodes())
        {
            foreach(IConstraint constraint in node.Constraints)
            {
                ICollection<string> constrainedFields = constraint.getConstrainedFieldIdentifiers();
                HashSet<IOperation> totalOperations = new HashSet<IOperation>();
                foreach(Edge edge in node.GetIncoming())
                {
                    // Should this instead allow for duplicate operations from different sources?
                    totalOperations.UnionWith(edge.getFilteredData(constrainedFields));
                }
                foreach(HashSet<IOperation> operations in getCombinations(totalOperations))
                {
                    bool test = constraint.test(operations);
                    if(!test)
                    {
                        // TODO:: log constraint and failed set of operations
                    }
                }
            }
        }
    }

    private static List<ICollection<IOperation>> getCombinations(HashSet<IOperation> totalOperations)
    {
        List<ICollection<IOperation>> ret = new List<ICollection<IOperation>>();
        // TODO:: Add all combinations to ret
        ret.Add(totalOperations);
        return ret;
    }
}