using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace workiom_test_project.Models
{
    public class Contact : Document
    {
        [BsonElement("Name")]
        public string Name { get; set; }
        public List<Company> Companies { get; set; }
    }
}