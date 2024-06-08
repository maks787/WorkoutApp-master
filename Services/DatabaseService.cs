using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using WorkoutApp.Models;
using System.Diagnostics;

namespace WorkoutApp.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait();
            _database.CreateTableAsync<WorkoutDay>().Wait(); // Создание таблицы WorkoutDay
        }

        // User methods
        public Task<List<User>> GetUsersAsync()
        {
            return _database.Table<User>().ToListAsync();
        }

        public async Task<User> GetUserAsync(string username, string password)
        {
            var user = await _database.Table<User>()
                            .Where(u => u.Username == username && u.Password == password)
                            .FirstOrDefaultAsync();

            Debug.WriteLine(user != null ? $"User found: {user.Username}" : "User not found");
            return user;
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

        public Task<int> DeleteUserAsync(User user)
        {
            return _database.DeleteAsync(user);
        }

        // WorkoutDay methods
        public Task<List<WorkoutDay>> GetWorkoutDaysAsync(int userId, string workoutType)
        {
            return _database.Table<WorkoutDay>()
                            .Where(d => d.UserId == userId && d.WorkoutType == workoutType)
                            .ToListAsync();
        }

        public Task<int> SaveWorkoutDayAsync(WorkoutDay day)
        {
            if (day.Id != 0)
            {
                return _database.UpdateAsync(day);
            }
            else
            {
                return _database.InsertAsync(day);
            }
        }

        public Task<int> DeleteWorkoutDayAsync(WorkoutDay day)
        {
            return _database.DeleteAsync(day);
        }
    }
}
