using System;
using SQLite;
namespace MeteoApp
{
    public class Entry
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string State { get; set; }  //aggiunto dopo
        public string Description { get; set; } //aggiunto dopo
        public double Lat { get; set; }
        public double Lon { get; set; }
        public double ActualTemperature { get; set; }
        public double MaxTemperature { get; set; }
        public double MinTemperature { get; set; }
        public string icon { get; set; }


    }
}
