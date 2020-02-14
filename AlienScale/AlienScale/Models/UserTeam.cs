using SQLite;
using SQLiteNetExtensions.Attributes;

namespace AlienScale.Models
{
    public class UserTeam :  BaseModel
    {
        private string _idUserTeam;
        [PrimaryKey]
        public string IdUserTeam
        {
            get { return _idUserTeam; }
            set
            {
                _idUserTeam = value;
                SetProperty(ref _idUserTeam, value);
            }
        }

        private string _user_IdUser;
        [ForeignKey(typeof(User))]
        public string User_IdUser
        {
            get { return _user_IdUser; }
            set
            {
                _user_IdUser = value;
                SetProperty(ref _user_IdUser, value);
            }
        }

        private string _idTeam;
        [ForeignKey(typeof(Team))]
        public string IdTeam
        {
            get { return _idTeam; }
            set
            {
                _idTeam = value;
                SetProperty(ref _idTeam, value);
            }
        }
    }
}
