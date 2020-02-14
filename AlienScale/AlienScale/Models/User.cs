using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AlienScale.Models
{
    public class User :  BaseModel
    {

        //******************************* PK *******************************
        private string _idUser;
        [PrimaryKey]
        public string IdUser
        {
            get { return _idUser; }
            set
            {
                _idUser = value;
                SetProperty(ref _idUser, value);
            }
        }

        //******************************* Members *******************************
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                SetProperty(ref _name, value);
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                SetProperty(ref _password, value);
            }
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                OnPropertyChanged("ConfirmPassword");
            }
        }
        
        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                SetProperty(ref _email, value);
            }
        }

        private string _telNumber;
        public string TelNumber
        {
            get { return _telNumber; }
            set
            {
                _telNumber = value;
                SetProperty(ref _telNumber, value);
            }
        }
        
        private string _avatarUrl;
        public string AvatarUrl
        {
            get { return _avatarUrl; }
            set
            {
                _avatarUrl = value;
                SetProperty(ref _avatarUrl, value);
            }
        }
        
        private string _text1;
        [Ignore]
        public string Text1
        {
            get { return _text1; }
            set
            {
                _text1 = value;
                SetProperty(ref _text1, value);
            }
        }

        private string _text2;
        [Ignore]
        public string Text2
        {
            get { return _text2; }
            set
            {
                _text2 = value;
                SetProperty(ref _text2, value);
            }
        }

        private string _cityName;
        [Ignore]
        public string CityName
        {
            get { return _cityName; }
            set
            {
                _cityName = value;
                SetProperty(ref _cityName, value);
            }
        }

        private string _zipCode;
        [Ignore]
        public string ZipCode
        {
            get { return _zipCode; }
            set
            {
                _zipCode = value;
                SetProperty(ref _zipCode, value);
            }
        }
        //******************************* FK *******************************
        private string _address_IdAddress;
        [ForeignKey(typeof(Address))]
        public string Address_IdAddress
        {
            get { return _address_IdAddress; }
            set
            {
                _address_IdAddress = value;
                SetProperty(ref _address_IdAddress, value);
            }
        }

        private Address _address;
        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public Address Address
        {
            get { return _address; }
            set
            {
                _address = value;
                SetProperty(ref _address, value);
            }
        }
        
        public static bool Login(string email, string password)
        {

            bool isEmailEmpty = string.IsNullOrEmpty(email);
            bool isPasswordEmpty = string.IsNullOrEmpty(password);

            //if at least one is empty
            if (isEmailEmpty || isPasswordEmpty)
            {
                return false;
            }
            else
            {
                //Email regex
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<User>();
                    var user = conn.Table<User>().Where(p => p.Email == email).FirstOrDefault();

                    if (user != null)
                    {
                        if (user.Password == password)
                        {
                            App.user = user;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        
        public static bool IsExistingEmail(User user)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<User>();
                if(user != null)
                {
                    var us = conn.Table<User>().Where(p => p.Email == user.Email).FirstOrDefault();
                    if (us == null)
                        return false;
                    else
                        return true;
                }
                return true;
            }
        }

        public static bool IsExistingName(User user)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<User>();
                if (user != null)
                {
                    var us = conn.Table<User>().Where(p => p.Name == user.Name).FirstOrDefault();
                    if (us == null)
                        return false;
                    else
                        return true;
                }
                return true;
            }
        }

        public static void Insert(User user)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                user.IdUser = Guid.NewGuid().ToString();
                conn.CreateTable<User>();
                conn.InsertOrReplaceWithChildren(user, recursive: true);
            }
        }
    }
}
