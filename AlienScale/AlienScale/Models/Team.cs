using SQLite;

namespace AlienScale.Models
{
    public class Team : BaseModel
    {
        //******************************* PK *******************************
        private string _idTeam;
        [PrimaryKey]
        public string IdTeam
        {
            get { return _idTeam; }
            set
            {
                _idTeam = value;
                SetProperty(ref _idTeam, value);
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
        
    }
}
