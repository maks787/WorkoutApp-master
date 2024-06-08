using Microsoft.Maui.Controls;
using WorkoutApp.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace WorkoutApp
{
    public partial class ABSWork : ContentPage
    {
        public ObservableCollection<WorkoutDay> Days { get; set; }

        public ABSWork()
        {
            InitializeComponent();

            // Создание списка дней с описанием тренировок и упражнениями
            Days = new ObservableCollection<WorkoutDay>();
            for (int i = 1; i <= 30; i++)
            {
                var exercises = new ObservableCollection<WorkoutExercise>
                {
                    new WorkoutExercise { Name = "Crunches", Description = "Lie on your back with your knees bent and feet flat on the floor. Place your hands behind your head and lift your upper body towards your knees.", Image = "crunches.jpg" },
                    new WorkoutExercise { Name = "Plank", Description = "Get into a forearm plank position with your body in a straight line from head to heels. Hold the position for 1 minute.", Image = "plank.png" },
                    new WorkoutExercise { Name = "Bicycle Crunches", Description = "Lie on your back and bring your knees towards your chest. Alternate touching your elbows to the opposite knee in a cycling motion.", Image = "bicycle.png" }
                };

                Days.Add(new WorkoutDay
                {
                    Day = $"Day {i}",
                    Description = $"Full Body Workout details for day {i}.",
                    Exercises = exercises,
                    IsLocked = i != 1 // Разблокируем только первый день
                });
            }

            // Установка контекста данных для привязки
            BindingContext = this;
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

        private async void OnExerciseTapped(object sender, EventArgs e)
        {
            var frame = sender as Frame;
            var exercise = frame?.BindingContext as WorkoutExercise;
            if (exercise != null)
            {
                await DisplayAlert(exercise.Name, exercise.Description, "OK");
            }
        }

        private void OnDayCompleted(object sender, EventArgs e)
        {
            var button = sender as Button;
            var day = button?.BindingContext as WorkoutDay;
            if (day != null)
            {
                day.IsCompleted = true;
                int currentIndex = Days.IndexOf(day);
                if (currentIndex < Days.Count - 1)
                {
                    Days[currentIndex + 1].IsLocked = false; // Разблокируем следующий день
                }
                UpdateDayStyles();
                RefreshCollectionView();
            }
        }

        private void UpdateDayStyles()
        {
            foreach (var day in Days)
            {
                var frame = FindFrameForDay(day);
                if (frame != null)
                {
                    frame.BackgroundColor = Colors.Green;
                }
            }
        }

        private Frame FindFrameForDay(WorkoutDay day)
        {
            foreach (var frame in CollectionViewContainer.Children.OfType<Frame>())
            {
                if (frame.BindingContext == day)
                {
                    return frame;
                }
            }
            return null;
        }
        private void RefreshCollectionView()
        {
            var oldDays = Days;
            Days = new ObservableCollection<WorkoutDay>(Days);
            BindingContext = null;
            BindingContext = this;
        }
    }
}
