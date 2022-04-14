using System.Collections.Generic;

namespace Weather.Core.Parser
{
    interface IParser
    {
        List<City> GetPopularCities();

        List<WeatherForDay> GetWeatherForTenDays(City city);
    }
}
