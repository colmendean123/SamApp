using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
namespace app6
{
     public class Database
    {
        private  readonly SQLiteAsyncConnection _database;

        public  Database(String dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Music>();
        }
        public  Task<List<Music>> GetPeopleAsync()
        {
            return _database.Table<Music>().ToListAsync();
        }
        public  Task<int>SavePersonAsync(Music byt)
        {
            return _database.InsertAsync(byt);
        }

        
    }
}
