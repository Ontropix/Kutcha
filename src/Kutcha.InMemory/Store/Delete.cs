using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Kutcha.InMemory
{
    internal partial class InMemoryKutchaStore<TRoot>
    {
        public void DeleteById(string id)
        {
            Argument.StringNotEmpty(id, "id");
            TRoot root;
            Container.TryRemove(id, out root);
        }

        public async Task DeleteByIdAsync(string id)
        {
            await Task.Run(() => DeleteById(id));
        }

        public void DeleteByIds(params string[] ids)
        {
            Argument.ElementsNotEmpty(ids);
            foreach (string id in ids)
            {
                DeleteById(id);
            }
        }

        public async Task DeleteByIdsAsync(params string[] ids)
        {
            Argument.ElementsNotEmpty(ids);
            await Task.Run(() => Parallel.ForEach(ids, id => DeleteByIds(id)));
        }

        public void Delete(Expression<Func<TRoot, bool>> whereExpression)
        {
            Argument.IsNotNull(whereExpression, "whereExpression");
            Func<TRoot, bool> whereFunc = whereExpression.Compile();

            foreach (TRoot document in Container.Values)
            {
                if (whereFunc.Invoke(document))
                {
                    DeleteById(document.Id);
                }
            }
        }

        public async Task DeleteAsync(Expression<Func<TRoot, bool>> whereExpression)
        {
            Argument.IsNotNull(whereExpression, "whereExpression");
            Func<TRoot, bool> whereFunc = whereExpression.Compile();

            await Task.Run(() =>
            {
                Parallel.ForEach(Container.Values, document =>
                {
                    if (whereFunc.Invoke(document))
                    {
                        DeleteById(document.Id);
                    }
                });
            });
        }
    }
}