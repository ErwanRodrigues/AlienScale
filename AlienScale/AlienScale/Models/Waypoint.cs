using AlienScale.StaticResources;
using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlienScale.Models
{
    public class Waypoint :  BaseModel
    {
        private string _idWaypoint;
        [PrimaryKey]
        public string IdWaypoint
        {
            get { return _idWaypoint; }
            set
            {
                _idWaypoint = value;
                SetProperty(ref _idWaypoint, value);
            }
        }

        private int _order;
        public int Order
        {
            get { return _order; }
            set
            {
                _order = value;
                SetProperty(ref _order, value);
            }
        }

        private string _coordinate_IdCoordinate;
        [ForeignKey(typeof(Coordinate))]
        public string Coordinate_IdCoordinate
        {
            get { return _coordinate_IdCoordinate; }
            set
            {
                _coordinate_IdCoordinate = value;
                SetProperty(ref _coordinate_IdCoordinate, value);
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
        
        private Coordinate _coordinate;
        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public Coordinate Coordinate
        {
            get { return _coordinate; }
            set
            {
                _coordinate = value;
                SetProperty(ref _coordinate, value);
            }
        }
        
        public static Waypoint Insert(Waypoint wp)
        {

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                wp.IdWaypoint = Guid.NewGuid().ToString();
                conn.CreateTable<Waypoint>();
                int rows = conn.Insert(wp);

                if (rows > 0)
                    return wp;
                else
                    return null;
            }
        }

        public static List<Waypoint> GetWaypoints()
        {
            List<Waypoint> wps = new List<Waypoint>();
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Waypoint>();
                wps = conn.Table<Waypoint>().ToList();

            }
            return wps;
        }

        public static List<Waypoint> GetWaypointsByFishingLoc(string idFishingLoc)
        {
            List<Waypoint> wps = new List<Waypoint>();

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Waypoint>();
                wps = conn.GetAllWithChildren<Waypoint>(recursive: true).Where(p => p.FishingLoc_IdFishingLoc == idFishingLoc).ToList();
            }
            return wps;
        }

        public static int Delete(Waypoint wp)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Waypoint>();
                int rows = conn.Delete<Waypoint>(wp.IdWaypoint);

                if (rows > 0)
                    return StaticValues.SUCCESS;
                else
                    return StaticValues.ERROR;
            }
        }
        
    }
}
