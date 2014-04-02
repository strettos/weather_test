using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using WeatherApp.Data;
using System.Collections.ObjectModel;
using System.Windows;

namespace WeatherApp
{
    public class WeatherRequest
    {
        public ObservableCollection<WeatherData> WeatherList
        {
            get;
            set;
        }
        public WeatherRequest()
        {
            WeatherList = new ObservableCollection<WeatherData>();
        }

        public void GetWeather(string Id)
        {

            var requestUri = new Uri(string.Format("http://api.openweathermap.org/data/2.5/forecast/daily?id={0}", Id));
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.CreateHttp(requestUri);
                request.BeginGetResponse(new AsyncCallback(GetWeatherCallback), request);

            }
            catch (System.Exception ex)
            {

            }
        }
        void GetWeatherCallback(IAsyncResult asynchronousResult)
        {
            try
            {
                var request = (HttpWebRequest)asynchronousResult.AsyncState;

                using (var resp = (HttpWebResponse)request.EndGetResponse(asynchronousResult))
                {
                    using (var streamResponse = resp.GetResponseStream())
                    {
                        using (var streamRead = new StreamReader(streamResponse))
                        {
                            JsonTextReader reader = new JsonTextReader(streamRead);
                            ObservableCollection<WeatherData> tmpList =
                                new ObservableCollection<WeatherData>();
                            while (reader.Read())
                            {
                                if (null != reader.Value && reader.Value.Equals("dt"))
                                {
                                    tmpList.Add(new WeatherData());                               
                                    reader.Read();
                                    System.DateTime dtDateTime = new DateTime(1970,1,1,0,0,0,0,System.DateTimeKind.Utc);
                                    dtDateTime = dtDateTime.AddSeconds(Convert.ToInt32(reader.Value.ToString())).ToLocalTime();
                                    tmpList.ElementAt(tmpList.Count - 1).Day = dtDateTime.Date.ToShortDateString();
                                }
                                else if (null != reader.Value && reader.Value.Equals("day"))
                                {
                                    reader.Read();
                                    tmpList.ElementAt(tmpList.Count - 1).TempDay = Convert.ToString(Convert.ToInt32(Convert.ToDouble(reader.Value.ToString())-273.15));
                                }
                                else if (null != reader.Value && reader.Value.Equals("night"))
                                {
                                    reader.Read();
                                    tmpList.ElementAt(tmpList.Count - 1).TempNight = Convert.ToString(Convert.ToInt32(Convert.ToDouble(reader.Value.ToString()) - 273.15));
                                }
                                else if (null != reader.Value && reader.Value.Equals("main"))
                                {
                                    reader.Read();
                                    tmpList.ElementAt(tmpList.Count - 1).Main = (string)reader.Value.ToString();
                                }
                                else if (null != reader.Value && reader.Value.Equals("clouds"))
                                {
                                    reader.Read();
                                    tmpList.ElementAt(tmpList.Count - 1).Cloud = (string)reader.Value.ToString();
                                }
                                    
                            }
                            Deployment.Current.Dispatcher.BeginInvoke(() =>
                            {
                                WeatherList.Clear();
                                foreach (var elem in tmpList)
                                {
                                    WeatherList.Add(elem);
                                }
                            });
                        }

                    }
                }
            }
            catch (WebException ex)
            {

            }

        }
    }
}
