using SQLite;
using System.Threading.Tasks;
using System.IO;
using WorkoutApp.Models;

namespace WorkoutApp.Services
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait();
        }

        public Task<int> SaveUserAsync(User user)
        {
            if (user.Id != 0)
            {
                return _database.UpdateAsync(user);
            }
            else
            {
                return _database.InsertAsync(user);
            }
        }

        public Task<User> GetUserAsync(string username)
        {
            return _database.Table<User>().Where(u => u.Username == username).FirstOrDefaultAsync();
        }
    }
}
