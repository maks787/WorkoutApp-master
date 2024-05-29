using Microsoft.Maui.Controls;
using WorkoutApp.Models;

namespace WorkoutApp
{
    public partial class WorkoutDetailPage : ContentPage
    {
        public WorkoutDetailPage(WorkoutProgram workoutProgram)
        {
            InitializeComponent();
            // ����� �� ������ ������������ ������ � ���������� (workoutProgram) ��� ��������� ��������
            Title = workoutProgram.Name; // ��������� ��������� �������� ��� �������� ����������
        }
    }
}