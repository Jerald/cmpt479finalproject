using System.Collections.Generic;
using Stater.Operations;

namespace Stater.Constraints
{
// Aggregate constraint which is true iff all subconstraints are false
public class BlackListConstraint : WhiteListConstraint
{
    public override bool test(ICollection<IOperation> operations){
        return !base.test(operations);
    }
}

}