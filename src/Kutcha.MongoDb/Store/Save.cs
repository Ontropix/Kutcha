using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Kutcha.MongoDb
{
    internal partial class MongoKutchaStore<TRoot>
    {
        public void Save(TRoot root)
        {
            ValidateRoot(root);
            Collection.ReplaceOne(Filters.Eq(x => x.Id, root.Id), root, Upsert);
        }

        public async Task SaveAsync(TRoot root)
        {
            ValidateRoot(root);
            await Collection.ReplaceOneAsync(Filters.Eq(x => x.Id, root.Id), root, Upsert);
        }
    }
}