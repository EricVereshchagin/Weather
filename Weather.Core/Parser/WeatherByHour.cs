using System;

namespace Weather.Core.Parser
{
    public class WeatherByHour
    {
        public TimeSpan Time { get; set; }
        public double Temperature { get; set;}
        public string SpeedWind { get; set; }
        public double AmountPrecipitation { get; set; }
    }
}
