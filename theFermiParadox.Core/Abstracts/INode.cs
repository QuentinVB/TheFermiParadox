namespace theFermiParadox.Core.Abstracts
{
    public interface INode
    {
        void Accept(Visitor v);
        INode Accept(MutationVisitor v);
    }
}