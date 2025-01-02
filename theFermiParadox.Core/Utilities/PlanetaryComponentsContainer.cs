using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theFermiParadox.Core.Utilities
{
    public class PlanetaryComponentsContainer : List<string>
    {
        public PlanetaryComponentsContainer()
        {

        }
        public override string ToString()
        {
            string offset = "  - ";
            string answer = "\n  Caracteristics :\n";
            if (this.Count != 0)
            {
                foreach (string item in this)
                {
                    answer += offset + item.ToString() + "\n";
                }
                return answer;
            }
            return offset + "none";
        }

    }
}
