using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Weather.Core.Data;

namespace Weather.Infrastructure.Database
{
    public class DbClient : IDbClient
    {
        private readonly IMongoCollection<TemperatureToDb> _temperature;
        private readonly IMongoCollection<CityToDb> _cities;

      
        public DbClient(IOptions<WeatherDbConfig> DbConfig)
        {
            var client = new MongoClient(DbConfig.Value.Connection_String);
            var database = client.GetDatabase(DbConfig.Value.Database_Name);
            _temperature = database.GetCollection<TemperatureToDb>(DbConfig.Value.Weather_Collection_Name);
            _cities = database.GetCollection<CityToDb>(DbConfig.Value.City_Collection_Name);
        }

        public IMongoCollection<CityToDb> GetCityCollection() => _cities;

        public IMongoCollection<TemperatureToDb> GetTemperatureCollection() => _temperature;

    }

}
