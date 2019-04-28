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


         void  OnItemAdded(object sender, EventArgs e)
        {
            // TODO modificare per compilare anche in ANDROID
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
                    App.addItem(appogio);
                }
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



    }
}
