using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace workiom_test_project.Models
{
    public abstract class BaseModel
    {
        public ObjectId Id { get; set; }
    }
}
