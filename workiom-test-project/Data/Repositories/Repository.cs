using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver.Core.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using workiom_test_project.Data.Interfaces;
using workiom_test_project.Extensions;
using workiom_test_project.Models;

namespace workiom_test_project.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : IDocument
    {
        protected IDbSettings Settings { get; private set; }

        protected readonly IMongoCollection<T> mongoCollection;

        public Repository(string mongoDBConnectionString, string dbName, string collectionName)
        {
            var client = new MongoClient(mongoDBConnectionString);
            var database = client.GetDatabase(dbName);
            mongoCollection = database.GetCollection<T>(collectionName);
        }

        public virtual async Task<List<T>> GetListAsync()
        {
            return (await mongoCollection.FindAsync(item => true)).ToList();
        }

        public virtual async Task<T> GetByIdAsync(string id)
        {
            var docId = new ObjectId(id);
            return (await mongoCollection.FindAsync<T>(m => m.Id == docId)).FirstOrDefault();
        }

        public virtual async Task<List<T>> GetByIdAsync(List<string> ids)
        {
            var docIds = ids.Select(id => new ObjectId(id)).ToList();
            var filter = Builders<T>.Filter.In(x => x.Id, docIds);
            return (await mongoCollection.FindAsync<T>(filter)).ToList();
        }

        public virtual async Task<T> CreateAsync(T model)
        {
            await mongoCollection.InsertOneAsync(model);
            return model;
        }

        public virtual async Task<bool> UpdateAsync(string id, T model)
        {
            model.Id = new ObjectId(id);
            return (await mongoCollection.ReplaceOneAsync(m => m.Id == model.Id, model)).ModifiedCount > 0;
        }

        public virtual async Task<bool> DeleteAsync(T model)
        {
            return (await mongoCollection.DeleteOneAsync(m => m.Id == model.Id)).DeletedCount > 0;
        }

        public virtual async Task<bool> DeleteAsync(string id)
        {
            var docId = new ObjectId(id);
            return (await mongoCollection.DeleteOneAsync(m => m.Id == docId)).DeletedCount > 0;
        }

        public virtual async Task<bool> AddColumnAsync(NewColumn item)
        {
            var filterDefinition = Builders<T>.Filter.Empty;
            var update = new BsonDocument("$set", new BsonDocument(item.name, item.value ?? item.type.ToDefaultValue()));
            return (await mongoCollection.UpdateManyAsync(filterDefinition, update)).ModifiedCount > 0;
        }

        public virtual async Task<List<T>> SearchAsync(Dictionary<string, object> queries)
        {
            var builder = Builders<T>.Filter;
            FilterDefinition<T> filter = builder.Empty;

            foreach (var query in queries)
            {
                var subFilter = builder.Eq(query.Key, query.Value);
                filter = filter == builder.Empty ? subFilter : filter & subFilter;
            }

            return (await mongoCollection.FindAsync<T>(filter)).ToList();
        }
    }
}
