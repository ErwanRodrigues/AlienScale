using AlienScale.StaticResources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AlienScale.Services
{
    public class CityResolver
    {
      
        public async static Task<List<Cities>> GetCityFmZipCode(string zipcode)
        {
            var url = GenerateURL(zipcode);
            List<Cities> cities = new List<Cities>();

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                
                var json = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<IEnumerable<Cities>>(json);
                
                cities = result as List<Cities>;
            }
            return cities;
        }

        public static string GenerateURL(string zipcode)
        {
            string url = string.Empty;
            url = string.Format(StaticValues.ZIPCODE_SEARCH, zipcode);
            return url;
        }
    }

    public class Cities
    {
        public string nom { get; set; }
        public string code { get; set; }
    }   
}
