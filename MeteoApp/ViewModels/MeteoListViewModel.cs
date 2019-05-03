using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Plugin.Geolocator;
using System.Net.Http;
using Newtonsoft.Json.Linq;

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

         public   MeteoListViewModel()
        {
            Entries = new ObservableCollection<Entry>();
            Entries.Add(new Entry { ID = 0, Name = "Localizzazione in corso" });
            foreach (Entry a in App.Database.GetItemsAsync().Result) {
                Entries.Add(a);
            }

            GetLocation(); 
        }


        async void GetLocation()
        {
            var locator = CrossGeolocator.Current;

            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));

            Task<Entry> task = GetWeatherAsyncLatLong(position.Latitude, position.Longitude);
            var appogio = await task;

            Entries.RemoveAt(0);
            Entries.Insert(0, appogio);
        }


        public async Task<Entry> GetWeatherAsyncLatLong(double lat, double lon)
        {
            var httpClient = new HttpClient();
            Task<string> contentsTask = httpClient.GetStringAsync("https://api.openweathermap.org/data/2.5/weather?lat=" + lat + "&lon=" + lon + "&units=metric&appid=c200173e4aeed3198803206f96382afe");
            Console.WriteLine(contentsTask);
            var content = await contentsTask; 
            var Appoggio = new Entry
            {
                Lat = (double)JObject.Parse(content)["coord"]["lat"],
                Lon = (double)JObject.Parse(content)["coord"]["lon"],
                Description = (string)JObject.Parse(content)["weather"][0]["description"],
                icon = "http://openweathermap.org/img/w/" + (string)JObject.Parse(content)["weather"][0]["icon"] + ".png",
                ActualTemperature = (double)JObject.Parse(content)["main"]["temp"],
                MinTemperature = (double)JObject.Parse(content)["main"]["temp_min"],
                MaxTemperature = (double)JObject.Parse(content)["main"]["temp_max"],
                Name = (string)JObject.Parse(content)["name"],
                State = (string)JObject.Parse(content)["sys"]["country"]
            };
            return Appoggio;
        }

    }


}

