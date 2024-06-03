using System.Collections.ObjectModel;
using WorkoutApp.Models;
using Microsoft.Maui.Controls;
public class WorkoutDay
{
    public string Day { get; set; }
    public string Description { get; set; }
    public ObservableCollection<WorkoutExercise> Exercises { get; set; }
    public bool IsCompleted { get; set; } = false; // Новое свойство для отслеживания выполнения дня
    public bool IsLocked { get; set; } = true; // Новое свойство для отслеживания состояния блокировки
}