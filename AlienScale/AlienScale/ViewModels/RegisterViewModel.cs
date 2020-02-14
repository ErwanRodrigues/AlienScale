using AlienScale.Models;
using AlienScale.Services;
using AlienScale.ViewModels.Behaviors;
using AlienScale.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AlienScale.ViewModels
{
    public class RegisterViewModel : BaseViewModel
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

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                User = new User()
                {
                    Name = this.Name,
                    Password = this.Password,
                    ConfirmPassword = this.ConfirmPassword,
                    Email = this.Email,
                    TelNumber = this.TelNumber,
                    Text1 = this.Text1,
                    Text2 = this.Text2,
                    CityName = this.CityName,
                    ZipCode = this.ZipCode
                };
                OnPropertyChanged("Name");
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
                    Name = this.Name,
                    Password = this.Password,
                    ConfirmPassword = this.ConfirmPassword,
                    Email = this.Email,
                    TelNumber = this.TelNumber,
                    Text1 = this.Text1,
                    Text2 = this.Text2,
                    CityName = this.CityName,
                    ZipCode = this.ZipCode
                };
                OnPropertyChanged("Password");
            }
        }

        private string confirmPassword;

        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
                User = new User()
                {
                    Name = this.Name,
                    Password = this.Password,
                    ConfirmPassword = this.ConfirmPassword,
                    Email = this.Email,
                    TelNumber = this.TelNumber,
                    Text1 = this.Text1,
                    Text2 = this.Text2,
                    CityName = this.CityName,
                    ZipCode = this.ZipCode
                };
                OnPropertyChanged("ConfirmPassword");
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
                    Name = this.Name,
                    Password = this.Password,
                    ConfirmPassword = this.ConfirmPassword,
                    Email = this.Email,
                    TelNumber = this.TelNumber,
                    Text1 = this.Text1,
                    Text2 = this.Text2,
                    CityName = this.CityName,
                    ZipCode = this.ZipCode
                };
                OnPropertyChanged("Email");
            }
        }

        private string telNumber;

        public string TelNumber
        {
            get { return telNumber; }
            set
            {
                telNumber = value;
                User = new User()
                {
                    Name = this.Name,
                    Password = this.Password,
                    ConfirmPassword = this.ConfirmPassword,
                    Email = this.Email,
                    TelNumber = this.TelNumber,
                    Text1 = this.Text1,
                    Text2 = this.Text2,
                    CityName = this.CityName,
                    ZipCode = this.ZipCode
                };
                OnPropertyChanged("TelNumber");
            }
        }

        private string addName;

        public string AddName
        {
            get { return addName; }
            set
            {
                addName = value;
                User = new User()
                {
                    Name = this.Name,
                    Password = this.Password,
                    ConfirmPassword = this.ConfirmPassword,
                    Email = this.Email,
                    TelNumber = this.TelNumber,
                    Text1 = this.Text1,
                    Text2 = this.Text2,
                    CityName = this.CityName,
                    ZipCode = this.ZipCode
                };
                OnPropertyChanged("AddName");
            }
        }

        private string text1;

        public string Text1
        {
            get { return text1; }
            set
            {
                text1 = value;
                User = new User()
                {
                    Name = this.Name,
                    Password = this.Password,
                    ConfirmPassword = this.ConfirmPassword,
                    Email = this.Email,
                    TelNumber = this.TelNumber,
                    Text1 = this.Text1,
                    Text2 = this.Text2,
                    CityName = this.CityName,
                    ZipCode = this.ZipCode
                };
                OnPropertyChanged("Text1");
            }
        }

        private string text2;

        public string Text2
        {
            get { return text2; }
            set
            {
                text2 = value;
                User = new User()
                {
                    Name = this.Name,
                    Password = this.Password,
                    ConfirmPassword = this.ConfirmPassword,
                    Email = this.Email,
                    TelNumber = this.TelNumber,
                    Text1 = this.Text1,
                    Text2 = this.Text2,
                    CityName = this.CityName,
                    ZipCode = this.ZipCode
                };
                OnPropertyChanged("Text2");
            }
        }

        private string cityName;

        public string CityName
        {
            get { return cityName; }
            set
            {
                cityName = value;
                User = new User()
                {
                    Name = this.Name,
                    Password = this.Password,
                    ConfirmPassword = this.ConfirmPassword,
                    Email = this.Email,
                    TelNumber = this.TelNumber,
                    Text1 = this.Text1,
                    Text2 = this.Text2,
                    CityName = this.CityName,
                    ZipCode = this.ZipCode
                };
                OnPropertyChanged("CityName");
            }
        }

        private string zipCode;

        public string ZipCode
        {
            get { return zipCode; }
            set
            {
                zipCode = value;
                User = new User()
                {
                    Name = this.Name,
                    Password = this.Password,
                    ConfirmPassword = this.ConfirmPassword,
                    Email = this.Email,
                    TelNumber = this.TelNumber,
                    Text1 = this.Text1,
                    Text2 = this.Text2,
                    CityName = this.CityName,
                    ZipCode = this.ZipCode
                };
                OnPropertyChanged("ZipCode");
                OnPropertyChanged("ZipCode");
            }
        }

        private List<Cities> cities;

        public List<Cities> Cities
        {
            get { return cities; }
            set
            {
                cities = value;
                OnPropertyChanged("Cities");
            }
        }

        public RegisterCommand RegisterCommand { get; set; }

        private ICommand _zipcodeCompletedCommand;

        public ICommand ZipcodeCompletedCommand
        {
            get { return _zipcodeCompletedCommand; }
            set
            {
                _zipcodeCompletedCommand = value;
                OnPropertyChanged("ZipcodeCompletedCommand");
            }
        }


        public RegisterViewModel()
        {
            User = new User();
            Cities = new List<Cities>();
            RegisterCommand = new RegisterCommand(this);
            ZipcodeCompletedCommand = new Command((obj) =>
            {
                FillCityPicker((string)obj);
            });
        }

        public async Task<User> Register()
        {
            //getting bindable 
            User user = User;
            
            //adding address & city
            user.Address = new Address();
            user.Address.IdAddress = Guid.NewGuid().ToString();
            user.Address.Text1 = user.Text1;
            user.Address.Text2 = user.Text2;
            user.Address.City = new City();
            user.Address.City.IdCity = Guid.NewGuid().ToString();
            user.Address.City.CityName = user.CityName;
            user.Address.City.ZipCode = user.ZipCode;

            if (User.IsExistingEmail(user))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Email Already Exist", "OK");
            }
            else
            {
                if (User.IsExistingName(user))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Name Already Exist", "OK");
                }
                else
                {
                    if (user.Password != user.ConfirmPassword)
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "Password don't match", "OK");
                    }
                    else
                    {
                        User.Insert(user);
                        await App.Current.MainPage.DisplayAlert("Success", "Registering Successfull", "OK");
                        await App.Current.MainPage.Navigation.PopModalAsync();
                        return user;
                    }
                }
            }
            return null;
        }

        public async void FillCityPicker(string zipCode)
        {
            Cities.Clear();
            Cities = await CityResolver.GetCityFmZipCode(zipCode);
        }


    }
}
