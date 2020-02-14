using AlienScale.StaticResources;
using SQLite;
using System;
using System.Collections.Generic;

namespace AlienScale.Models
{
    public class FishType : BaseModel
    {
        private string _idFishType;
        [PrimaryKey]
        public string IdFishType
        {
            get { return _idFishType; }
            set
            {
                _idFishType = value;
                SetProperty(ref _idFishType, value);
            }
        }

        private string _typeName;
        public string TypeName
        {
            get { return _typeName; }
            set
            {
                _typeName = value;
                SetProperty(ref _typeName, value);
            }
        }

        private string _typeDescription;
        public string TypeDescription
        {
            get { return _typeDescription; }
            set
            {
                _typeDescription = value;
                SetProperty(ref _typeDescription, value);
            }
        }

        private string _descriptionUrl;
        public string DescriptionUrl
        {
            get { return _descriptionUrl; }
            set
            {
                _descriptionUrl = value;
                SetProperty(ref _descriptionUrl, value);
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
        
        public static FishType Insert(FishType type)
        {

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                type.IdFishType = Guid.NewGuid().ToString();
                conn.CreateTable<FishType>();
                int rows = conn.Insert(type);

                if (rows > 0)
                    return type;
                else
                    return null;
            }
        }

        public static List<FishType> GetFishTypes()
        {
            List<FishType> fishTypes = new List<FishType>();
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<FishType>();
                fishTypes = conn.Table<FishType>().ToList();

            }
            return fishTypes;
        }
        
        public static int Delete(FishType fishType)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<FishType>();
                int rows = conn.Delete<FishType>(fishType.IdFishType);

                if (rows > 0)
                    return StaticValues.SUCCESS;
                else
                    return StaticValues.ERROR;
            }
        }
    }
}
