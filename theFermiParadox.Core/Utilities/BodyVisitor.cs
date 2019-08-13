using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theFermiParadox.Core
{
    public abstract class BodyVisitor
    {
       

        public void VisitNode(Node n) => n.Accept(this);

        public virtual void Visit(BinaryNode n)
        {
            VisitNode(n.Left);
            VisitNode(n.Right);
        }

    }
}
