using SQLite;

namespace WeatherApp
{
    public class City
    {
        public City()
        {

        }
        public City(string id, string name)
        {
            CityId = id;
            CityName = name;       
        }
        [PrimaryKey]
        public string CityId { get; set; }

        public string CityName { get; set; }
    }
}
