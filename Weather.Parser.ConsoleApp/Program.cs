using System;
using Weather.Parser.Gismeteo;
using Weather.Core.Parser;

namespace Weather.Parser.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Start();
        }

        static void Start()
        {
            ParserGismeteo test = new ParserGismeteo();// TO DO DI // репозиторий // какой-то тамер посмотреть
            var cities = test.GetPopularCities();
            foreach (var city in cities)
            {
                var weatherForTenDays = test.GetShortWeatherForTenDays(city);

            }
            
        }
    }
}
