using System;
using Newtonsoft.Json;

namespace QueryGeneratorCore
{
    public static class JSONReaderFactory
    {       
        public static QuerySchema Create(string rawJSON)
        {
            if (string.IsNullOrEmpty(rawJSON)) throw new Exception("Please inform a JSON");

            return JsonConvert.DeserializeObject<QuerySchema>(rawJSON);
        }        
    }
}
