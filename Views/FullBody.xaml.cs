using Microsoft.Maui.Controls;
using WorkoutApp.Models;
using WorkoutApp.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;

namespace WorkoutApp
{
    public partial class FullBody : ContentPage
    {
        private DatabaseService _databaseService;
        private User _currentUser;

        public ObservableCollection<WorkoutDay> Days { get; set; }

        public FullBody(User user)
        {
            InitializeComponent();
            _currentUser = user;
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "workout.db3");
            Debug.WriteLine($"Database path in FullBody: {dbPath}");
            _databaseService = new DatabaseService(dbPath);
            LoadProgress();
        }

        private async void LoadProgress()
        {
            Debug.WriteLine("LoadProgress started");
            var days = await _databaseService.GetWorkoutDaysAsync(_currentUser.Id, "FullBody"); // Указываем тип тренировки
            if (days == null || !days.Any())
            {
                Debug.WriteLine("No existing days found, initializing new days");
                InitializeDays();
            }
            else
            {
                Debug.WriteLine($"Found {days.Count} days in database");
                Days = new ObservableCollection<WorkoutDay>(days.OrderBy(d => d.Id).ToList());
                Debug.WriteLine($"Loaded {Days.Count} days from the database");
                foreach (var day in Days)
                {
                    Debug.WriteLine($"Day {day.Day}, IsLocked: {day.IsLocked}, IsCompleted: {day.IsCompleted}");
                }
                BindingContext = this;
            }
        }

        private void InitializeDays()
        {
            Debug.WriteLine("Initializing days");
            Days = new ObservableCollection<WorkoutDay>();
            for (int i = 1; i <= 30; i++)
            {
                var exercises = new ObservableCollection<WorkoutExercise>
                {
                    new WorkoutExercise { Name = "Tõuked", Description = "Tehke 20 tõuget.", Image = "pushups.jpg" },
                    new WorkoutExercise { Name = "Kükid", Description = "Tehke 30 kükki.", Image = "squats.jpg" },
                    new WorkoutExercise { Name = "Kükke", Description = "Tehke mõlemal jalal 20 kükitamist.", Image = "lunges.jpg" }
                };

                var day = new WorkoutDay
                {
                    UserId = _currentUser.Id,
                    WorkoutType = "FullBody", // Указываем тип тренировки
                    Day = $"Päev {i}",
                    Description = $"Full Body treeningu üksikasjad päevaks {i}.",
                    Exercises = exercises,
                    IsLocked = i != 1 // Разблокируем только первый день
                };

                day.UpdateExercisesJson();
                Days.Add(day);
                _databaseService.SaveWorkoutDayAsync(day).Wait();
                Debug.WriteLine($"Initialized day {i}");
            }
            Debug.WriteLine($"Initialized {Days.Count} days");
            BindingContext = this;
        }

        private async void SaveProgress(WorkoutDay day)
        {
            day.UpdateExercisesJson();
            await _databaseService.SaveWorkoutDayAsync(day);
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
                    SaveProgress(day);
                }
                else
                {
                    DisplayAlert("Viga", "Te ei saa seda päeva läbida, ilma et te oleksite lõpetanud eelmise päeva.", "OK");
                }
            }
        }
    }
}
