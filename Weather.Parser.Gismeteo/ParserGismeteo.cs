using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Weather.Core.Parser;
using HtmlAgilityPack;

namespace Weather.Parser.Gismeteo
{
    public class ParserGismeteo : IParser
    {
        private readonly static string[] _daysForLink = { "", "tomorrow", "3-day", "4-day", "5-day", "6-day", "7-day", "8-day", "9-day", "10-day" };

        public List<City> GetPopularCities()
        {
            
            var htmlDoc = new HtmlWeb().Load(@"https://www.gismeteo.ru/");

            var htmlCityNodes = htmlDoc?.DocumentNode.SelectNodes("/html/body/section/div[1]/section[2]/div/div[2]/div[2]/div/a");

            List<City> popularCities = new List<City>();

            if (htmlCityNodes != null)
            {
                foreach (var htmlCityNode in htmlCityNodes)
                {
                    var cityURL = htmlCityNode.Attributes["href"].Value;
                    if (!string.IsNullOrEmpty(cityURL))
                    {
                        popularCities.Add(new City()
                        {
                            Name = htmlCityNode.InnerText,
                            Id = Guid.NewGuid(),
                            URL = cityURL
                        });
                    }
                }
            }

            return popularCities;
        }

        public List<WeatherForDay> GetWeatherForTenDays(City city)
        {
            throw new NotImplementedException();
        }
    }
}
