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
            WorkoutDescriptionLabel.Text = $"Description: {workoutProgram.Description}";
            // Assuming you have a duration property or you can add it to your model
            WorkoutDurationLabel.Text = $"Duration: 45 minutes"; // Example static duration
        }
    }
}
