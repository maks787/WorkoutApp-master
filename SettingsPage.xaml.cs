using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using WorkoutApp.Models;
using Microsoft.Maui.Controls;

namespace WorkoutApp
{
    public partial class SettingsPage : ContentPage
    {
        public static class Settings
        {
            public static string user { get; set; }
        }

        public SettingsPage()
        {
            InitializeComponent();
            PopulateUserData();
        }

        private async void PopulateUserData()
        {
            // �������� ��� ������������ �� �������� ����������
            var loggedInUsername = Settings.user;

            if (!string.IsNullOrEmpty(loggedInUsername))
            {
                // �������� ������ ������������ �� ���� ������ �� ��� �����
                var user = await App.Database.GetUserAsync(loggedInUsername, string.Empty); // �������� ������ ������ ������ ������

                if (user != null)
                {
                    // ���������� ������ ������������ � ������
                    UsernameLabel.Text = user.Username;
                    PasswordLabel.Text = user.Password;
                }
                else
                {
                    // ������������ �� ������ � ���� ������
                    await DisplayAlert("Error", "User not found", "OK");
                }
            }
            else
            {
                // ��� ������������ �� ������� � ����������
                await DisplayAlert("Error", "Username not found in settings", "OK");
            }
        }
    }
}
