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
            var indexModels = new List<CreateIndexModel<Company>>();

            var textIndex = new CreateIndexOptions() { Name = "Text" };
            var nameUniqueIndex = new CreateIndexOptions() { Name = "NameUniqueIndex", Unique = true };

            indexModels.Add(new CreateIndexModel<Company>(builder.Text("$**"), textIndex));
            indexModels.Add(new CreateIndexModel<Company>(builder.Ascending(x => x.Name), nameUniqueIndex));

            try
            {
                mongoCollection.Indexes.CreateMany(indexModels);
            }
            catch (Exception)
            {
                mongoCollection.Indexes.DropAll();
                mongoCollection.Indexes.CreateMany(indexModels);
            }
        }
    }
}
