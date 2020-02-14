using AlienScale.StaticResources;
using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace AlienScale.Models
{
    public class FishingLoc : BaseModel
    {
        //******************************* PK *******************************
        private string _idFishingLoc;
        [PrimaryKey]
        public string IdFishingLoc
        {
            get { return _idFishingLoc; }
            set
            {
                _idFishingLoc = value;
                SetProperty(ref _idFishingLoc, value);
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

        private string _type;
        public string Type
        {
            get { return _type; }
            set
            {
                SetProperty(ref _type, value);
            }
        }

        private int _placeCount;
        public int PlaceCount
        {
            get { return _placeCount; }
            set
            {
                SetProperty(ref _placeCount, value);
            }
        }

        private string _placeCountstr;
        public string PlaceCountstr
        {
            get { return _placeCountstr; }
            set
            {
                _placeCountstr = value;
                if (!string.IsNullOrEmpty(_placeCountstr))
                    PlaceCount = int.Parse(_placeCountstr);
                else
                    PlaceCount = 0;
                SetProperty(ref _placeCountstr, value);
            }
        }

        private string _pictureUrl;
        public string PictureUrl
        {
            get { return _pictureUrl; }
            set
            {
                _pictureUrl = value;
                SetProperty(ref _pictureUrl, value);
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

        private List<Waypoint> _waypoints;
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Waypoint> Waypoints
        {
            get { return _waypoints; }
            set
            {
                _waypoints = value;
                SetProperty(ref _waypoints, value);
            }
        }

        private double _totalWeight;
        [Ignore]
        public double TotalWeight
        {
            get { return _totalWeight; }
            set
            {
                _totalWeight = value;
                SetProperty(ref _totalWeight, value);
            }
        }

        private int _totalCatches;
        [Ignore]
        public int TotalCatches
        {
            get { return _totalCatches; }
            set
            {
                _totalCatches = value;
                SetProperty(ref _totalCatches, value);
            }
        }
        
        public FishingLoc()
        {
            Waypoints = new List<Waypoint>();
        }

        public static void Insert(FishingLoc fishLoc)
        {
            
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<FishingLoc>();
                conn.InsertOrReplaceWithChildren(fishLoc, recursive:true);
            }
        }

        public static List<FishingLoc> GetFishingLocsByUserId(string idUser)
        {
            List<FishingLoc> fishingLocs = new List<FishingLoc>();
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<FishingLoc>();
                fishingLocs =  conn.GetAllWithChildren<FishingLoc>(recursive: true).Where(p => p.User_IdUser == idUser).ToList();
                
            }
            return fishingLocs;
        }

        public static List<FishingLoc> GetNamedFishingLoc(string idUser)
        {
            List<FishingLoc> fishingLocs = GetFishingLocsByUserId(idUser);

            //getting named fishing loc
            fishingLocs = (from l in fishingLocs
                           where l.Name != null
                           select l).ToList();

            List<Fish> fishes = Fish.GetFishesByUserId(idUser);
            if (fishingLocs != null)
            {
                foreach (var loc in fishingLocs)
                {
                    // getting fish by loc Id
                    List<Fish> locFishes = (from p in fishes
                                            where p.FishingLoc_IdFishingLoc == loc.IdFishingLoc
                                            select p).ToList();
                    //calculate & set total weight & total catches for each loc
                    double sum = 0;
                    foreach (var fish in locFishes)
                    {
                        sum += fish.Weight;
                    }
                    loc.TotalWeight = sum;
                    loc.TotalCatches = locFishes.Count();
                }
            }

            return fishingLocs;
        }

        public static FishingLoc GetNamedFishingLocById(string idUser, string idFishingLoc)
        {
            List<FishingLoc> fishingLocs = GetNamedFishingLoc(idUser);
            FishingLoc fl = new FishingLoc();

            fl = (from l in fishingLocs
                           where l.IdFishingLoc == null
                           select l).ToList().FirstOrDefault();
            if(fishingLocs.Count == 1)
            {
                return fl;
            }
            else
            {
                return null;
            }
            
        }

        public static int Delete(FishingLoc fishLoc)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<FishingLoc>();
                int rows = conn.Delete<FishingLoc>(fishLoc.IdFishingLoc);

                if (rows > 0)
                    return StaticValues.SUCCESS;
                else
                    return StaticValues.ERROR;
            }
        }

        public static FishingLoc GetFishingLocById(string id)
        {
            FishingLoc loc = new FishingLoc();
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<FishingLoc>();
                loc = conn.Table<FishingLoc>().Where(p => p.IdFishingLoc == id).FirstOrDefault();
            }
            return loc;
        }
    }
}
