using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;
using Acr.UserDialogs;

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


        async void OnItemAdded(object sender, EventArgs e)
        {
            var cityToAdd = await UserDialogs.Instance.PromptAsync(new PromptConfig
            {
                InputType = InputType.Name,
                OkText = "Add",

                Title = "New city",
            });
            if (cityToAdd.Ok && !string.IsNullOrWhiteSpace(cityToAdd.Text))
            {
                var newEntry = new Entry
                {
                    ID = cityToAdd.GetHashCode(),
                    Name = cityToAdd.Text
                };

                Task<Entry> task = GetWeatherAsync(cityToAdd.Text);
                var appoggio = await task;

                MeteoListViewModel.Entries.Add(appoggio);
                await App.Database.SaveItemAsync(appoggio);
            }
        }


        public async  Task<Entry>  GetWeatherAsync(string Nome)
        {
            var httpClient = new HttpClient();
           Task<string> contentsTask = httpClient.GetStringAsync("https://api.openweathermap.org/data/2.5/weather?q=" + Nome + "&units=metric&appid=c200173e4aeed3198803206f96382afe");
      
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
