using System.Collections.Generic;

namespace Weather.Core.Parser
{
    public interface IParser
    {
        public List<City> GetPopularCities();

        public List<WeatherForDayShort> GetShortWeatherForTenDays(City city);
    }
}
