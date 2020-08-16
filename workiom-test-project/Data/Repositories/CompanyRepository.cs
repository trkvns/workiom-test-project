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
            SetIndexes();
        }

        private void SetIndexes()
        {
            var builder = Builders<Company>.IndexKeys;
            var indexOptions = new CreateIndexOptions { Name = "NameUniqueIndex", Unique = true };
            var indexModel = new CreateIndexModel<Company>(builder.Ascending(x => x.Name), indexOptions);
            try
            {
                mongoCollection.Indexes.CreateOne(indexModel);
            }
            catch (Exception)
            {
                mongoCollection.Indexes.DropOne(indexOptions.Name);
                mongoCollection.Indexes.CreateOne(indexModel);
            }
        }
    }
}
