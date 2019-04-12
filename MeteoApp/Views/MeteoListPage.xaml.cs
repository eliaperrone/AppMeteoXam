using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
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


        ICollection<Entry> getAllCity() {
            ICollection<Entry> appoggio = new ObservableCollection<Entry>();


            return  null;
        }


        async void OnItemAdded(object sender, EventArgs e)
        {
            //DisplayAlert("Messaggio", "Testo", "OK");

            var cityToAdd = await UserDialogs.Instance.PromptAsync(new PromptConfig
            {
                InputType = InputType.Name,
                OkText = "Add",

                Title = "New city",
            });
            // esempio: creo una nuova Entry partendo dal testo e la aggiungo al ViewModel
            if (cityToAdd.Ok && !string.IsNullOrWhiteSpace(cityToAdd.Text))
            {
                var newEntry = new Entry
                {
                    ID = cityToAdd.GetHashCode(),
                    Name = cityToAdd.Text
                };

                MeteoListViewModel.Entries.Add(newEntry);
            }


            //string input="";
            //UIAlertView alert = new UIAlertView();
            //alert.Title = "New City";
            //alert.AddButton("Add");
            //alert.AddButton("Cancel");
            //alert.Message = "Please Enter a city";
            //alert.AlertViewStyle = UIAlertViewStyle.PlainTextInput;
            //alert.Clicked += (object s, UIButtonEventArgs ev) =>
            //{
            //    if (ev.ButtonIndex == 0)
            //    {
            //         input = alert.GetTextField(0).Text;

            //        var appoggio = new Entry
            //        {
            //            ID = 2,
            //            Name = input

            //        };
            //        OnActionSheetSimpleClicked(sender, e);
            //        MeteoListViewModel.Entries.Add(appoggio);
            //    }
            //    //faccio la richiesta ad openCage

            //};
            //alert.Show();

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
            string action = await DisplayActionSheet("ActionSheet: Send to?", "Cancel", null, "Email", "Twitter", "Facebook");
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
