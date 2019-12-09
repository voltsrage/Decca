using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DECCA2
{
    public class PropEfficiency
    {
        
        
        public static void PropEff(double BHP,Double V, Double rho,Double Dp, Double Nv)
        {
           
            Double A, Np, Ni, Npnew, w, T, Delta;
            
            A = Math.PI * Math.Pow(Dp, 2) / 4;

            Np = 0.5;

            if (V == 0)
            {
                GAAircraftDesign.PropEffi= 0;
            }

            do
            {
                T = Np * BHP * 550 / V;
                w = 0.5 * (Math.Sqrt(Math.Pow(V, 2) + 2 * T / (rho * A)) - V);
                Ni = 1 / (1 + w / V);
                Npnew = Nv * Ni;
                Delta = Math.Abs(Np - Npnew);
                Np = Npnew;
            } while (Delta < 0.0001);

            GAAircraftDesign.PropEffi = Math.Round(Np,2);
        }
    }
}
