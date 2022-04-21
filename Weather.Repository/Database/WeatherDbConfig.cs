namespace Weather.Infrastructure.Database
{
    public class WeatherDbConfig
    {
        public string Database_Name { get; set; }
        public string Weather_Collection_Name { get; set; }
        public string City_Collection_Name { get; set; }
        public string Connection_String { get; set; }
    }
}
