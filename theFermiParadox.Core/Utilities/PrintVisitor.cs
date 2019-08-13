using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theFermiParadox.Core.Abstracts;

namespace theFermiParadox.Core
{
    public class PrintVisitor: BodyVisitor
    {
        StringBuilder _buffer = new StringBuilder();

        public string Result => _buffer.ToString();

        public override void Visit(APhysicalObject n)
        {
            _buffer.Append(n.Name);

            _buffer.Append('(');
            foreach (Orbit orbit in n.ChildOrbits)
            {
                VisitNode(orbit.Body);
                _buffer.Append(',');

            }
            _buffer.Append(')');

            /*
            VisitNode(n.Left);
            VisitNode(n.Right);
            */
        }
        public override void Visit(Barycenter n)
        {
            _buffer.Append('*');

            _buffer.Append('(');
            foreach (Orbit orbit in n.ChildOrbits)
            {
                VisitNode(orbit.Body);
                _buffer.Append(',');

            }
            _buffer.Append(')');

            /*
            VisitNode(n.Left);
            VisitNode(n.Right);
            */
        }


        public override void Visit(IOrbitable n)
        {

            _buffer.Append(n.Name);

            _buffer.Append('(');
            foreach (Orbit orbit in n.ChildOrbits)
            {
                VisitNode(orbit.Body);
                _buffer.Append(',');

            }
            _buffer.Append(')');

            /*
            VisitNode(n.Left);
            VisitNode(n.Right);
            */
        }

    }
}
