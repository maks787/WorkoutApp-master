using System;
using Microsoft.Maui.Controls;
using System.IO;
using WorkoutApp.Services;

namespace WorkoutApp
{
    public partial class App : Application
    {
        static Database database;

        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WorkoutApp.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new RegistrationPage());
        }
    }
}
