using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theFermiParadox.Core.Abstracts;

namespace theFermiParadox.Core
{
    public class PrintVisitor: Visitor
    {
        StringBuilder _buffer = new StringBuilder();

        public string Result => _buffer.ToString();

        public override void Visit(APhysicalObject n)
        {
            _buffer.Append(n.Name);

            if (n.ChildOrbits.Count>0)
            {
                _buffer.Append('(');
                foreach (Orbit orbit in n.ChildOrbits)
                {
                    VisitNode(orbit);
                }
                _buffer.Append(')');
            }
            
        }
        public override void Visit(Barycenter n)
        {
            _buffer.Append('*');

            if (n.ChildOrbits.Count > 0)
            {
                _buffer.Append('(');
                foreach (Orbit orbit in n.ChildOrbits)
                {
                    VisitNode(orbit);
                }
                _buffer.Append(')');
            }
        }
        public override void Visit(Orbit n)
        {
            _buffer.Append('(');          
            VisitNode(n.Body);            
            _buffer.Append(')');
        }
    }
}
