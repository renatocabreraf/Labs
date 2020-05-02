using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab6_EDII
{
    public class DiffieHellman
    {
        int a { get; set; }
        int b { get; set; }
        int g = 43;
        int p = 107;

        public DiffieHellman(int a, int b)
        {
            this.a = a;
            this.b = b;
        }

        double mod(double a, double n)
        {
            double result = a % n;
            if ((result < 0 && n > 0) || (result > 0 && n < 0))
            {
                result += n;
            }
            return result;
        }

        public int generateKey()
        {
            long auxA = (long)Math.Pow(g, a);
            long auxB = (long)Math.Pow(g, b);

            double A = mod(auxA, p);
            double B = mod(auxB, p);
            double Ka = mod(Math.Pow(B, a), p);
            double Kb = mod(Math.Pow(A, b), p);

            if (Ka == Kb)
            {
                return (int)Ka;
            }
            return 0;
        }
    }
}
