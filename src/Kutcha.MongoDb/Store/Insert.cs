using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kutcha.MongoDb
{
    internal partial class MongoKutchaStore<TRoot>
    {
        public void Insert(TRoot root)
        {
            ValidateRoot(root);
            Collection.InsertOne(root);
        }

        public async Task InsertAsync(TRoot root)
        {
            ValidateRoot(root);
            await Collection.InsertOneAsync(root);
        }

        public void InsertMany(ICollection<TRoot> roots)
        {
            foreach(TRoot root in roots) ValidateRoot(root);
            Collection.InsertMany(roots);
        }

        public async Task InsertManyAsync(ICollection<TRoot> roots)
        {
            foreach (TRoot root in roots) ValidateRoot(root);
            await Collection.InsertManyAsync(roots);
        }
    }
}