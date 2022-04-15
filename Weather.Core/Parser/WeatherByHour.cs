using System;

namespace Weather.Core.Parser
{
    public class WeatherByHour
    {
        public string IconSvg { get; set; }

        public TimeSpan Time { get; set; }
        public double Temperature { get; set;}
        public string SpeedWind { get; set; }
        public double AmountPrecipitation { get; set; }
    }
}
