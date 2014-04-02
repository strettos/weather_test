using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WeatherApp.Resources;

namespace WeatherApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
            CityList.ItemsSource = App.cityList;
        }
        private void CityList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CityList.SelectedIndex != -1)
            {
                City selCity = (City)CityList.SelectedItem;
                this.NavigationService.Navigate(new Uri("/CityWeatherPage.xaml?City=" +
                    selCity.CityName + "&Id=" + selCity.CityId, UriKind.Relative));
            }

        }

    }
}