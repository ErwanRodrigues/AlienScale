using AlienScale.Models;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AlienScale.StaticResources
{
    public class Initialization
    {
        public static bool Initialize()
        {
            List<FishType> fisht = FishType.GetFishTypes();
            if(fisht.Count == 0)
            {
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(Record)).Assembly;
                Stream stream = assembly.GetManifestResourceStream("AlienScale.StaticResources.JSON.FirstUse.json");
                //get info from json
                using (var reader = new System.IO.StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    try
                    {
                        var result = JsonConvert.DeserializeObject<IEnumerable<Record>>(json);
                        var records = result as List<Record>;

                        foreach (var rec in records)
                        {
                            FishType ft = new FishType() { TypeName = rec.typename, TypeDescription = rec.typedescription };
                            FishType.Insert(ft);
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                }
            }  
            return true;
        }

        public static void CreateTables()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Address>();
                conn.CreateTable<City>();
                conn.CreateTable<Coordinate>();
                conn.CreateTable<Country>();
                conn.CreateTable<Fish>();
                conn.CreateTable<FishingLoc>();
                conn.CreateTable<FishType>();
                conn.CreateTable<MainTeam>();
                conn.CreateTable<Session>();
                conn.CreateTable<SessionTeam>();
                conn.CreateTable<Spot>();
                conn.CreateTable<Team>();
                conn.CreateTable<TeamMainTeam>();
                conn.CreateTable<User>();
                conn.CreateTable<UserTeam>();
                conn.CreateTable<Waypoint>();
            }

        }


    }
    
    public class Record
    {
        public string typename { get; set; }
        public string typedescription { get; set; }
    }

}
