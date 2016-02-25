using System;
using System.Collections.Generic;
using System.Linq;
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

        public void DeleteMany(Expression<Func<TRoot, bool>> filter)
        {
            Argument.IsNotNull(filter, "filter");
            Func<TRoot, bool> where = filter.Compile();

            List<string> idsForDelete = Container.Values.Where(x => where.Invoke(x)).Select(x => x.Id).ToList();
            foreach (string id in idsForDelete)
            {
                DeleteById(id);
            }
        }

        public async Task DeleteManyAsync(Expression<Func<TRoot, bool>> filter)
        {
            DeleteMany(filter);
            await Task.CompletedTask;
        }
    }
}