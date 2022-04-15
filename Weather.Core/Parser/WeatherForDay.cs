using System;
using System.Collections.Generic;

namespace Weather.Core.Parser
{
    public class WeatherForDay
    {
        public DateTime Day { get; set; }
        public WeatherForDayShort TemperatureForDay { get; set; }
        public SortedDictionary<TimeSpan, WeatherByHour> WeatherByHours { get; set; }
    }
}
