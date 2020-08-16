using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace workiom_test_project.Models
{
    public class Contact : Document
    {
        public Contact()
        {
            CompanyIds = new List<string>();
        }

        [BsonElement("Name")]
        public string Name { get; set; }
        public List<string> CompanyIds { get; set; }
        [BsonExtraElements, JsonExtensionData]
        public Dictionary<string, object> AdditionalData { get; set; }
        
        // Non Db Fields
        [BsonIgnore]
        public List<Company> Companies { get; set; }
    }
}