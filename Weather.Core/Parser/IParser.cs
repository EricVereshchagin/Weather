using System.Collections.Generic;

namespace Weather.Core.Parser
{
    public interface IParser<T>
    {
        public List<City> GetPopularCities();

        public List<T> GetWeatherForDays(City city);
    }
}
