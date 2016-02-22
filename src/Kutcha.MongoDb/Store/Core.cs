using System;
using Kutcha.Core;
using MongoDB.Driver;

namespace Kutcha.MongoDb
{
    internal partial class MongoKutchaStore<TRoot> : IKutchaStore<TRoot> where TRoot : class, IKutchaRoot, new()
    {
        protected readonly IMongoCollection<TRoot> Collection;
        protected readonly FilterDefinitionBuilder<TRoot> Filters;
        protected readonly UpdateDefinitionBuilder<TRoot> Update;
        private readonly UpdateOptions Upsert;

        public MongoKutchaStore(IMongoCollection<TRoot> collection)
        {
            Collection = collection;
            Filters = Builders<TRoot>.Filter;
            Update = Builders<TRoot>.Update;
            Upsert = new UpdateOptions { IsUpsert = true };
        }

        private void ValidateRoot(TRoot root)
        {
            Argument.IsNotNull(root, nameof(root));
            Argument.StringNotEmpty(root.Id, nameof(root.Id));
        }

        public void Truncate()
        {
            Collection.DeleteMany(Filters.Empty);
        }
    }
}