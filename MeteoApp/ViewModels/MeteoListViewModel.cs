using System;
using System.Collections.ObjectModel;

using Plugin.Geolocator;

namespace MeteoApp
{
    public class MeteoListViewModel : BaseViewModel
    {
         ObservableCollection<Entry> _entries;

         public   ObservableCollection<Entry> Entries
        {
            get { return _entries; }
            set
            {
                _entries = value;
                OnPropertyChanged();
            }
        }

        public MeteoListViewModel()
        {
            Entries = new ObservableCollection<Entry>();

            //GetLocation(); NON VA PERCHÈ ???

        }


        async void GetLocation()
        {
            var locator = CrossGeolocator.Current;

            // One position
            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));

            Entries = new ObservableCollection<Entry>();

            Entries.Add(new Entry
            {
                ID = 0,
                Name = "Current Location",
                Lat = position.Latitude,
                Lon = position.Longitude
            });

        }

    }


}

