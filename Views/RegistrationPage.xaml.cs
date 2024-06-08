using Microsoft.Maui.Controls;
using WorkoutApp.Models;
using WorkoutApp.Services;
using System.IO;

namespace WorkoutApp
{
    public partial class RegistrationPage : ContentPage
    {
        private DatabaseService _databaseService;

        public RegistrationPage()
        {
            InitializeComponent();
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "workout.db3");
            _databaseService = new DatabaseService(dbPath);
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(); 
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            // Проверьте, что введенные данные не пустые
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Ошибка", "Пожалуйста, введите имя пользователя и пароль", "OK");
                return;
            }

            User newUser = new User
            {
                Username = username,
                Password = password
            };

            await _databaseService.SaveUserAsync(newUser);

            await DisplayAlert("Успех", "Пользователь зарегистрирован", "OK");
            await Navigation.PopAsync(); // Возвращаемся на страницу логина
        }
    }
}
