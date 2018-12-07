namespace Stater.Operations{

public abstract class IOperation{

        private string field;
        private bool isConditional;

        public string Field { get => field; set => field = value; }
        public bool IsConditional { get => isConditional; set => isConditional = value; }

        // some representation of condition


        // TODO:: other things?
        // AbstractDomain apply(AbstractDomain ad)?

    }

}