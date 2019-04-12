using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


        ICollection<Entry> getAllCity() {
            ICollection<Entry> appoggio = new ObservableCollection<Entry>();


            return  null;
        }



        void OnItemAdded(object sender, EventArgs e)
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
            alert.Clicked += (object s, UIButtonEventArgs ev) =>
            {
                if (ev.ButtonIndex == 0)
                {
                     input = alert.GetTextField(0).Text;
                }

                //faccio la richiesta ad openCage
               var appoggio = new Entry
                {
                    ID = 2,
                    Name = input

                };
                if (appoggio.Name != "")
                {
                    MeteoListViewModel.Entries.Add(appoggio);
                }
            };
            alert.Show();

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
