using SQLite;
using SQLiteNetExtensions.Attributes;

namespace AlienScale.Models
{
    public class Spot :  BaseModel
    {
        private string _idSpot;
        [PrimaryKey]
        public string IdSpot
        {
            get { return _idSpot; }
            set
            {
                _idSpot = value;
                SetProperty(ref _idSpot, value);
            }
        }

        private int _number;
        public int Number
        {
            get { return _number; }
            set
            {
                _number = value;
                SetProperty(ref _number, value);
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
        
    }
}
