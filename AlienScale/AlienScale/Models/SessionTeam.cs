using SQLite;
using SQLiteNetExtensions.Attributes;

namespace AlienScale.Models
{
    public class SessionTeam :  BaseModel
    {
        private string _idSessionTeam;
        [PrimaryKey]
        public string IdSessionTeam
        {
            get { return _idSessionTeam; }
            set
            {
                _idSessionTeam = value;
                SetProperty(ref _idSessionTeam, value);
            }
        }

        private string _session_IdSession;
        [ForeignKey(typeof(Session))]
        public string Session_IdSession
        {
            get { return _session_IdSession; }
            set
            {
                _session_IdSession = value;
                SetProperty(ref _session_IdSession, value);
            }
        }

        private string _team_IdTeam;
        [ForeignKey(typeof(Team))]
        public string Team_IdTeam
        {
            get { return _team_IdTeam; }
            set
            {
                _team_IdTeam = value;
                SetProperty(ref _team_IdTeam, value);
            }
        }

        private string _spot_IdSpot;
        [ForeignKey(typeof(Spot))]
        public string Spot_IdSpot
        {
            get { return _spot_IdSpot; }
            set
            {
                _spot_IdSpot = value;
                SetProperty(ref _spot_IdSpot, value);
            }
        }
    }
}
