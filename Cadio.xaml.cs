using Microsoft.Maui.Controls;
using WorkoutApp.Models;
using System.Collections.ObjectModel;
using Microsoft.Maui.Storage;
using System.Text.Json;

namespace WorkoutApp
{
    public partial class Cadio : ContentPage
    {
        public ObservableCollection<WorkoutDay> Days { get; set; }

        public Cadio()
        {
            InitializeComponent();
            LoadProgress();
            BindingContext = this;
        }

        private void LoadProgress()
        {
            string savedProgress = Preferences.Get("WorkoutProgress", string.Empty);
            if (!string.IsNullOrEmpty(savedProgress))
            {
                Days = JsonSerializer.Deserialize<ObservableCollection<WorkoutDay>>(savedProgress);
                int lastCompletedIndex = Preferences.Get("LastCompletedDayIndex", 0);
                if (lastCompletedIndex > 0 && lastCompletedIndex < Days.Count)
                {
                    var lastCompletedDay = Days[lastCompletedIndex];
                    Days.RemoveAt(lastCompletedIndex);
                    Days.Insert(0, lastCompletedDay);
                }
            }
            else
            {
                InitializeDays();
            }
        }

        private void InitializeDays()
        {
            Days = new ObservableCollection<WorkoutDay>();
            for (int i = 1; i <= 30; i++)
            {
                var exercises = new ObservableCollection<WorkoutExercise>
                {
                    new WorkoutExercise { Name = "Jumping Jacks", Description = "Do 50 jumping jacks.", Image = "jumpingjacks.jpg" },
                    new WorkoutExercise { Name = "Burpees", Description = "Do 20 burpees.", Image = "burpees.jpg" },
                    new WorkoutExercise { Name = "Mountain Climbers", Description = "Do 40 mountain climbers.", Image = "mountainclimbers.jpg" }
                };

                Days.Add(new WorkoutDay
                {
                    Day = $"Day {i}",
                    Description = $"Full Body Workout details for day {i}.",
                    Exercises = exercises,
                    IsLocked = i != 1 // Разблокируем только первый день
                });
            }
        }

        private void SaveProgress()
        {
            string progress = JsonSerializer.Serialize(Days);
            Preferences.Set("WorkoutProgress", progress);
        }

        private async void OnDayTapped(object sender, EventArgs e)
        {
            var frame = sender as Frame;
            var day = frame?.BindingContext as WorkoutDay;
            if (day != null && !day.IsLocked)
            {
                await Navigation.PushAsync(new WorkoutDayPage(day));
            }
        }

        private void OnDayCompleted(object sender, EventArgs e)
        {
            var button = sender as Button;
            var day = button?.BindingContext as WorkoutDay;
            if (day != null)
            {
                int currentIndex = Days.IndexOf(day);
                if (currentIndex == 0 || Days[currentIndex - 1].IsCompleted)
                {
                    day.IsCompleted = true;
                    if (currentIndex < Days.Count - 1)
                    {
                        Days[currentIndex + 1].IsLocked = false; // Разблокируем следующий день
                    }
                    Preferences.Set("LastCompletedDayIndex", currentIndex);
                    SaveProgress();
                }
                else
                {
                    DisplayAlert("Ошибка", "Вы не можете выполнить этот день, не завершив предыдущий.", "OK");
                }
            }
        }
    }
}
