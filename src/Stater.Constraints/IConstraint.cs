using System.Collections.Generic;
using Stater.Operations;

namespace Stater.Constraints
{
// This interface defines a constraint in it's most general form.
// Can be implemented in a wide variety of ways to verify different
// classes of behaviour.
public interface IConstraint
{
    // returns the collection of all fields in which 'this' constraint applies
    ICollection<string> getConstrainedFieldIdentifiers();

    // Tests whether or not the collection of operations can violate 'this' constraint
    // in one game tick.

    /* Template:
        Initial state is the abstract domain (AD) representing all concrete states in which
        'this' constraint holds.
        "Apply" operations to AD.
        Return whether or not resulting AD upholds 'this' constraint.
    */ 
    bool test(ICollection<IOperation> operations);
}


}