using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;

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

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }


        ICollection<Entry> getAllCity() {
            ICollection<Entry> appoggio = new ObservableCollection<Entry>();


            return  null;
        }

        static async Task<ICollection<Entry>> HandleFileAsync()
        {
 

            return null;
        }

        void OnItemAdded(object sender, EventArgs e)
        {
            //DisplayAlert("Messaggio", "Testo", "OK");
            string input="";
            UIAlertView alert = new UIAlertView();
            alert.Title = "New City";
            alert.AddButton("Add");
            alert.AddButton("Cancel");
            alert.Message = "Please Enter a city";
            alert.AlertViewStyle = UIAlertViewStyle.PlainTextInput;
            alert.Clicked += (object s, UIButtonEventArgs ev) =>
            {
                if (ev.ButtonIndex == 0)
                {
                     input = alert.GetTextField(0).Text;
                }

                //faccio la richiesta ad openCage
                string c = "https://api.opencagedata.com/geocode/v1/json?q=Manno&key=ea341c0e70344a13bc96f6c28727735a";
                var appoggio = new Entry
                {
                    ID = 2,
                    Name = input

                };
                MeteoListViewModel.Entries.Add(appoggio);
            };
            alert.Show();

        }

        void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Navigation.PushAsync(new MeteoItemPage()
                {
                    BindingContext = e.SelectedItem as Entry
                });
            }
        }
    }
}