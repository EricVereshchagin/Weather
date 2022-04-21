using MongoDB.Driver;
using Weather.Core.Data;

namespace Weather.Infrastructure.Database
{
    public interface IDbClient
    {
        IMongoCollection<CityToDb> GetCityCollection();
        IMongoCollection<TemperatureToDb> GetTemperatureCollection();
    }

}
