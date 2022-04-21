using MongoDB.Driver;
using System;
using System.Collections.Generic;
using Weather.Core.API;
using Weather.Core.Data;
using System.Linq;
using MongoDB.Bson;

namespace Weather.Infrastructure.Database
{
    public class WeatherAPIServices : IWeatherServices
    {
        private readonly IMongoCollection<TemperatureToDb> _temperature;
        private readonly IMongoCollection<CityToDb> _cities;


        public WeatherAPIServices(IDbClient dbClient)
        {
            _temperature = dbClient.GetTemperatureCollection();
            _cities = dbClient.GetCityCollection();
        }

        public List<City> GetCities() => _cities.AsQueryable().Select(c => new City {Id = c.Id.ToString(),  Name = c.Name}).ToList();
        
        public Temperature GetTemperature(string cityId, DateTime dateTime)
        {
            var filterWeather = Builders<TemperatureToDb>.Filter.And(
                   Builders<TemperatureToDb>.Filter.In(w => w.Id, _cities.AsQueryable().FirstOrDefault(c => c.Id == ObjectId.Parse(cityId))?.TemperatureInfo),
                   Builders<TemperatureToDb>.Filter.Eq(w => w.Day, DateTime.SpecifyKind(dateTime, DateTimeKind.Utc)));
           var temp = _temperature.Find(filterWeather)?.FirstOrDefault();
            return temp is not null ? new Temperature {TemperatureFrom = temp.TemperatureFrom, TemperatureTo = temp.TemperatureTo } : null; 
        }
    }
}
