using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kutcha.MongoDb
{
    internal partial class MongoKutchaStore<TRoot>
    {
        public void Insert(TRoot root)
        {
            ValidateRoot(root);
            AsyncHelpers.RunSync(() => InsertAsync(root));
        }

        public async Task InsertAsync(TRoot root)
        {
            ValidateRoot(root);
            await Collection.InsertOneAsync(root);
        }

        public void InsertMany(List<TRoot> roots)
        {
            roots.ForEach(ValidateRoot);
            AsyncHelpers.RunSync(() => InsertManyAsync(roots));
        }

        public async Task InsertManyAsync(List<TRoot> roots)
        {
            roots.ForEach(ValidateRoot);
            await Collection.InsertManyAsync(roots);
        }
    }
}