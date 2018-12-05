using System.Collections.Generic;
using System;
using Stater.Operations;

namespace Stater.Constraints
{
    // Wrapper to extend range constraint to n dimensions
    public class BoundingBoxConstraint : IConstraint
    {
        private Dictionary<string, RangeConstraint> bounds = new Dictionary<string, RangeConstraint>();

        public BoundingBoxConstraint(List<string> fieldIDs, List<Tuple<double, double>> minMax)
        {
            if (fieldIDs.Count != minMax.Count)
            {
                throw new System.ArgumentException("FieldIDs and minMax lists must be of equal length");
            }
            for (int i = 0; i < fieldIDs.Count; i++)
            {
                if (bounds.ContainsKey(fieldIDs[i]))
                {
                    throw new ArgumentException("Each field id in bounding box constraint must be unique");
                }
                bounds.Add(fieldIDs[i], new RangeConstraint(fieldIDs[i], minMax[i].Item1, minMax[i].Item2));
            }
        }

        public BoundingBoxConstraint(List<RangeConstraint> ranges)
        {
            foreach (RangeConstraint range in ranges)
            {
                if (bounds.ContainsKey(range.FieldId))
                {
                    throw new ArgumentException("Each field id in bounding box constraint must be unique");
                }
                bounds.Add(range.FieldId, range);
            }
        }

        public ICollection<string> getConstrainedFieldIdentifiers()
        {
            return bounds.Keys;
        }

        public bool test(ICollection<IOperation> operations)
        {
            bool passed = true;
            foreach (RangeConstraint range in bounds.Values)
            {  
                passed = passed && range.test(operations);
            }
            return passed;
        }
    }
}