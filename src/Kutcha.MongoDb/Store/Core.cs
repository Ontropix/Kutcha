using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Kutcha.Core;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Kutcha.MongoDb
{
    internal partial class MongoKutchaStore<TRoot> : IKutchaStore<TRoot> where TRoot : class, IKutchaRoot, new()
    {
        protected readonly IMongoCollection<TRoot> Collection;
        protected readonly FilterDefinitionBuilder<TRoot> MongoFilter;
        protected readonly UpdateDefinitionBuilder<TRoot> MongoUpdate;
        protected readonly IndexKeysDefinitionBuilder<TRoot> MongoIndex;
        protected readonly BsonDocument EmptyFilter = new BsonDocument();

        public MongoKutchaStore(IMongoCollection<TRoot> collection)
        {
            Collection = collection;
            MongoFilter = Builders<TRoot>.Filter;
            MongoUpdate = Builders<TRoot>.Update;
            MongoIndex = Builders<TRoot>.IndexKeys;
        }
        
        private void ValidateRoot(TRoot root)
        {
            Argument.IsNotNull(root, "root");
            Argument.StringNotEmpty(root.Id, "root.Id");
        }

        public void Drop()
        {
            throw new NotImplementedException();
        }

        public void Truncate()
        {
            throw new NotImplementedException();
        }

        public async Task CreateIndex(Expression<Func<TRoot, object>> field)
        {
            await Collection.Indexes.CreateOneAsync(MongoIndex.Ascending(field));
        }

        public async Task CreateGeoIndex(Expression<Func<TRoot, object>> field)
        {
            await Collection.Indexes.CreateOneAsync(MongoIndex.Geo2DSphere(field));
        }

        public async Task DropAllIndexes()
        {
            await Collection.Indexes.DropAllAsync();
        }
    }
}