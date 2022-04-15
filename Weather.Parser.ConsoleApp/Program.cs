using System.Timers;
using Weather.Parser.Gismeteo;
using System;

namespace Weather.Parser.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Parsing();
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
            Parsing();
        }
        static void Parsing()
        {
            ParserGismeteo test = new ParserGismeteo();// TO DO DI 
            var cities = test.GetPopularCities();
            foreach (var city in cities)
            {
                var weatherForTenDays = test.GetWeatherForDays(city);
            }
            
        }
    }
}
