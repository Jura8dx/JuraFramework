using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMath
{
    public static class Mathf
    {
        public static double Lerp(double a, double b, double t)
        {
            return a + (b - a) * t;
        }

        public static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }
    }
}
