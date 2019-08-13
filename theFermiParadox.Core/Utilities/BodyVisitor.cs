using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theFermiParadox.Core.Abstracts;

namespace theFermiParadox.Core
{
    public abstract class BodyVisitor
    {
        public void VisitNode(IOrbitable n) => n.Accept(this);

        public virtual void Visit(APhysicalObject n)
        {
            if (n is IOrbitable)
            {
                foreach (Orbit orbit in n.ChildOrbits)
                {
                    VisitNode(orbit.Body);

                }
            }
        }
        public virtual void Visit(Barycenter n)
        {
            if (n is IOrbitable)
            {
                foreach (Orbit orbit in n.ChildOrbits)
                {
                    VisitNode(orbit.Body);

                }
            }
        }
        /*
        public virtual void Visit(IOrbitable n)
        {


            foreach (Orbit orbit in n.ChildOrbits)
            {
                VisitNode(orbit.Body);

            }

            /*
            VisitNode(n.Left);
            VisitNode(n.Right);
        }*/
    }
}
