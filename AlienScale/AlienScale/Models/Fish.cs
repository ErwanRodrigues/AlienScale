using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using SQLiteNetExtensions.Extensions;
using SQLiteNetExtensions.Attributes;
using System.Globalization;
using AlienScale.StaticResources;

namespace AlienScale.Models
{
    public class Fish : BaseModel
    {
        //******************************* PK *******************************
        private string _idFish;
        [PrimaryKey]
        public string IdFish
        {
            get { return _idFish; }
            set
            {
                _idFish = value;
                SetProperty(ref _idFish,value);
            }
        }

        //******************************* Members *******************************
        
        private DateTimeOffset _catchedDateTime;
        public DateTimeOffset CatchedDateTime
        {
            get { return _catchedDateTime; }
            set
            {
                _catchedDateTime = value;
                SetProperty(ref _catchedDateTime, value);
            }
        }

        private double _weight;
        public double Weight
        {
            get { return _weight; }
            set
            {
                _weight = value;
                SetProperty(ref _weight, value);
            }
        }

        private string _weightstr;
        [Ignore]
        public string Weightstr
        {
            get { return _weightstr; }
            set
            {
                _weightstr = value;
                if (!string.IsNullOrEmpty(_weightstr))
                    Weight = double.Parse(value.Replace(',', '.'), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
                SetProperty(ref _weightstr, value);
            }
        }

        private double _length;
        public double Length
        {
            get { return _length; }
            set
            {
                _length = value;
                SetProperty(ref _length, value);
            }
        }

        private string _lengthstr;
        [Ignore]
        public string Lengthstr
        {
            get { return _lengthstr; }
            set
            {
                _lengthstr = value;
                if(!string.IsNullOrEmpty(_lengthstr))
                    Length = double.Parse(value.Replace(',', '.'), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
                SetProperty(ref _lengthstr, value);
            }
        }

        private string _fishPictureUrl;
        public string FishPictureUrl
        {
            get { return _fishPictureUrl; }
            set
            {
                _fishPictureUrl = value;
                SetProperty(ref _fishPictureUrl, value);
            }
        }
        
        private string _bait;
        public string Bait
        {
            get { return _bait; }
            set
            {
                _bait = value;
                SetProperty(ref _bait, value);
            }
        }

        private string _weather;
        public string Weather
        {
            get { return _weather; }
            set
            {
                _weather = value;
                SetProperty(ref _weather, value);
            }
        }

        //******************************* FK *******************************
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

        private string _fishingLoc_IdFishingLoc;
        [ForeignKey(typeof(FishingLoc))]
        public string FishingLoc_IdFishingLoc
        {
            get { return _fishingLoc_IdFishingLoc; }
            set
            {
                _fishingLoc_IdFishingLoc = value;
                SetProperty(ref _fishingLoc_IdFishingLoc, value);
            }
        }

        private string _fishType_IdType;
        [ForeignKey(typeof(FishType))]
        public string FishType_IdFishType
        {
            get { return _fishType_IdType; }
            set
            {
                _fishType_IdType = value;
                SetProperty(ref _fishType_IdType, value);
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

        private string _waypoint_IdWaypoint;
        [ForeignKey(typeof(Waypoint))]
        public string Waypoint_IdWaypoint
        {
            get { return _waypoint_IdWaypoint; }
            set
            {
                _waypoint_IdWaypoint = value;
                SetProperty(ref _waypoint_IdWaypoint, value);
            }
        }
        
        private FishType _fishType;
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeInsert | CascadeOperation.CascadeRead)]
        public FishType FishType
        {
            get { return _fishType; }
            set
            {
                _fishType = value;
                SetProperty(ref _fishType, value);
            }
        }

        private FishingLoc _fishingLoc;
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert | CascadeOperation.CascadeRead)]
        public FishingLoc FishingLoc
        {
            get { return _fishingLoc; }
            set
            {
                _fishingLoc = value;
                SetProperty(ref _fishingLoc, value);
            }
        }
        
        public static void Insert(Fish fish)
        {

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Fish>();
                conn.InsertOrReplaceWithChildren(fish, recursive:true);
            }
        }

        public static List<Fish> GetFishes()
        {
            List<Fish> fishes = new List<Fish>();
            List<FishType> types  = new List<FishType>();
            using (SQLiteConnection conn = new SQLiteConnection( App.DatabaseLocation))
            {
                conn.CreateTable<Fish>();
                conn.CreateTable<FishType>();
                fishes = conn.GetAllWithChildren<Fish>();
            }
            return fishes;
        }

        public static List<Fish> GetFishesByUserId(string idUser)
        {
            List<Fish> fishes = new List<Fish>();
            List<FishType> types  = new List<FishType>();
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                fishes = conn.GetAllWithChildren<Fish>(recursive:true);
                fishes = (from p in fishes
                            where p.User_IdUser == idUser
                            select p).ToList();
            }
            return fishes;
        }

        public static int Delete(Fish fish)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Fish>();
                int rows = conn.Delete<Fish>(fish.IdFish);

                if (rows > 0)
                    return StaticValues.SUCCESS;
                else
                    return StaticValues.ERROR;
            }
        }

        public void Clean()
        {
            this.Weightstr = string.Empty;
            this.Lengthstr = string.Empty;
            this.Weight = 0;
            this.Length = 0;
            this.Bait = string.Empty;
        }
    }
}
