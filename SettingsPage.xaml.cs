using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using WorkoutApp.Models;
using Microsoft.Maui.Controls;

namespace WorkoutApp
{
    public partial class SettingsPage : ContentPage
    {
        public static class Settings
        {
            public static string user { get; set; }
        }

        public SettingsPage()
        {
            InitializeComponent();
            PopulateUserData();
        }

        private async void PopulateUserData()
        {
            // Получаем имя пользователя из настроек приложения
            var loggedInUsername = Settings.user;

            if (!string.IsNullOrEmpty(loggedInUsername))
            {
                // Получаем данные пользователя из базы данных по его имени
                var user = await App.Database.GetUserAsync(loggedInUsername, string.Empty); // Передаем пустую строку вместо пароля

                if (user != null)
                {
                    // Отображаем данные пользователя в метках
                    UsernameLabel.Text = user.Username;
                    PasswordLabel.Text = user.Password;
                }
                else
                {
                    // Пользователь не найден в базе данных
                    await DisplayAlert("Error", "User not found", "OK");
                }
            }
            else
            {
                // Имя пользователя не найдено в настройках
                await DisplayAlert("Error", "Username not found in settings", "OK");
            }
        }
    }
}
