using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using WorkoutApp.Models;
using WorkoutApp.Services;
using System.Linq;
using System.Diagnostics;

namespace WorkoutApp
{
    public partial class ProgressPage : ContentPage
    {
        private DatabaseService _databaseService;
        private User _currentUser;

        public ObservableCollection<WorkoutProgress> WorkoutProgress { get; set; }

        public ProgressPage(User user)
        {
            InitializeComponent();
            _currentUser = user;
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "workout.db3");
            Debug.WriteLine($"Database path in ProgressPage: {dbPath}");
            _databaseService = new DatabaseService(dbPath);
            LoadProgress();
            BindingContext = this;
        }

        private async void LoadProgress()
        {
            WorkoutProgress = new ObservableCollection<WorkoutProgress>();

            var workoutTypes = new[] { "Cardio", "FullBody", "FatBurn", "ABS", "WeightGain" };
            foreach (var type in workoutTypes)
            {
                var days = await _databaseService.GetWorkoutDaysAsync(_currentUser.Id, type);
                if (days != null && days.Any())
                {
                    var completedDays = days.Count(d => d.IsCompleted);
                    var totalDays = days.Count;
                    WorkoutProgress.Add(new WorkoutProgress
                    {
                        WorkoutType = type,
                        ProgressDescription = $"{completedDays} päeva {totalDays} lõpule viidud"
                    });
                }
            }

            BindingContext = this;
        }
    }

    public class WorkoutProgress
    {
        public string WorkoutType { get; set; }
        public string ProgressDescription { get; set; }
    }
}
