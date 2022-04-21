using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Weather.Core.Data;

namespace Weather.Infrastructure.Database
{
    public class DbClient : IDbClient
    {
        private readonly IMongoCollection<TemperatureToDb> _temperature;
        private readonly IMongoCollection<CityToDb> _cities;

      
        public DbClient(IOptions<WeatherDbConfig> bookstoreDbConfig)
        {
            var client = new MongoClient(bookstoreDbConfig.Value.Connection_String);
            var database = client.GetDatabase(bookstoreDbConfig.Value.Database_Name);
            _temperature = database.GetCollection<TemperatureToDb>(bookstoreDbConfig.Value.Weather_Collection_Name);
            _cities = database.GetCollection<CityToDb>(bookstoreDbConfig.Value.City_Collection_Name);
        }

        public IMongoCollection<CityToDb> GetCityCollection() => _cities;

        public IMongoCollection<TemperatureToDb> GetTemperatureCollection() => _temperature;

    }

}
