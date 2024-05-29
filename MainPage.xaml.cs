using System;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using WorkoutApp.Models;

namespace WorkoutApp
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<WorkoutProgram> RecommendedPrograms { get; set; }

        public MainPage()
        {
            InitializeComponent();

            // Инициализация данных
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

        // Обработчики событий для навигации
        private async void OnHomeClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Navigation", "Home clicked", "OK");
        }

        private async void OnProgramsClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Navigation", "Programs clicked", "OK");
        }

        private async void OnProgressClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Navigation", "Progress clicked", "OK");
        }

        private async void OnSettingsClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Navigation", "Settings clicked", "OK");
        }

        private async void OnCarouselItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedProgram = e.SelectedItem as WorkoutProgram;
            if (selectedProgram != null)
            {
                await DisplayAlert("Workout", $"Starting workout: {selectedProgram.Name}", "OK");
                // Логика для перехода на страницу выбранной программы
                await Navigation.PushAsync(new WorkoutDetailPage(selectedProgram));
            }
        }
    }
}
