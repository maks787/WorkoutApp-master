using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Graphics;

namespace WorkoutApp.Models
{
    public class WorkoutDay : INotifyPropertyChanged
    {
        private bool isCompleted;
        private bool isLocked;

        public string Day { get; set; }
        public string Description { get; set; }
        public ObservableCollection<WorkoutExercise> Exercises { get; set; }

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
    }
}
