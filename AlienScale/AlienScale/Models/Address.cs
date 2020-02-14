using SQLite;
using System;
using SQLiteNetExtensions.Attributes;
using AlienScale.StaticResources;
using AlienScale.Models;

namespace AlienScale.Models
{
    public class Address : BaseModel
    {

        //******************************* PK *******************************
        private string _idAddress;
        [PrimaryKey]
        public string IdAddress
        {
            get { return _idAddress; }
            set
            {
                _idAddress = value;
                SetProperty(ref _idAddress, value);
            }
        }

        //******************************* Members *******************************
        private string _text1;
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
        public string Text2
        {
            get { return _text2; }
            set
            {
                _text2 = value;
                SetProperty(ref _text2, value);
            }
        }

        //******************************* FKs *******************************
        private string _city_IdCity;
        [ForeignKey(typeof(City))]
        public string City_IdCity
        {
            get { return _city_IdCity; }
            set
            {
                _city_IdCity = value;
                SetProperty(ref _city_IdCity, value);
            }
        }

        private City _city;
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeInsert | CascadeOperation.CascadeRead)]
        public City City
        {
            get { return _city; }
            set
            {
                _city = value;
                SetProperty(ref _city, value);
            }
        }
        
        public static int Insert(Address address)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                address.IdAddress = Guid.NewGuid().ToString();
                conn.CreateTable<Address>();
                int rows = conn.Insert(address);

                if (rows > 0)
                    return StaticValues.SUCCESS;
                else
                    return StaticValues.ERROR;
            }
        }
        
    }
}
