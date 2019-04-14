using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;

namespace MeteoApp
{
    public partial class MeteoListPage : ContentPage
    {
        MeteoListViewModel MeteoListViewModel = new MeteoListViewModel();

        public MeteoListPage()
        {
            InitializeComponent();
            BindingContext = MeteoListViewModel;
        }


        ICollection<Entry> getAllCity() {
            ICollection<Entry> appoggio = new ObservableCollection<Entry>();
            return  null;
        }



         void  OnItemAdded(object sender, EventArgs e)
        {
            // TODO modificare per compilare anche in ANDROID

            //DisplayAlert("Messaggio", "Testo", "OK");
            string input="";
            UIAlertView alert = new UIAlertView();
            alert.Title = "New City";
            alert.AddButton("Add");
            alert.AddButton("Cancel");
            alert.Message = "Please Enter a City";
            alert.AlertViewStyle = UIAlertViewStyle.PlainTextInput;
            alert.Clicked += async (object s, UIButtonEventArgs ev) =>
            {
                if (ev.ButtonIndex == 0)
                {
                     input = alert.GetTextField(0).Text;
                    Task < Entry > task = GetWeatherAsync(input);


                    var appogio = await task;
                   MeteoListViewModel.Entries.Add(appogio);
                }
                //faccio la richiesta ad openCage
               
            };
            alert.Show();

        }

        public async  Task<Entry>  GetWeatherAsync(string Nome)
        {
            var httpClient = new HttpClient();
           Task<string> contentsTask = httpClient.GetStringAsync("https://api.openweathermap.org/data/2.5/weather?q=" + Nome + "&appid=c200173e4aeed3198803206f96382afe");
      
            var content = await contentsTask; 
            var Appoggio = new Entry
            {
                Lat = (double)JObject.Parse(content)["coord"]["lat"],
                Lon = (double)JObject.Parse(content)["coord"]["lon"],
                Description  = (string)JObject.Parse(content)["weather"][0]["description"],
                icon = "http://openweathermap.org/img/w/"+ (string)JObject.Parse(content)["weather"][0]["icon"]+ ".png" ,
                ActualTemperature = (double)JObject.Parse(content)["main"]["temp"],
                MinTemperature =  (double)JObject.Parse(content)["main"]["temp_min"],
                MaxTemperature = (double)JObject.Parse(content)["main"]["temp_max"],
                Name =(string)JObject.Parse(content)["name"],
                State = (string)JObject.Parse(content)["sys"]["country"]
            };
            return Appoggio;
        }



        void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Navigation.PushAsync(new MeteoItemPage()
                {
                    BindingContext = new MeteoItemViewModel(e.SelectedItem as Entry)
                });
            }
        }



        async void OnActionSheetSimpleClicked(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Select City", "Cancel", null, "Choose1", "Choose2", "Choose3");
            Debug.WriteLine("Action: " + action);
        }

        //funzione per visualizzare tutte le città del mondo che hanno quel nome 
        ICollection<Support> GetRequestForNameToLatLon(string name) {
            ICollection<Support> appoggio = new Collection<Support>();
            appoggio.Add(new Support { ID = 0, Name="Manno, JP", Lat = 12, Lon = 13 });
            appoggio.Add(new Support { ID = 1, Name ="Manno, IT", Lat = 2, Lon = 55 });
            appoggio.Add(new Support { ID = 2, Name ="Manno, DE", Lat = 24, Lon = 34 });
            appoggio.Add(new Support { ID = 3, Name ="Manno, USD", Lat = 124, Lon = 138 });
            appoggio.Add(new Support { ID = 4, Name ="Manno, SW", Lat = 23, Lon = 3 });

            return appoggio;
        }
    }
}
