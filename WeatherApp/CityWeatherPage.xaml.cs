using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace WeatherApp
{
    public partial class CityWeatherPage : PhoneApplicationPage
    {
        WeatherRequest wRequest;
        public CityWeatherPage()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string Name = this.NavigationContext.QueryString["City"];
            string Id = this.NavigationContext.QueryString["Id"];

            wRequest = new WeatherRequest();

            wRequest.GetWeather(Id);

            DataContext = wRequest;

            WList.ItemsSource = wRequest.WeatherList;

        }
    }
}