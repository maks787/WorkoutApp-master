using Microsoft.Maui.Controls;
using WorkoutApp.Models;

namespace WorkoutApp
{
    public partial class WorkoutDetailPage : ContentPage
    {
        public WorkoutDetailPage(WorkoutProgram workoutProgram)
        {
            InitializeComponent();
            // Здесь вы можете использовать данные о тренировке (workoutProgram) для настройки страницы
            Title = workoutProgram.Name; // Установка заголовка страницы как название тренировки
        }
    }
}