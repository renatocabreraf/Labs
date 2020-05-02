using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab6_EDII
{
    public class RSA
    {
        //p y q como parámetro en generateKey()
        // d, N -> private
        // e, N -> public 

        int p { get; set; }
        int q { get; set; }

        public RSA(int p, int q)
        {
            this.p = p;
            this.q = q;
        }

        long getN(int p, int q)
        {
            return p * q;
        }

        long getPhi(int p, int q)
        {
            return (p - 1) * (q - 1);
        }


        int calcularE(int phi)
        {
            //sacar el MCD de phi de n y e
            int e = 0;
            for (int i = 2; i < phi; i++)
            {
                e = MCD(i, phi);
                if (e == 1)            
                    break;
            }
            return e;
        }

        int calcularD(int e, int phi)
        {

            double aux1;
            double d = 0;

            for (double i = 1; i < e + 1; i++)
            {
                aux1 = (i * phi) + 1;
                d = aux1 / e;
                if (d - (int)d == 0)
                {
                    break;
                }
            }

            return (int)d;

        }


        public int MCD(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a == 0 ? b : a;
        }

        double Cifrar(int claveSimetrico, int d, int n)
        {

            return Math.Pow(claveSimetrico, d) % n;
            
        }
        

        double Descifrar(int cifradoAsimetrico, int e, int n)
        {
            return Math.Pow(cifradoAsimetrico, e) % n;
        }
    }
}
