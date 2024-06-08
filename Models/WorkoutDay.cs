using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.Maui.Graphics;
using SQLite;

namespace WorkoutApp.Models
{
    public class WorkoutDay : INotifyPropertyChanged
    {
        private bool isCompleted;
        private bool isLocked;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Day { get; set; }
        public string Description { get; set; }


        [Ignore]


        public string Program { get; set; } 

        [Ignore]

        public ObservableCollection<WorkoutExercise> Exercises { get; set; }
        public string ExercisesJson
        {
            get => JsonSerializer.Serialize(Exercises);
            set => Exercises = JsonSerializer.Deserialize<ObservableCollection<WorkoutExercise>>(value);
        }

        public bool IsCompleted
        {
            get => isCompleted;
            set
            {
                if (isCompleted != value)
                {
                    isCompleted = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(FrameBackgroundColor));
                }
            }
        }

        public bool IsLocked
        {
            get => isLocked;
            set
            {
                if (isLocked != value)
                {
                    isLocked = value;
                    OnPropertyChanged();
                }
            }
        }

        public Color FrameBackgroundColor => IsCompleted ? Colors.Green : Colors.White;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UpdateExercisesJson()
        {
            ExercisesJson = JsonSerializer.Serialize(Exercises);
        }
    }
}
