using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DECCA2
{
    public class PerfOfV
    {
        public static Double Perf_f_of_V(Double S,Double CDmin,Double K, Double W,Double BHP,Double eta, Double V)
        {
            Double K1;

            K1 = Math.Pow(550 * eta * BHP, 2) - Math.Pow(2 * W * V, 2) * CDmin * K;

            if (K1 < 0)
            {
                K1 = 0;
            }

            Double PERF_f_of_V = Math.Round(AltitudeCalculator.rho_h * 0.00194122449,6) * S * CDmin * Math.Pow(V, 3) - 550 * eta * BHP;
            return PERF_f_of_V = PERF_f_of_V - Math.Sqrt(K1);
        }
    }
}
