using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Kutcha.MongoDb
{
    internal partial class MongoKutchaStore<TRoot>
    {
        public void DeleteById(string id)
        {
            Argument.StringNotEmpty(id, "id");
            Collection.DeleteOne(Filters.Eq(root => root.Id, id));
        }

        public async Task DeleteByIdAsync(string id)
        {
            Argument.StringNotEmpty(id, "id");
            await Collection.DeleteOneAsync(Filters.Eq(root => root.Id, id));
        }

        public void DeleteMany(Expression<Func<TRoot, bool>> filter)
        {
            Argument.IsNotNull(filter, "filter");
            Collection.DeleteMany(filter);
        }

        public async Task DeleteManyAsync(Expression<Func<TRoot, bool>> filter)
        {
            Argument.IsNotNull(filter, "filter");
            await Collection.DeleteManyAsync(filter);
        }
    }
}