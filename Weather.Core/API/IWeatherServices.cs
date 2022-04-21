using System;
using System.Collections.Generic;

namespace Weather.Core.API
{
    public interface IWeatherServices
    {
        Temperature GetTemperature(string cityId , DateTime dateTime);
        List<City> GetCities();
    }
}
