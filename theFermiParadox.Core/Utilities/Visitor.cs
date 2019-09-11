using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theFermiParadox.Core.Abstracts;

namespace theFermiParadox.Core
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
