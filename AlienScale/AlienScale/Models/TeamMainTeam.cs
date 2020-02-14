using SQLite;
using SQLiteNetExtensions.Attributes;

namespace AlienScale.Models
{
    public class TeamMainTeam : BaseModel
    {
        private string _idTeamMainTeam;
        [PrimaryKey]
        public string IdTeamMainTeam
        {
            get { return _idTeamMainTeam; }
            set
            {
                _idTeamMainTeam = value;
                SetProperty(ref _idTeamMainTeam, value);
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

        private string _mainTeam_IdMainTeam;
        [ForeignKey(typeof(MainTeam))]
        public string MainTeam_IdMainTeam
        {
            get { return _mainTeam_IdMainTeam; }
            set
            {
                _mainTeam_IdMainTeam = value;
                SetProperty(ref _mainTeam_IdMainTeam, value);
            }
        }
        
    }
}
