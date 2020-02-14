using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlienScale.Models
{
    public class Session : BaseModel
    {
        #region Properties
        //******************************* PK *******************************
        private string _idSession;
        [PrimaryKey]
        public string IdSession
        {
            get { return _idSession; }
            set
            {
                _idSession = value;
                SetProperty(ref _idSession, value);
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

        private DateTime _starts;
        public DateTime Starts
        {
            get { return _starts; }
            set
            {
                _starts = value;
                SetProperty(ref _starts, value);
            }
        }
        
        private DateTime _ends;
        public DateTime Ends
        {
            get { return _ends; }
            set
            {
                _ends = value;
                SetProperty(ref _ends, value);
            }
        }

        private bool _isEnduro;
        public bool IsEnduro
        {
            get { return _isEnduro; }
            set
            {
                _isEnduro = value;
                SetProperty(ref _isEnduro, value);
            }
        }

        private bool _isInterclubMode;
        public bool IsInterclubMode
        {
            get { return _isInterclubMode; }
            set
            {
                _isInterclubMode = value;
                SetProperty(ref _isInterclubMode, value);
            }
        }

        private bool _isRunning;
        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                _isRunning = value;
                SetProperty(ref _isRunning, value);
            }
        }
        
        //******************************* FK *******************************
        private string _user_IdUserCreation;
        [ForeignKey(typeof(User))]
        public string User_IdUserCreation
        {
            get { return _user_IdUserCreation; }
            set
            {
                _user_IdUserCreation = value;
                SetProperty(ref _user_IdUserCreation, value);
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

        private FishingLoc _fishingLoc;
        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public FishingLoc FishingLoc
        {
            get { return _fishingLoc; }
            set
            {
                _fishingLoc = value;
                SetProperty(ref _fishingLoc, value);
            }
        }

        private List<Fish> _fishes;
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Fish> Fishes
        {
            get { return _fishes; }
            set
            {
                _fishes = value;
                SetProperty(ref _fishes, value);
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
        #endregion

        public Session()
        {
            Starts = DateTime.Now;
            Ends = DateTime.Now;
            Fishes = new List<Fish>();
        }

        public static void Insert(Session session)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
               conn.CreateTable<Session>();
               conn.InsertOrReplaceWithChildren(session,recursive:true);

            }
        }

        public static void InsertFullSession(Session session)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.InsertOrReplaceWithChildren(session, recursive: true);
            }
        }

        public static List<Session> GetSessions(string idUser)
        {
            List<Session> sessions = new List<Session>();
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                sessions = conn.GetAllWithChildren<Session>(recursive:true).Where(p => p.User_IdUserCreation == idUser).ToList();
                
                if (sessions != null)
                {
                    foreach (var session in sessions)
                    {
                        double sum = 0;
                        foreach (var fish in session.Fishes)
                        {
                            sum += fish.Weight;
                        }
                        session.TotalWeight = sum;
                        session.TotalCatches = session.Fishes.Count();
                    }
                }
            }
            return sessions;
        }

        public static Session GetFullSessionById(string idSession)
        {
            List<Session> sessions = GetSessions(App.user.IdUser);
            Session sess = (from s in sessions
                            where s.IdSession == idSession
                            select s).ToList().FirstOrDefault();

            double sum = 0;
            foreach (var fish in sess.Fishes)
            {
                sum += fish.Weight;
            }
            sess.TotalWeight = sum;
            sess.TotalCatches = sess.Fishes.Count();
            return sess;
        }
        
        public static bool Delete(Session session, bool cascadeValidation = false)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                if (cascadeValidation)
                {
                    conn.Delete(session, recursive: true);
                }
                else
                {
                    conn.Delete<Session>(session._idSession);
                }
                return true;
            }
        }
    }
}
