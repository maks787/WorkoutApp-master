using Microsoft.Maui.Controls;
using WorkoutApp.Models;

namespace WorkoutApp
{
    public partial class WorkoutDayPage : ContentPage
    {
        public WorkoutDayPage(WorkoutDay workoutDay)
        {
            InitializeComponent();
            BindingContext = workoutDay;
        }
    }
}
