using System.Collections.Generic;

namespace Weather.Core.Parser
{
    public interface IParser
    {
        public List<City> GetPopularCities();

        public List<WeatherForDay> GetWeatherForTenDays(City city);
    }
}
