using Microsoft.Maui.Controls;
using WorkoutApp.Models;
using System.Collections.ObjectModel;

namespace WorkoutApp
{
    public partial class Cadio : ContentPage
    {
        public ObservableCollection<WorkoutDay> Days { get; set; }

        public Cadio()
        {
            InitializeComponent();

            // Создание списка дней с описанием тренировок
            Days = new ObservableCollection<WorkoutDay>();
            for (int i = 1; i <= 30; i++)
            {
                Days.Add(new WorkoutDay
                {
                    Day = $"Day {i}",
                    Description = $"Full Body Workout details for day {i}."
                });
            }

            // Установка контекста данных для привязки
            BindingContext = this;
        }
    }
}
