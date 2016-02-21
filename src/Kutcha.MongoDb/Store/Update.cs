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
        
        //public void UpdateField<TField>(Expression<Func<TRoot, bool>> @where, Expression<Func<TRoot, TField>> updater, TField value)
        //{
        //    AsyncHelpers.RunSync(() =>  Collection.UpdateManyAsync(where, Update.Set(updater, value)));
        //}

        //public async Task UpdateFieldAsync<TField>(Expression<Func<TRoot, bool>> @where, Expression<Func<TRoot, TField>> updater, TField value)
        //{
        //    await Collection.UpdateManyAsync(where, Update.Set(updater, value));
        //}
    }
}