using Kutcha.Core;
using MongoDB.Driver;

namespace Kutcha.MongoDb
{
    public sealed class MongoKutchaContext : IKutchaContext
    {
        private readonly IStoreNamingConvention _namingConvention;
        private readonly IMongoDatabase _mongoDatabase;
        
        public MongoKutchaContext(IMongoDatabase mongoDatabase, IStoreNamingConvention namingConvention = null)
        {
            _mongoDatabase = mongoDatabase;
            _namingConvention = namingConvention ?? new AttributeStoreNamingConvention();
        }

        public IKutchaStore<TRoot> GetStore<TRoot>() where TRoot : class, IKutchaRoot, new()
        {
            return CreateStore<TRoot>();
        }

        public IKutchaReadOnlyStore<TRoot> GetReadOnlyStore<TRoot>() where TRoot : class, IKutchaRoot, new()
        {
            return CreateStore<TRoot>();
        }

        private IKutchaStore<TRoot> CreateStore<TRoot>() where TRoot : class, IKutchaRoot, new()
        {
            string storeName = _namingConvention.GetStoreName<TRoot>();
            var settings = new MongoCollectionSettings { AssignIdOnInsert = false };
            IMongoCollection<TRoot> collection = _mongoDatabase.GetCollection<TRoot>(storeName, settings);
            return new MongoKutchaStore<TRoot>(collection);
        }
    }
}