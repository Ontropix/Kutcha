using System;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Kutcha.MongoDb
{
    internal partial class MongoKutchaStore<TRoot>
    {
        public void Replace(TRoot root)
        {
            ValidateRoot(root);
            AsyncHelpers.RunSync(() => ReplaceAsync(root));
        }

        public async Task ReplaceAsync(TRoot root)
        {
            ValidateRoot(root);
            await Collection.ReplaceOneAsync(x => x.Id == root.Id, root);
        }
    }
}