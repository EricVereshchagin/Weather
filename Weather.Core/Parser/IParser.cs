using System.Collections.Generic;

namespace Weather.Core.Parser
{
    public interface IParser
    {
        public List<City> GetCities();

        public List<WeatherForDayShort> GetWeatherForDays(City city);
    }
}
