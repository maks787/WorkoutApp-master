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
                new WorkoutProgram { Name = "Full Body treening", Description = "Täielik treening kogu kehale.", Image = "fullbody.jpg" },
                new WorkoutProgram { Name = "Cardio Blast", Description = "Kõrge intensiivsusega kardiotreening.", Image = "cardio.jpg" },
                new WorkoutProgram { Name = "Fat Burning", Description = "Intensiivne treening rasva põletamiseks.", Image = "fatburning.png" },
                new WorkoutProgram { Name = "Abs Workout", Description = "Sihtotstarbelised harjutused kõhulihaste ehitamiseks.", Image = "absworkout.jpg" },
                new WorkoutProgram { Name = "Weight Gain", Description = "Jõutreening lihaste kasvatamiseks.", Image = "weightgain.jpg" },
            };

            // Установка BindingContext для привязки данных
            this.BindingContext = this;
        }

        private async void OnHomeClicked(object sender, EventArgs e)
        {
           
            await Navigation.PushAsync(new MainPage(_currentUser));
        }

        private async void OnProgressClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProgressPage(_currentUser));
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
                case "Full Body treening":
                    targetPage = new FullBody(_currentUser);
                    break;
                case "Cardio Blast":
                    targetPage = new Cadio(_currentUser);
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
