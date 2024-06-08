using WorkoutApp.Models;
using Microsoft.Maui.Controls;

namespace WorkoutApp
{
    public partial class UserDetailsPage : ContentPage
    {
        private User _user;

        public UserDetailsPage(User user)
        {
            InitializeComponent();
            _user = user;
            BindingContext = _user; 
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            if (int.TryParse(AgeEntry.Text, out int age) &&
                double.TryParse(WeightEntry.Text, out double weight) &&
                double.TryParse(HeightEntry.Text, out double height))
            {
                _user.Age = age;
                _user.Weight = weight;
                _user.Height = height;

                await App.Database.SaveUserAsync(_user);

                await Navigation.PushAsync(new MainPage(_user)); 
            }
            else
            {
                await DisplayAlert("Error", "Please enter valid details.", "OK");
            }
        }
    }
}
