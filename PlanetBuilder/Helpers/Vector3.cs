using System;

namespace PlanetBuilder.Helpers
{
    public struct Vector3
    {
        double x;
        double y;
        double z;
        public Vector3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }
        public double Z { get => z; set => z = value; }
        public double Length => Math.Sqrt(Math.Pow(this.X, 2) + Math.Pow(this.Y, 2) + Math.Pow(this.Z, 2));

        //operator//
        public static Vector3 operator +(Vector3 a, Vector3 b) => new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        public static Vector3 operator -(Vector3 a, Vector3 b) => new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        public static Vector3 operator *(Vector3 a, double q) => new Vector3(a.X * q, a.Y * q, a.Z * q);
        public static Vector3 operator /(Vector3 a, double q)
        {
            if (q == 0) throw new DivideByZeroException();
            return new Vector3(a.X / q, a.Y / q, a.Z / q);
        }
        //static method//
        public static double Distance(Vector3 a, Vector3 b) => Math.Sqrt(Math.Pow((a.X - b.X), 2) + Math.Pow((a.Y - b.Y), 2) + Math.Pow((a.Z - b.Z), 2));
        public static double DotProduct(Vector3 a, Vector3 b) => a.Length * b.Length * Math.Cos(Angle(a, b));
        public static Angle Angle(Vector3 a, Vector3 b)
        {
            return Math.Acos((a.X * b.X + a.Y * b.Y + a.Z * b.Z) / (a.Length * b.Length));
        }

        //override//
        public override string ToString()
        {
            return X + ":" + Y + ":" + Z;
        }
        //convertor//
        public Polar3 ToPolar()
        {
            return ToPolar(this);
        }
        public static Polar3 ToPolar(Vector3 a)
        {
            return new Polar3(Math.Sqrt(a.X * a.X + a.Y * a.Y + a.Z * a.Z), Math.Atan2(a.Y, a.X), Math.Acos(a.Z / Math.Sqrt(a.X * a.X + a.Y * a.Y + a.Z * a.Z)));
        }
    }
}