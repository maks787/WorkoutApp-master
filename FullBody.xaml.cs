﻿using Microsoft.Maui.Controls;
using WorkoutApp.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace WorkoutApp
{
    public partial class FullBody : ContentPage
    {
        public ObservableCollection<WorkoutDay> Days { get; set; }

        public FullBody()
        {
            InitializeComponent();

            // Создание списка дней с описанием тренировок и упражнениями
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
