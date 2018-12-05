using System.Collections.Generic;
using System;
using Stater.Operations;

namespace Stater.Constraints
{
    // Aggregate constraint which is true iff at least one subconstraint is true
    public class WhiteListConstraint : IConstraint
    {
        HashSet<IConstraint> constraints = new HashSet<IConstraint>();
        HashSet<string> constrainedFields = new HashSet<string>();

        public void add(IConstraint constraint)
        {
            constraints.Add(constraint);
            constrainedFields.UnionWith(constraint.getConstrainedFieldIdentifiers());
        }

        public ICollection<string> getConstrainedFieldIdentifiers()
        {
            return constrainedFields;
        }

        public virtual bool test(ICollection<IOperation> operations)
        {
            bool passed = false;
            foreach (IConstraint constraint in constraints)
            {
                passed = passed || constraint.test(operations);
            }
            return passed;
        }
    }
}