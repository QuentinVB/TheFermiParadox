using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helpers
{
    public enum AngleUnit
    {
        degree,
        radian
    }
    public struct Angle
    {
        double angleValue;
        AngleUnit unit;

        public Angle(double angleValue, AngleUnit unit)
        {
            this.angleValue = angleValue;
            this.unit = unit;
        }
        public double AngleValue { get => angleValue; set => angleValue = value; }
        public AngleUnit Unit { get => unit;}
        //operator//
        public static Angle operator +(Angle a, Angle b)
        {
            if(a.Unit==AngleUnit.degree && b.Unit == AngleUnit.degree)
            {
                return new Angle((a.angleValue + b.angleValue)%360,AngleUnit.degree);
            }
            else if (a.Unit == AngleUnit.radian && b.Unit == AngleUnit.radian)
            {
                return new Angle((a.angleValue + b.angleValue) % (2*Math.PI), AngleUnit.radian);
            }
            //TODO different unit !
            else
            {
                throw new ArgumentException("Both angle must be on same AngleUnit");
            }
        }
        public static Angle operator -(Angle a, Angle b)
        {
            if (a.Unit == AngleUnit.degree && b.Unit == AngleUnit.degree)
            {
                return new Angle((a.angleValue - b.angleValue) % 360, AngleUnit.degree);
            }
            else if (a.Unit == AngleUnit.radian && b.Unit == AngleUnit.radian)
            {
                return new Angle((a.angleValue - b.angleValue) % (2 * Math.PI), AngleUnit.radian);
            }
            else
            {
                throw new ArgumentException("Both angle must be on same AngleUnit");
            }
        }
        public static Angle operator *(Angle a, double q)
        {
            if (a.Unit == AngleUnit.degree)
            {
                return new Angle((a.angleValue * q) % 360, AngleUnit.degree);
            }
            else if (a.Unit == AngleUnit.radian)
            {
                return new Angle((a.angleValue * q) % (2 * Math.PI), AngleUnit.radian);
            }
            else
            {
                throw new ArgumentException("invalid");
            }
        }
        public static Angle operator /(Angle a, double q)
        {
            if (q == 0) throw new DivideByZeroException();
            if (a.Unit == AngleUnit.degree)
            {
                return new Angle((a.angleValue / q) % 360, AngleUnit.degree);
            }
            else if (a.Unit == AngleUnit.radian)
            {
                return new Angle((a.angleValue / q) % (2 * Math.PI), AngleUnit.radian);
            }
            else
            {
                throw new ArgumentException("invalid");
            }
        } 
        //converters//
        public Angle ToRadian()
        {
            if (this.Unit == AngleUnit.degree)
                return new Angle(angleValue*(Math.PI/180), AngleUnit.radian);
            else
                return this;
        }
        public Angle ToDegree()
        {
            if (this.Unit == AngleUnit.radian)
                return new Angle(angleValue * (180 / Math.PI  ), AngleUnit.degree);
            else
                return this;
        }
        /// <summary>
        /// User-defined conversion from Angle to double
        /// </summary>
        /// <param name="a">angle</param>
        public static implicit operator double(Angle a)
        {
            return a.AngleValue;
        }
        /// <summary>
        /// User-defined conversion from double to Angle, unit is radian by default
        /// </summary>
        public static implicit operator Angle(double d)
        {
            return new Angle(d,AngleUnit.radian);
        }
        public override string ToString()
        {
            return AngleValue.ToString();
        }
    }
}
