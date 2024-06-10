using Microsoft.Maui.Controls;
using WorkoutApp.Models;
using WorkoutApp.Services;
using System.IO;
using System.Diagnostics;

namespace WorkoutApp
{
    public partial class RegistrationPage : ContentPage
    {
        private DatabaseService _databaseService;

        public RegistrationPage()
        {
            InitializeComponent();
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "workout.db3");
            Debug.WriteLine($"Database path in RegistrationPage: {dbPath}");
            _databaseService = new DatabaseService(dbPath);
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            var user = new User
            {
                Username = username,
                Password = password
                // Остальные поля можно установить позже
            };

            await _databaseService.SaveUserAsync(user);

            await DisplayAlert("Edu", "Kasutaja registreeritud", "OK");
            await Navigation.PopAsync();
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
