using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using theFermiParadox.Core.Utilities;

namespace theFermiParadox.Core.Abstracts
{
    public abstract class Node
    {
        public abstract void Accept(Visitor v);

        public abstract Node Accept(MutationVisitor v);

        public override string ToString()
        {
            string rslt = "";
            foreach (PropertyInfo p in this.GetType().GetProperties().ToList())
            {
                System.Attribute[] attrs = System.Attribute.GetCustomAttributes(p);
                bool isPrintable = true;
                foreach (System.Attribute attr in attrs)
                {
                    if (attr is NonPrintable)
                    {
                        isPrintable = false;
                    }
                }
                if (isPrintable)
                {
                    rslt += "  " + p.Name + " : " + (this.GetType().GetProperty(p.Name).GetValue(this, null) ?? "none").ToString() + "\n";
                }
                else
                {
                    rslt += "  " + p.Name + "\n";
                }
            }
            return rslt;
        }
    }
}
