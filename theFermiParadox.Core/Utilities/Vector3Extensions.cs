using System;
using System.Numerics;

namespace theFermiParadox.Core.Utilities
{
    public static class Vector3Extensions
    {
        public static Angle Angle(Vector3 a, Vector3 b)
        {
            return Math.Acos((a.X * b.X + a.Y * b.Y + a.Z * b.Z) / (a.Length() * b.Length()));
        }
        //override//
        public static string ToString(this Vector3 v)
        {
            return v.X + ":" + v.Y + ":" + v.Z;
        }
        //convertor//
        public static Polar3 ToPolar(this Vector3 a)
        {
            return new Polar3(Math.Sqrt(a.X * a.X + a.Y * a.Y + a.Z * a.Z), Math.Atan2(a.Y, a.X), Math.Acos(a.Z / Math.Sqrt(a.X * a.X + a.Y * a.Y + a.Z * a.Z)));
        }
    }
}