using System.Threading.Tasks;
using System.Collections.Generic;

namespace Weather.Core.Data
{
    public interface ILoaderData<T>
    {
        Task LoadDataWeather(string cityName, ICollection<T> weather);
    }
}
