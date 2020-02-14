using SQLite;

namespace AlienScale.Models
{
    public class MainTeam : BaseModel
    {
        //******************************* PK *******************************
        private string _idMainTeam;
        [PrimaryKey]
        public string IdMainTeam
        {
            get { return _idMainTeam; }
            set
            {
                _idMainTeam = value;
                SetProperty(ref _idMainTeam, value);
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

        private string _emblemUrl;

        public string EmblemUrl
        {
            get { return _emblemUrl; }
            set
            {
                _emblemUrl = value;
                SetProperty(ref _emblemUrl,value);
            }
        }
        
    }
}
