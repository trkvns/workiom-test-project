using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using workiom_test_project.Data.Interfaces;
using workiom_test_project.Extensions;
using workiom_test_project.Models;

namespace workiom_test_project.Data.Repositories
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(IDbSettings s) : base(s.ConnectionString, s.DatabaseName, s.CompaniesCollectionName)
        {

        }

        public async Task<bool> AddColumnAsync(NewColumn item)
        {
            var filterDefinition = Builders<Company>.Filter.Empty;
            var update = new BsonDocument("$set", new BsonDocument(item.name, item.type.ToDefaultValue()));
            return (await mongoCollection.UpdateManyAsync(filterDefinition, update)).ModifiedCount > 0;
        }
    }
}
