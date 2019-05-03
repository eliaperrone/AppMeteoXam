﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MeteoApp
{
    public class MeteoListViewModel : BaseViewModel
    {
         ObservableCollection<Entry> _entries;
         List<Entry> dbEntry;

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

            //Carico contenuto database
            dbEntry = App.Database.GetItemsAsync().Result;
            foreach (var entry in dbEntry)
            {
                Entries.Add(entry);
            }

            //for (var i = 0; i < 10; i++)
            //{
            //    var e = new Entry
            //    {
            //        ID = i,
            //        Name = "Entry " + i,
            //        MaxTemperature = GetRandomNumber(230,550),
            //        ActualTemperature = GetRandomNumber(0,100),
            //        MinTemperature = GetRandomNumber(-10,40)
            //    };

            //    Entries.Add(e);
            //}
        }
        private static readonly Random getrandom = new Random();

        public static int GetRandomNumber(int min, int max)
        {
            lock (getrandom) // synchronize
            {
                return getrandom.Next(min, max);
            }
        }



    }


}

