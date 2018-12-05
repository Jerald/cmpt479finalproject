using System.Collections.Generic;
using System;
using Stater.Operations;

namespace Stater.Constraints
{
    public class RangeConstraint : IConstraint
    {
        string fieldId;
        double min;
        double max;

        public RangeConstraint(string fieldId, double min, double max)
        {
            this.fieldId = fieldId;
            if(max < min){
                throw new ArgumentException("Min must be less than or equal to Max");
            }
            this.min = min;
            this.max = max;
        }

        public string FieldId { get => fieldId;}
        public double Min { get => min;}
        public double Max { get => max;}

        public ICollection<string> getConstrainedFieldIdentifiers()
        {
            List<string> ret = new List<string>();
            ret.Add(FieldId);
            return ret;
        }

        public bool test(ICollection<IOperation> operations)
        {   
            // Define abstract domain as upper and lower bound covering all valid states for this range constraint
            double lower = Min;
            double upper = Max;
            foreach (IOperation o in operations)
            {
                // TODO:: if(o.field != FieldId) then continue;

                // TODO:: apply o to abstract domain according to conditional?
            }
            return lower >= Min && upper <= Max;
        }
    }
}