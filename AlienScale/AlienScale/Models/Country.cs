using SQLite;

namespace AlienScale.Models
{
    public class Country : BaseModel
    {
        //******************************* PK *******************************
        private string _idCountry;
        [PrimaryKey]
        public string IdCountry
        {
            get { return _idCountry; }
            set
            {
                _idCountry = value;
                SetProperty(ref _idCountry, value);
            }
        }

        //******************************* Members *******************************
        private string _countryName;
        public string CountryName
        {
            get { return _countryName; }
            set
            {
                _countryName = value;
                SetProperty(ref _countryName, value);
            }
        }

        private int _language;
        public int Language
        {
            get { return _language; }
            set
            {
                _language = value;
                SetProperty(ref _language, value);
            }
        }

    }
}
