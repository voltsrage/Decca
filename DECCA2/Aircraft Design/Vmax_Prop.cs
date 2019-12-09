using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DECCA2
{
    public class Vmax_Prop
    {
        public static Double Perf_Vmax_Prop(Double S,Double K,Double CDmin,Double W, Double BHP, Double eta)
        {
            Double Counter, V0, V1, Vmid, F0, Fmid;
            Byte flag;            

            Counter = 0;
            V0 = 0;
            V1 = 500;
            F0 = -1100 * eta * BHP;

            do
            {
                flag = 1;
                Counter += 1;
                Vmid = 0.5 * (V0 + V1);

                Fmid = PerfOfV.Perf_f_of_V(S, CDmin, K, W, BHP, eta, Vmid);                

                if (F0 * Fmid < 0)
                {
                    V1 = Vmid;
                }
                else
                {
                    V0 = Vmid;
                    F0 = Fmid;
                }

                if (Math.Abs(V1 - V0) < 0.0001)
                {
                    flag = 0;
                    break;
                }
                
            } while (flag != 0 || Counter != 100);

            Double PERF_Vmax_Prop = Math.Round(0.5 * (V0 + V1),2);

            if (Counter == 100)
            {
                PERF_Vmax_Prop = -1;
            }

            return PERF_Vmax_Prop;
        }
    }
}
