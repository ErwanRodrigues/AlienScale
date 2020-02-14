using AlienScale.StaticResources;
using SQLite;
using System.Collections.Generic;

namespace AlienScale.Models
{
    public class Coordinate : BaseModel
    {
        private string _idCoordinate;
        [PrimaryKey]
        public string IdCoordinate
        {
            get { return _idCoordinate; }
            set
            {
                _idCoordinate = value;
                SetProperty(ref _idCoordinate, value);
            }
        }
        #region
        #endregion
        private double _lattitude;
        public double Lattitude
        {
            get { return _lattitude; }
            set
            {
                _lattitude = value;
                SetProperty(ref _lattitude, value);
            }
        }

        private double _longitude;
        public double Longitude
        {
            get { return _longitude; }
            set
            {
                _longitude = value;
                SetProperty(ref _longitude, value);
            }
        }

        public static Coordinate Insert(Coordinate latLong)
        {

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Coordinate>();
                int rows = conn.Insert(latLong);

                if (rows > 0)
                    return latLong;
                else
                    return null;
            }
        }

        public static List<Coordinate> GetCoordinates()
        {
            List<Coordinate> coordinates = new List<Coordinate>();
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Coordinate>();
                coordinates = conn.Table<Coordinate>().ToList();
            }
            return coordinates;
        }

        public static int Delete(Coordinate coordinates)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Coordinate>();
                int rows = conn.Delete<Coordinate>(coordinates.IdCoordinate);

                if (rows > 0)
                    return StaticValues.SUCCESS;
                else
                    return StaticValues.ERROR;
            }
        }
    }
}
