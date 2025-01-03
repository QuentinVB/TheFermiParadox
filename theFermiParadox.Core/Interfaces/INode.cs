using theFermiParadox.Core.Utilities;

namespace theFermiParadox.Core.Interfaces
{
    public interface INode
    {
        void Accept(Visitor v);
        INode Accept(MutationVisitor v);
    }
}