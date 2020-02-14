using AlienScale.Models;
using AlienScale.ViewModels.Commands;
using AlienScale.Views;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using AlienScale.Views.Popups;
using Rg.Plugins.Popup.Services;
using System.Linq;
using System.Threading.Tasks;

namespace AlienScale.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private User user;
        public User User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                User = new User()
                {
                    Email = this.Email,
                    Password = this.Password
                };
                OnPropertyChanged("Email");
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                User = new User()
                {
                    Email = this.Email,
                    Password = this.Password
                };
                OnPropertyChanged("Password");
            }
        }
        
        public ICommand LoginCommand { get; set; }

        public ICommand RegisterNavigationCommand { get; set; }

        public ICommand GoToToolbarCommand { get; set; }
        
        public LoginViewModel()
        {
            User = new User();
            LoginCommand = new LoginCommand(this);
            RegisterNavigationCommand = new RegisterNavigationCommand(this);
            GoToToolbarCommand = new Command(async () =>
            {
                
            }); 

        }

        public async void Login()
        {
            bool canLogin = User.Login(User.Email, User.Password);

            if (canLogin)
                try
                {
                    var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Location);

                    if (status != PermissionStatus.Granted)
                    {
                        if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                        {
                            await App.Current.MainPage.DisplayAlert("Need permission", "We will have to access your location", "OK");
                        }
                        var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                        if (results.ContainsKey(Permission.Location))
                            status = results[Permission.Location];
                    }
                    if (status == PermissionStatus.Granted)
                    {
                        await App.Current.MainPage.Navigation.PushAsync(new HomePage());
                    }

                }
                catch (Exception ex)
                {

                }

            else
                await App.Current.MainPage.DisplayAlert("Error", "Error processing login, try again", "Ok");
        }

        public async void Navigate()
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new RegisterPage());
        }

    }
}
