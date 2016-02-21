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
            AsyncHelpers.RunSync(() => DeleteByIdAsync(id));
        }

        public async Task DeleteByIdAsync(string id)
        {
            Argument.StringNotEmpty(id, "id");
            await Collection.DeleteOneAsync(Filters.Eq(root => root.Id, id));
        }

        public void DeleteByIds(params string[] ids)
        {
            Argument.ElementsNotEmpty(ids);
            AsyncHelpers.RunSync(() => DeleteByIdsAsync(ids));
        }

        public async Task DeleteByIdsAsync(params string[] ids)
        {
            Argument.ElementsNotEmpty(ids);
            await Collection.DeleteManyAsync(Filters.In(root => root.Id, ids));
        }

        public void Delete(Expression<Func<TRoot, bool>> whereExpression)
        {
            Argument.IsNotNull(whereExpression, "whereExpression");
            AsyncHelpers.RunSync(() => DeleteAsync(whereExpression));
        }

        public async Task DeleteAsync(Expression<Func<TRoot, bool>> whereExpression)
        {
            Argument.IsNotNull(whereExpression, "whereExpression");
            await Collection.DeleteManyAsync(whereExpression);
        }
    }
}