using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theFermiParadox.Core.Utilities
{
    public class RandomF : Random
    {
        public RandomF(int Seed) : base(Seed)
        {
        }

        public RandomF() : base()
        {
        }

        public int NextInclusive(int a, int b)
        {
            return base.Next(a, b + 1);
        }
    }
}
