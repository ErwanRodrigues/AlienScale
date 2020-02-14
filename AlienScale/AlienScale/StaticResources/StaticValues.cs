using System;
using System.Collections.Generic;
using System.Text;

namespace AlienScale.StaticResources
{
    public class StaticValues
    {
        //return types
        public const int EXIST_ALRDY = 1;
        public const int ERROR = 99;
        public const int SUCCESS = 0;

        public const string ZIPCODE_SEARCH = "https://geo.api.gouv.fr/communes?codePostal={0}&fields=nom&format=json&geometry=centre";
    }
    
}
