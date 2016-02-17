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
            AsyncHelpers.RunSync(() => SaveAsync(root));
        }

        public async Task SaveAsync(TRoot root)
        {
            ValidateRoot(root);
            await Collection.ReplaceOneAsync(MongoFilter.Eq(x => x.Id, root.Id),
                                             root,
                                             new UpdateOptions()
                                             {
                                                 IsUpsert = true
                                             });
        }

        public void SaveMany(List<TRoot> roots)
        {
            roots.ForEach(ValidateRoot);
        }

        public async Task SaveManyAsync(List<TRoot> roots)
        {
            roots.ForEach(ValidateRoot);
        }
    }
}