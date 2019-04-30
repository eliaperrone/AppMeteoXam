using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SQLite;

namespace MeteoApp
{
    public class TestDatabase
    {
        readonly SQLiteAsyncConnection database;

        public TestDatabase()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TestSQLite.db4");
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Entry>().Wait();
        }

        /*
         * Ritorna una lista con tutti gli items.
         */
        public Task<List<Entry>> GetItemsAsync()
        { 
            return database.Table<Entry>().ToListAsync();
        }

        /*
         * Query con query SQL.
         */
        public Task<List<Entry>> GetItemsWithWhere(int id)
        {
            return database.QueryAsync<Entry>("SELECT * FROM [TestItem] WHERE [Id] =?", id);
        }

        /*
         * Query con LINQ.
         */
        public Task<Entry> GetItemAsync(int id)
        {
            return database.Table<Entry>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        /*
         * Salvataggio o update.
         */
        public Task<int> SaveItemAsync(Entry item)
        {
            System.Console.WriteLine("Inserito Elemento nel db " + item.Name);
            return database.InsertAsync(item);
        }

        public Task<int> DeleteItemAsync(Entry item)
        {
            return database.DeleteAsync(item);
        }
    }
}

