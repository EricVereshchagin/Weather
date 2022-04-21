using System.Timers;
using System;
using Weather.Core.Parser;
using Weather.Infrastructure.Database;
using System.Threading.Tasks;
using Weather.Autofac;
using Autofac;
using Weather.Core.Data;

namespace Weather.Parser.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            AutofucConfig.ConfigureContainer();
            ParsingAndLoad();
            RepeatParsing(60 * 60 * 1000); // 1hour;
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
            ParsingAndLoad();
        }

        static void ParsingAndLoad()
        {
            var client = AutofucConfig.Container.Resolve<IDbClient>();
            var parser = AutofucConfig.Container.Resolve<IParser>();
            var loadService = AutofucConfig.Container.Resolve<ILoaderData<WeatherForDayShort>>();
            var cities = parser.GetCities();

            Parallel.ForEach(cities, async (city) =>
            {
                await loadService.LoadDataWeather(city.Name, parser.GetWeatherForDays(city));
            });
        }
    }
}
