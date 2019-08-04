using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace theFermiParadox.Core
{
    public struct BasicStar
    {
        public string StarType { get; set; }
        public int SizeCode { get; set; }
        public int Random { get; set; }
        public double Luminosity { get; set; }
        public double Mass { get; set; }
        public double SurfaceTemperature { get; set; }
        public double Radius { get; set; }
    }
}