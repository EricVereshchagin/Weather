using System;
using System.Collections.Generic;

namespace Weather.Core.Parser
{
    class WeatherForDay
    {
        public DateTime Day { get; set; }
        public SortedDictionary<TimeSpan, WeatherByHour> WeatherByHours { get; set; }
    }
}
