﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DECCA2
{
    public class WeatherForecast
    {
        public city city { get; set; }
        public List<list> list { get; set; }
    }

    public class temp
    {
        public double day { get; set; }
    }

    public class weather
    {
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class main
    {
        public double temp { get; set; }
    }

    public class city
    {
        public string name { get; set; }
    }

    public class wind
    {
        public double speed { get; set; }
    }

    public class list
    {
        public double dt { get; set; }
        public double pressure { get; set; }
        public double humidity { get; set; }
        public wind wind { get; set; }
        public main main { get; set; }
        public List<weather> weather { get; set; }
    }


}
