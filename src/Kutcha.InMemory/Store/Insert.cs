using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kutcha.InMemory
{
    internal partial class InMemoryKutchaStore<TRoot>
    {
        public void Insert(TRoot root)
        {
            ValidateRoot(root);
            if (Container.ContainsKey(root.Id))
            {
                throw new InvalidOperationException(String.Format("Root with Id={0} already exists.", root.Id));
            }

            Container.TryAdd(root.Id, root);
        }

        public async Task InsertAsync(TRoot root)
        {
            await Task.Run(() => Insert(root));
        }

        public void InsertMany(List<TRoot> roots)
        {
            roots.ForEach(ValidateRoot);
            roots.ForEach(root => Container.TryAdd(root.Id, root));
        }

        public async Task InsertManyAsync(List<TRoot> roots)
        {
            await Task.Run(() => InsertMany(roots));
        }
    }
}