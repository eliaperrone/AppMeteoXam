using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SQLite;

namespace MeteoApp.Models
{
    public class Database
    {
        readonly SQLiteAsyncConnection database;

        public Database()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TAppMeteoSQLite.db3");
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Entry>().Wait();

        }

        //Ritorna tutti gli elementi nel database
        public Task<List<Entry>> GetItemsAsync()
        {
            return database.Table<Entry>().ToListAsync();
        }

        //Salva elemento nel database
        public Task<int> SaveItemAsync(Entry item)
        {
            //TODO completare
            if (item.ID == 0) // esempio
                return database.UpdateAsync(item);

            return database.InsertAsync(item);
        }

        //Elimina elemento dal database
        public Task<int> DeleteItemAsync(Entry item)
        {
            return database.DeleteAsync(item);
        }
    }
}
