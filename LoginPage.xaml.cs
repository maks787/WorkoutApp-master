using Microsoft.Maui.Controls;
using WorkoutApp.Models;
using WorkoutApp.Services;

namespace WorkoutApp
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            var username = UsernameEntry.Text;
            var password = PasswordEntry.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Error", "Please enter valid username and password.", "OK");
                return;
            }

            var user = await App.Database.GetUserAsync(username, password);
            if (user != null)
            {
                await DisplayAlert("Success", "Login successful", "OK");
                await Navigation.PushAsync(new UserDetailsPage(user));
            }
            else
            {
                await DisplayAlert("Error", "Invalid username or password.", "OK");
            }
        }
    }
}
