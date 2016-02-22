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
                throw new InvalidOperationException($"Root with Id={root.Id} already exists.");
            }

            Container.TryAdd(root.Id, root);
        }

        public async Task InsertAsync(TRoot root)
        {
            Insert(root);
            await Task.CompletedTask;
        }

        public void InsertMany(ICollection<TRoot> roots)
        {
            roots.ForEach(ValidateRoot);
            roots.ForEach(root => Container.TryAdd(root.Id, root));
        }

        public async Task InsertManyAsync(ICollection<TRoot> roots)
        {
            InsertMany(roots);
            await Task.CompletedTask;
        }
    }
}