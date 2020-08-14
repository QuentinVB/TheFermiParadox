using System;
using System.Collections.Generic;
using System.Text;

//https://docs.microsoft.com/fr-fr/dotnet/api/system.numerics.vector3?view=netframework-4.7.2

namespace theFermiParadox.Core
{

    public class Physic
    {
        //CONSTANTS
        public const double GravitationalConstant = 6.673e-11;//N⋅m2⋅kg-2
        public const double SolarMass = 1.9884e30;//kg
        public const double SolarRadius = 6.957e8;//m
        //earth mass
        //earth radius
        public const double SolarLumniosity = 3.828e26; //W
        public const double StefanBoltzmanConstant = 5.670374e-8;//W.m-2.K-4
        public const double AstronomicUnit = 1.49597870e11;//m
        public const double LightSpeed = 299792458;//m.s-1
        public const double LightYear = LightSpeed * 365 * 24 * 60 * 60; //m
        public static string[] GREEKALPHABET = new string[24] { "Alpha", "Beta", "Gamma", "Delta", "Epsilon", "Zeta", "Eta", "Theta", "Iota", "Kappa", "Lambda", "Mu", "Nu", "Xi", "Omicron", "Pi", "Rho", "Sigma", "Tau", "Upsilon", "Phi", "Chi", "Psi", "Omega" };
        public static string STARLETTER = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static double GravitationnalForce(double massA, double massB, double distance) => GravitationalConstant * (massA * massB) / (distance * distance);

        public static BasicColor ColorTemperatureToRGB(int kelvin)
        {
            double temp = kelvin / 100;
            double red;
            double green;
            double blue; ;
            if (temp <= 66)
            {
                red = 255;
                green = temp;
                green = 99.4708025861 * Math.Log(green) - 161.1195681661;
                if (temp <= 19)
                {
                    blue = 0;
                }
                else
                {
                    blue = temp - 10;
                    blue = 138.5177312231 * Math.Log(blue) - 305.0447927307;
                }
            }
            else
            {
                red = temp - 60;
                red = 329.698727446 * Math.Pow(red, -0.1332047592);
                green = temp - 60;
                green = 288.1221695283 * Math.Pow(green, -0.0755148492);
                blue = 255;
            }
            return new BasicColor(
                Clamp((int)red, 0, 255),
                Clamp((int)green, 0, 255),
                Clamp((int)blue, 0, 255)
                );
        }
        public static int Clamp(int x, int min, int max)
        {
            if (x < min) { return min; }
            if (x > max) { return max; }
            return x;
        }
        public static float Clamp(float x, float min, float max)
        {
            if (x < min) { return min; }
            if (x > max) { return max; }
            return x;
        }

        public static string LatinNumber(int number)
        {
            switch (number)
            {
                case 0: return "-";
                case 1: return "I";
                case 2: return "II";
                case 3: return "III";
                case 4: return "IV";
                case 5: return "V";
                case 6: return "VI";
                case 7: return "VII";
                case 8: return "VIII";
                case 9: return "IX";
                case 10: return "X";
                default: return "";
            }
        }

        public static string StarLetter(int index)
        {
            //TODO : add safety and maj/min ctrl
            return STARLETTER[index].ToString();
        }
    }


}
