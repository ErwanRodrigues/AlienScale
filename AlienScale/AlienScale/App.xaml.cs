using AlienScale.Models;
using AlienScale.StaticResources;
using AlienScale.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AlienScale
{
    public partial class App : Application
    {
        public static User user = new User();
        public static string DatabaseLocation = string.Empty;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage()); 
        }
        public App(string databaseLocation)
        {
            DatabaseLocation = databaseLocation;
            MainPage = new NavigationPage(new LoginPage());
            
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            //Check & do the first init if required
            Initialization.CreateTables();
            Initialization.Initialize();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
