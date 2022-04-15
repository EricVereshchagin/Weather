using System;

namespace Weather.Core.Parser
{
    public class WeatherForDayShort
    {
        public DateTime Day { get; set; }
        public double TemperatureFrom { get; set; }
        public double TemperatureTo { get; set; }
    }
}
