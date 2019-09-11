using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theFermiParadox.Core.Abstracts;

namespace theFermiParadox.Core
{
    public abstract class MutationVisitor
    {
        public INode VisitNode(INode n) => n.Accept(this);

        public virtual INode Visit(APhysicalObject n)
        {
            if (n is IOrbitable)
            {
                foreach (Orbit orbit in n.ChildOrbits)
                {                  
                    VisitNode(orbit);
                }
            }
            return n;
        }
        public virtual INode Visit(Barycenter n)
        {
            if (n is IOrbitable)
            {
                foreach (Orbit orbit in n.ChildOrbits)
                {
                    VisitNode(orbit);
                }
            }
            return n;

        }
        public virtual INode Visit(Orbit n)
        {            
            VisitNode(n.Body);    
            return n;
        }
    }
}
