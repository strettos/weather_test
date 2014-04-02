using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using System.Collections.ObjectModel;
namespace WeatherApp
{  
    public class WeatherDB
    {
        private string dbpath = string.Empty;
        private List<City> defaultCities = new List<City>
        {
            new City("524901","Moscow"),
            new City("498817","Saint Petersburg")
        };
        public WeatherDB()
        {
            dbpath = new SQLiteConnectionString("Weather.db", false).DatabasePath;
        }
        private void AddDefault()
        {
            foreach (var city in defaultCities)
            {
                AddCity(city);
            }

        }
        private bool ExistsTable<T>()
        {
            using (var conn = new SQLiteConnection(dbpath))
            {
                var command = new SQLiteCommand(conn)
                {
                    CommandText =
                        "SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='" +
                        typeof(T).Name + "'"
                };
                return (command.ExecuteScalar<int>() > 0);
            }
        }
        private bool CreateTable(string table)
        {
            using (var conn = new SQLiteConnection(dbpath))
            {
                if (table == "City")
                {
                    var command = new SQLiteCommand(conn)
                    {
                        CommandText = "CREATE TABLE 'City'('CityId' varchar(140)," +
                            "'CityName' varchar(140), "+
                            "PRIMARY KEY('CityId'))"
                    };
                    return (command.ExecuteScalar<int>() > 0);
                }
            }
            return false;
        }
        public int AddCity(City newCity)
        {

            using (var conn = new SQLiteConnection(dbpath))
            {
                try
                {
                    conn.Insert(newCity);
                }
                catch
                {
                    return 1;
                }
            }
            return 0;
        }
        public void GetCities(ObservableCollection<City> cityList)
        {
            using (var conn = new SQLiteConnection(dbpath))
            {
                var data = conn.Query<City>("SELECT * FROM City");
                if (null != data)
                {
                    foreach (var city in data)
                    {
                        cityList.Add(city);
                    }
                }
            }
        
        }
        public void CreateDatabase()
        {
            using (var conn = new SQLiteConnection(dbpath))
            {
                if (!ExistsTable<City>())
                {
                    CreateTable("City");
                    AddDefault();
                };
            }
        }
    }
}
