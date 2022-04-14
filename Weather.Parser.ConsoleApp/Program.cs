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
            ParserGismeteo test = new ParserGismeteo();// TO DO DI 
            var cities = test.GetPopularCities();
        }
    }
}
