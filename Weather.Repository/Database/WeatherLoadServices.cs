using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.Core.Data;
using Weather.Core.Parser;

namespace Weather.Infrastructure.Database
{
    public class WeatherLoadServices : ILoaderData<WeatherForDayShort>
    {
        private readonly IMongoCollection<TemperatureToDb> _temperature;
        private readonly IMongoCollection<CityToDb> _cities;

        public WeatherLoadServices(IDbClient dbClient)
        {
            _temperature = dbClient.GetTemperatureCollection();
            _cities = dbClient.GetCityCollection();
        }

        private bool HasNeedUpdate(string cityName) =>  _cities.AsQueryable().FirstOrDefault(c => c.Name == cityName) is not null;

        public async Task LoadDataWeather(string cityName, ICollection<WeatherForDayShort> weather)
        {
            if (HasNeedUpdate(cityName))
            {
                ObjectId objectIdNew = ObjectId.Empty;
                foreach (var weatherDay in weather)
                {
                    var updateWeather = Builders<TemperatureToDb>.Update.Set(w => w.TemperatureTo, weatherDay.TemperatureTo)
                                                                     .Set(w => w.TemperatureFrom, weatherDay.TemperatureFrom)
                                                                     .Set(w => w.Day, weatherDay.Day);


                    var filterWeather = Builders<TemperatureToDb>.Filter.And(
                    Builders<TemperatureToDb>.Filter.In(w => w.Id, _cities.AsQueryable().FirstOrDefault(c => c.Name == cityName)?.TemperatureInfo),
                    Builders<TemperatureToDb>.Filter.Eq(w => w.Day, weatherDay.Day));

                    var updateone = _temperature.UpdateOne(filterWeather, updateWeather, new UpdateOptions { IsUpsert = true });
                    
                    if (updateone.UpsertedId is not null && ObjectId.TryParse(updateone.UpsertedId.ToString(), out objectIdNew))
                    {         
                        var filterCity = Builders<CityToDb>.Filter.Eq(c => c.Name, cityName);
                        var updateCity = Builders<CityToDb>.Update.Push(c => c.TemperatureInfo, objectIdNew);
                        _cities.UpdateOne(filterCity, updateCity);
                    }

                }

            }
            else
            {
                var temp = weather.Select(w => new TemperatureToDb { Day = w.Day, TemperatureFrom = w.TemperatureFrom, TemperatureTo = w.TemperatureTo }).ToList();

                await _temperature.InsertManyAsync(temp); 

                await _cities.InsertOneAsync(new CityToDb { Name = cityName, TemperatureInfo = temp.Select(w => w.Id).ToList() });
            }

        }
    }
}
