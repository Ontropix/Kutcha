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
            DeleteById(id);
            await Task.CompletedTask;
        }

        public void Delete(Expression<Func<TRoot, bool>> filter)
        {
            Argument.IsNotNull(filter, "filter");
            Func<TRoot, bool> whereFunc = filter.Compile();

            foreach (TRoot document in Container.Values)
            {
                if (whereFunc.Invoke(document))
                {
                    DeleteById(document.Id);
                }
            }
        }

        public async Task DeleteAsync(Expression<Func<TRoot, bool>> filter)
        {
            Delete(filter);
            await Task.CompletedTask;
        }
    }
}