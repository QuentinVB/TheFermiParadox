using System.Text;
using theFermiParadox.Core.Abstracts;
using theFermiParadox.Core.Utilities;

namespace theFermiParadox.Core
{
    public class FullPrintVisitor : Visitor
    {
        StringBuilder _buffer = new StringBuilder();

        public string Result => _buffer.ToString();

        public override void Visit(APhysicalObject n)
        {
            _buffer.Append(n.ToString());

            if (n.ChildOrbits.Count>0)
            {
                _buffer.Append("{\n");
                foreach (Orbit orbit in n.ChildOrbits)
                {
                    VisitNode(orbit);
                }
                _buffer.Append("\n}");
            }           
        }
        public override void Visit(Barycenter n)
        {
            _buffer.Append(n.ToString());

            if (n.ChildOrbits.Count > 0)
            {
                _buffer.Append("{\n");
                foreach (Orbit orbit in n.ChildOrbits)
                {
                    VisitNode(orbit);
                }
                _buffer.Append("\n}");
            }
        }
        public override void Visit(Orbit n)
        {
            _buffer.Append("(\n");
            VisitNode(n.Body);
            _buffer.Append("\n=======================\n\n");
            _buffer.Append(n.ToString());
            _buffer.Append("\n)");
        }

        internal void Clear()
        {
            _buffer.Clear();
        }
    }
}
