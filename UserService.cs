using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutApp.Models;

namespace WorkoutApp.Services
{
    public class UserService
    {
        private static UserService _instance;
        private readonly List<User> users;

        private UserService()
        {
            users = new List<User>();
        }

        public static UserService Instance => _instance ??= new UserService();

        public Task SaveUserAsync(User user)
        {
            users.Add(user);
            return Task.CompletedTask;
        }

        public Task<User> GetUserAsync(string username, string password)
        {
            var user = users.FirstOrDefault(u => u.Username == username && u.Password == password);
            return Task.FromResult(user);
        }
    }
}
