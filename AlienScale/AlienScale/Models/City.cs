using SQLite;
using System;
using SQLiteNetExtensions.Attributes;
using AlienScale.StaticResources;

namespace AlienScale.Models
{
    public class City : BaseModel
    {
        //******************************* PK *******************************
        private string _idCity;
        [PrimaryKey]
        public string IdCity
        {
            get { return _idCity; }
            set
            {
                _idCity = value;
                SetProperty(ref _idCity, value);
            }
        }

        //******************************* Members *******************************
        private string _cityName;
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
        private string _country_IdCountry;
        [ForeignKey(typeof(Country))]
        public string Country_IdCountry
        {
            get { return _country_IdCountry; }
            set
            {
                _country_IdCountry = value;
                SetProperty(ref _country_IdCountry, value);
            }
        }

        private Country _country;
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeInsert | CascadeOperation.CascadeRead)]
        public Country Country
        {
            get { return _country; }
            set
            {
                _country = value;
                SetProperty(ref _country, value);
            }
        }
        
        public static int Insert(City city)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                city.IdCity = Guid.NewGuid().ToString();
                conn.CreateTable<City>();
                int rows = conn.Insert(city);

                if (rows > 0)
                    return StaticValues.SUCCESS;
                else
                    return StaticValues.ERROR;
            }
        }
    }

    
}
