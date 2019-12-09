using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DECCA2
{
    public class AircraftMissionType
    {
        public static void TwinEngine()
        {
            Missionstats twinEng = new Missionstats();
            twinEng.W1_W0 = 0.992;
            twinEng.W2_W1 = 0.996;
            twinEng.W3_W2 = 0.996;
            twinEng.W4_W3 = 0.998;
            twinEng.W7_W6 = 0.992;
            twinEng.W8_W7 = 0.992;
        }

        public static void BusinessJet()
        {
            Missionstats bizJet = new Missionstats();
            bizJet.W1_W0 = 0.990;
            bizJet.W2_W1 = 0.995;
            bizJet.W3_W2 = 0.995;
            bizJet.W4_W3 = 0.980;
            bizJet.W7_W6 = 0.990;
            bizJet.W8_W7 = 0.992;
        }

        public static void SingleEngine()
        {
            Missionstats sinEng = new Missionstats();
            sinEng.W1_W0 = 0.995;
            sinEng.W2_W1 = 0.997;
            sinEng.W3_W2 = 0.998;
            sinEng.W4_W3 = 0.992;
            sinEng.W7_W6 = 0.993;
            sinEng.W8_W7 = 0.993;
        }

        public static void TransportJet()
        {
            Missionstats transJet = new Missionstats();
            transJet.W1_W0 = 0.990;
            transJet.W2_W1 = 0.990;
            transJet.W3_W2 = 0.995;
            transJet.W4_W3 = 0.980;
            transJet.W7_W6 = 0.990;
            transJet.W8_W7 = 0.992;            
        }
    }
}
