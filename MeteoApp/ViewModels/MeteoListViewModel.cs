using System;
using System.Collections.ObjectModel;

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

            for (var i = 0; i < 10; i++)
            {
                var e = new Entry
                {
                    ID = i,
<<<<<<< HEAD
                    Name = "Entry " + i,
                    MaxTemperature = GetRandomNumber(230,550),
                    ActualTemperature = GetRandomNumber(0,100),
                    MinTemperature = GetRandomNumber(-10,40)
=======
                    Name = "Entry " + i
>>>>>>> ddb0fa2323fffd94c4d70ff9480ddedb0ec2c8db
                };

                Entries.Add(e);
            }
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
<<<<<<< HEAD

}
=======
}
>>>>>>> ddb0fa2323fffd94c4d70ff9480ddedb0ec2c8db
