using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Graphics;
using SQLite;
using Newtonsoft.Json;

namespace WorkoutApp.Models
{
    public class WorkoutDay : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Day { get; set; }
        public string Description { get; set; }
        public string ExercisesJson { get; set; }

        [Ignore]
        public ObservableCollection<WorkoutExercise> Exercises
        {
            get => JsonConvert.DeserializeObject<ObservableCollection<WorkoutExercise>>(ExercisesJson);
            set => ExercisesJson = JsonConvert.SerializeObject(value);
        }

        private bool isCompleted;
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

        private bool isLocked;
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

        [Ignore]
        public Color FrameBackgroundColor => IsCompleted ? Colors.Green : Colors.White;

        public void UpdateExercisesJson()
        {
            ExercisesJson = JsonConvert.SerializeObject(Exercises);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
