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

        public MongoKutchaStore(IMongoCollection<TRoot> collection)
        {
            Collection = collection;
            Filters = Builders<TRoot>.Filter;
            Update = Builders<TRoot>.Update;
        }
        
        private void ValidateRoot(TRoot root)
        {
            Argument.IsNotNull(root, "root");
            Argument.StringNotEmpty(root.Id, "root.Id");
        }

        public void Drop()
        {
            throw new NotSupportedException();
        }

        public void Truncate()
        {
            Collection.DeleteMany(Filters.Empty);
        }
    }
}