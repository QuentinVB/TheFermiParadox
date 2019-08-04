using System;
using System.Collections.Generic;
using System.Linq;
//using System.Numerics;
using System.Text;

namespace Helpers
{
    public struct Polar3
    {
        public Polar3(double r, Angle omega, Angle phi)
        {
            if (r < 0) throw new ArgumentException("polar Radius cant be negative");
            this.R = r;
            this.O = omega;
            this.P = phi;
        }
        public double R { get; set; }
        public Angle O { get; set; }
        public Angle P { get; set; }
        //operator//
        public static Polar3 operator +(Polar3 a, Vector3 b) => (a.ToCartesian() + b).ToPolar();
        public static Polar3 operator +(Vector3 a, Polar3 b) => b + a;
        public static Polar3 operator -(Polar3 a, Vector3 b) => (a.ToCartesian() - b).ToPolar();
        public static Polar3 operator -(Vector3 a, Polar3 b) => b - a;
        //TODO POLAR + POLAR

        //static//
        public static double Distance(Polar3 a, Polar3 b)
        {
            return Vector3.Distance(a.ToCartesian(),b.ToCartesian());
        }

        //converter
        public override string ToString()
        {
            return R + ":" + O + ":" + P;
        }

        public Vector3 ToCartesian()
        {
            return ToCartesian(this);
        }
        public static Vector3 ToCartesian(Polar3 a)
        {
            return new Vector3(a.R * Math.Sin(a.P) * Math.Cos(a.O), a.R * Math.Sin(a.P) * Math.Sin(a.O), a.R * Math.Cos(a.P));
        }

    }
}
