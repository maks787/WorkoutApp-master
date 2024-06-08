using Microsoft.Maui.Controls;
using WorkoutApp.Models;
using WorkoutApp.Services;
using System.IO;
using System.Diagnostics;

namespace WorkoutApp
{
    public partial class SettingsPage : ContentPage
    {
        private DatabaseService _databaseService;
        private User _currentUser;

        public SettingsPage(User user)
        {
            InitializeComponent();
            _currentUser = user;
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "workout.db3");
            Debug.WriteLine($"Database path in SettingsPage: {dbPath}");
            _databaseService = new DatabaseService(dbPath);
            LoadUserData();
        }

        private void LoadUserData()
        {
            UsernameEntry.Text = _currentUser.Username;
            PasswordEntry.Text = _currentUser.Password;
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            _currentUser.Username = UsernameEntry.Text;
            _currentUser.Password = PasswordEntry.Text;

            await _databaseService.SaveUserAsync(_currentUser);

            await DisplayAlert("Edu", "Muudatused salvestatud edukalt.", "OK");
            await Navigation.PushAsync(new MainPage(_currentUser));
        }
    }
}
