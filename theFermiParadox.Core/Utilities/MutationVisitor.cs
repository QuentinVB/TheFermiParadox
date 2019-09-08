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
        public Node VisitNode(Node n) => n.Accept(this);

        public virtual Node Visit(APhysicalObject n)
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
        public virtual Node Visit(Barycenter n)
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
        public virtual Node Visit(Orbit n)
        {            
            VisitNode(n.Body as ABody);    
            return n;
        }
    }
}
