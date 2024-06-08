using System;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using WorkoutApp.Models;

namespace WorkoutApp
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<WorkoutProgram> RecommendedPrograms { get; set; }
        private User _currentUser;

        public MainPage(User user)
        {
            InitializeComponent();
            _currentUser = user;

  
            RecommendedPrograms = new ObservableCollection<WorkoutProgram>
            {
                new WorkoutProgram { Name = "Full Body Workout", Description = "A complete workout for your whole body.", Image = "fullbody.jpg" },
                new WorkoutProgram { Name = "Cardio Blast", Description = "High-intensity cardio workout.", Image = "cardio.jpg" },
                new WorkoutProgram { Name = "Fat Burning", Description = "Intensive workout to burn fat.", Image = "fatburning.png" },
                new WorkoutProgram { Name = "Abs Workout", Description = "Targeted exercises to build abs.", Image = "absworkout.jpg" },
                new WorkoutProgram { Name = "Weight Gain", Description = "Strength training for muscle gain.", Image = "weightgain.jpg" },
            };

            // Установка BindingContext для привязки данных
            this.BindingContext = this;
        }

     
        private async void OnHomeClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Navigation", "Home clicked", "OK");
            await Navigation.PushAsync(new MainPage(_currentUser));
        }

        private async void OnProgressClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Navigation", "Progress clicked", "OK");
        }

        private async void OnSettingsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage(_currentUser));
        }


        private async void OnLogoutClicked(object sender, EventArgs e)
        {
      
            await Navigation.PushAsync(new LoginPage());

         
            Navigation.RemovePage(this);
        }

        private async void OnImageTapped(object sender, EventArgs e)
        {
            var image = sender as Image;
            if (image == null)
            {
                await DisplayAlert("Error", "Image is null", "OK");
                return;
            }

            var selectedProgram = image.BindingContext as WorkoutProgram;
            if (selectedProgram == null)
            {
                await DisplayAlert("Error", "Selected program is null", "OK");
                return;
            }

            Page targetPage = null;
            switch (selectedProgram.Name)
            {
                case "Full Body Workout":
                    targetPage = new FullBody();
                    break;
                case "Cardio Blast":
                    targetPage = new Cadio(_currentUser); // Передаем текущего пользователя
                    break;
                case "Fat Burning":
                    targetPage = new FatBurn(_currentUser);
                    break;
                case "Abs Workout":
                    targetPage = new ABSWork(_currentUser);
                    break;
                case "Weight Gain":
                    targetPage = new WeightGain(_currentUser);
                    break;
            }

            if (targetPage != null)
            {
                await Navigation.PushAsync(targetPage);
            }
            else
            {
                await DisplayAlert("Error", "Target page is null", "OK");
            }
        }
    }
}
