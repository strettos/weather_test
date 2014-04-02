using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace WeatherApp.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        // Note the use of the CallerMemberName attribute.
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (Object.Equals(field, value))
                return false;

            field = value;
            RaisePropertyChanged(propertyName);
            return true;
        }

        protected void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        int busyLoading = 0;
        ManualResetEvent dataLoadedEvent = new ManualResetEvent(false);


        protected void InitializeOnceOrWait(Action action)
        {
            // Make sure we only load once
            int alreadyLoading = Interlocked.CompareExchange(ref busyLoading, 1, 0);
            if (alreadyLoading == 1)
            {
                dataLoadedEvent.WaitOne();
                return;
            }

            action();

            dataLoadedEvent.Set();
        }
    }
}
