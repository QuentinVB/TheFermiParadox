using theFermiParadox.Core.Abstracts;
using theFermiParadox.Core.Interfaces;

namespace theFermiParadox.Core.Utilities
{
    public abstract class Visitor
    {
        public void VisitNode(INode n) => n.Accept(this);

        public virtual void Visit(APhysicalObject n)
        {
            if (n is IOrbitable)
            {
                foreach (Orbit orbit in n.ChildOrbits)
                {                  
                    VisitNode(orbit);
                }
            }
        }
        public virtual void Visit(Barycenter n)
        {
            if (n is IOrbitable)
            {
                foreach (Orbit orbit in n.ChildOrbits)
                {
                    VisitNode(orbit);
                }
            }
        }
        public virtual void Visit(Orbit n)
        {            
             VisitNode(n.Body);                
        }
    }
}
