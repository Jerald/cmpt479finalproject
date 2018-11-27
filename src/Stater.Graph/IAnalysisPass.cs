namespace Stater.Graph
{
    // This interface is for a class which would be added as an analysis pass
    // to be ran while building out a class graph
    public interface IAnalysisPass<T>
    {
        void analyze(T input, Builder.BuilderState builderState);

    }
}