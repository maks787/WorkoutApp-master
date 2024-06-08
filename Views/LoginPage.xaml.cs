using Microsoft.Maui.Controls;
using WorkoutApp.Models;
using WorkoutApp.Services;
using System.IO;
using System.Diagnostics;


namespace WorkoutApp
{
    public partial class LoginPage : ContentPage
    {
        private DatabaseService _databaseService;

        public LoginPage()
        {
            InitializeComponent();
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "workout.db3");
            Debug.WriteLine($"Database path: {dbPath}");
            _databaseService = new DatabaseService(dbPath);
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            Debug.WriteLine($"Attempting to login with Username: {username}, Password: {password}");

            User user = await _databaseService.GetUserAsync(username, password);

            if (user != null)
            {
                Debug.WriteLine("Login successful");

                // Заменяем текущую страницу MainPage
                var mainPage = new MainPage(user);
                NavigationPage navigationPage = (NavigationPage)Application.Current.MainPage;
                navigationPage.Navigation.InsertPageBefore(mainPage, this);
                await navigationPage.Navigation.PopAsync(); // Удаляем страницу логина из стека
            }
            else
            {
                Debug.WriteLine("Login failed");
                await DisplayAlert("Ошибка", "Неправильное имя пользователя или пароль", "OK");
            }
        }


        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
        }
    }
}
