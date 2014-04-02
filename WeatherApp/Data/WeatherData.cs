using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Data
{
    public class WeatherData : INotifyPropertyChanged
    {
        private string day;
        private string temperature_day;
        private string temperature_night;
        private string cloud;
        private string main;

        public event PropertyChangedEventHandler PropertyChanged;

        public WeatherData()
        {

        }
        public string Day
        {
            get
            {
                return day;
            }
            set
            {
                if (value != day)
                {
                    this.day = value;
                    NotifyPropertyChanged("Day");
                }
            }
        }
        public string Main
        {
            get
            {
                return main;
            }
            set
            {
                if (value != main)
                {
                    this.main = value;
                    NotifyPropertyChanged("Main");
                }
            }
        }

        public string TempDay
        {
            get
            {
                return temperature_day;
            }
            set
            {
                if (value != temperature_day)
                {
                    this.temperature_day = value;
                    NotifyPropertyChanged("TempDay");
                }
            }
        }

        public string TempNight
        {
            get
            {
                return temperature_night;
            }
            set
            {
                if (value != temperature_night)
                {
                    this.temperature_night = value;
                    NotifyPropertyChanged("TempNight");
                }
            }
        }

        public string Cloud
        {
            get
            {
                return cloud;
            }
            set
            {
                if (value != cloud)
                {
                    this.cloud = value;
                    NotifyPropertyChanged("Cloud");
                }
            }
        }
        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
