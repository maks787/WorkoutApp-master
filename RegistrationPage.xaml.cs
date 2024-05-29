using Microsoft.Maui.Controls;
using WorkoutApp.Models;
using WorkoutApp.Services;

namespace WorkoutApp
{
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            var username = UsernameEntry.Text;
            var password = PasswordEntry.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Error", "Please enter valid username and password.", "OK");
                return;
            }

            var user = new User { Username = username, Password = password };
            await App.Database.SaveUserAsync(user);

            await Navigation.PushAsync(new UserDetailsPage(user));
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}
