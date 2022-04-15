using System;
using System.Collections.Generic;
using System.Linq;
using Weather.Core.Parser;
using HtmlAgilityPack;


namespace Weather.Parser.Gismeteo
{
    public class ParserGismeteo : IParser
    {
        private readonly static string[] _daysForLink = { "", "tomorrow", "3-day", "4-day", "5-day", "6-day", "7-day", "8-day", "9-day", "10-day" };
        private readonly static string _mainURL = @"https://www.gismeteo.ru/";
        public List<City> GetPopularCities()
        {
            
            var htmlDoc = new HtmlWeb().Load(_mainURL);

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

        private double TextTempToDouble(string text) => 
            text.Contains("&minus;") ? double.Parse(text.Split("&minus;").LastOrDefault()) * -1 
                                     : double.Parse(text.Split("+").LastOrDefault());
         

        public List<WeatherForDayShort> GetShortWeatherForTenDays(City city)
        {
            var nowTime = DateTime.Today;
            List<WeatherForDayShort> weatherForTenDays = new List<WeatherForDayShort>();
            foreach (string dayForLink in _daysForLink)
            {
                var htmlDoc = new HtmlWeb().Load(_mainURL + city.URL + dayForLink);
                var htmlWeatherShortInfo = htmlDoc?.DocumentNode.SelectNodes("/html/body/section[2]/div[1]/section[2]/div/a[2]/div/div[1]/div");

                var temperFrom = htmlWeatherShortInfo.LastOrDefault()
                                                     .FirstChild
                                                     .FirstChild
                                                     .FirstChild
                                                     .FirstChild
                                                     .InnerText;

                var temperTo = htmlWeatherShortInfo.LastOrDefault()
                                                     .FirstChild
                                                     .FirstChild
                                                     .LastChild
                                                     .FirstChild
                                                     .InnerText;

                weatherForTenDays.Add(new WeatherForDayShort()
                {
                    TemperatureFrom = TextTempToDouble(temperFrom),
                    TemperatureTo = TextTempToDouble(temperTo),
                    Day = nowTime
                });

                nowTime = nowTime.AddDays(1);
            }

            return weatherForTenDays;
        }

    }
}
