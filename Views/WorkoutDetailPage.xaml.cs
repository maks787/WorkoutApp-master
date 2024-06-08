using Microsoft.Maui.Controls;
using WorkoutApp.Models;

namespace WorkoutApp
{
    public partial class WorkoutDetailPage : ContentPage
    {
        public WorkoutDetailPage(WorkoutProgram workoutProgram)
        {
            InitializeComponent();

            Title = workoutProgram.Name;
            WorkoutNameLabel.Text = workoutProgram.Name;
            WorkoutDescriptionLabel.Text = $"Kirjeldus: {workoutProgram.Description}";
       
            WorkoutDurationLabel.Text = $"Kestus: 45 minutit"; // Example static duration
        }
    }
}
