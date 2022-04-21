using System.Timers;
using Weather.Parser.Gismeteo;
using System;
using System.Collections.Generic;
using Weather.Core.Parser;
using Weather.Infrastructure.Database;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Weather.Parser.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //DbClient client = new DbClient(new WeatherDbConfig());

            //ParsingAndLoad(client);
            //RepeatParsing(60 * 60 * 1000); // 1hour;
            Console.ReadLine();
            
        }

        static void RepeatParsing(double interval)
        {
            Timer timer = new Timer(interval);
            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            timer.Start();
        }

        static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            //ParsingAndLoad();
        }

        static void ParsingAndLoad(DbClient client)
        {
            ParserGismeteo test = new ParserGismeteo();
            var loadService = new WeatherLoadServices(client);

            var cities = test.GetCities();
            Parallel.ForEach(cities, async (city) =>
            {
                await loadService.LoadDataWeather(city.Name, test.GetWeatherForDays(city));
            });
        }
    }
}
