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
    public partial class WeightGain : ContentPage
    {
        private DatabaseService _databaseService;
        private User _currentUser;

        public ObservableCollection<WorkoutDay> Days { get; set; }

        public WeightGain(User user)
        {
            InitializeComponent();
            _currentUser = user;
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "workout.db3");
            Debug.WriteLine($"Database path in WeightGain: {dbPath}");
            _databaseService = new DatabaseService(dbPath);
            Days = new ObservableCollection<WorkoutDay>();
            LoadProgress();
            BindingContext = this;
        }

        private async void LoadProgress()
        {
            Debug.WriteLine("LoadProgress started");
            var days = await _databaseService.GetWorkoutDaysAsync(_currentUser.Id);
            var completedDays = await _databaseService.GetCompletedWorkoutDaysAsync(_currentUser.Id);

            if (days == null || !days.Any())
            {
                Debug.WriteLine("No existing days found, initializing new days");
                InitializeDays();
            }
            else
            {
                Debug.WriteLine($"Found {days.Count} days in database");
                Days.Clear();
                foreach (var day in days.OrderBy(d => d.Id))
                {
                    Days.Add(day);
                }

                // Обновление статусов дней на основе выполненных тренировок
                foreach (var completedDay in completedDays)
                {
                    var matchingDay = Days.FirstOrDefault(d => d.Day == completedDay.Day);
                    if (matchingDay != null)
                    {
                        matchingDay.IsCompleted = true;
                    }
                }

                Debug.WriteLine($"Loaded {Days.Count} days from the database");
                foreach (var day in Days)
                {
                    Debug.WriteLine($"Day {day.Day}, IsLocked: {day.IsLocked}, IsCompleted: {day.IsCompleted}");
                }
                // Установка BindingContext после загрузки данных
                BindingContext = this;
            }
        }

        private void InitializeDays()
        {
            Debug.WriteLine("Initializing days");
            for (int i = 1; i <= 30; i++)
            {
                var exercises = new ObservableCollection<WorkoutExercise>
                {
                    new WorkoutExercise { Name = "Push-Ups", Description = "Perform 3 sets of 15 push-ups.", Image = "pushup.jpg" },
                    new WorkoutExercise { Name = "Plank to Push-Up", Description = "Perform 3 sets of 10 plank to push-ups.", Image = "plankto.png" },
                    new WorkoutExercise { Name = "Chair Dips", Description = "Perform 3 sets of 12 chair dips.", Image = "chairdips.png" }
                };
                var day = new WorkoutDay
                {
                    UserId = _currentUser.Id,
                    Day = $"Day {i}",
                    Description = $"Weight Gain Workout details for day {i}.",
                    Exercises = exercises,
                    IsLocked = i != 1, // Разблокируем только первый день
                    Program = "WeightGain" // Указываем программу
                };

                day.UpdateExercisesJson();
                Days.Add(day);
                _databaseService.SaveWorkoutDayAsync(day).Wait();
                Debug.WriteLine($"Initialized day {i}");
            }
            Debug.WriteLine($"Initialized {Days.Count} days");
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
                    DisplayAlert("Ошибка", "Вы не можете выполнить этот день, не завершив предыдущий.", "OK");
                }
            }
        }
    }
}
