using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System;
using System.IO;
using WorkoutApp.Services;

namespace WorkoutApp
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        static DatabaseService database;

        public static DatabaseService Database
        {
            get
            {
                if (database == null)
                {
                    database = new DatabaseService(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WorkoutApp.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage()); // Инициализация страницы логина
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
