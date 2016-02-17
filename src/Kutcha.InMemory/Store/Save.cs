using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kutcha.InMemory
{
    internal partial class InMemoryKutchaStore<TRoot>
    {
        public void Save(TRoot root)
        {
            ValidateRoot(root);
            Container.AddOrUpdate(root.Id, id => root, (id, existing) => root);
        }

        public async Task SaveAsync(TRoot root)
        {
            await Task.Run(() => Save(root));
        }

        public void SaveMany(List<TRoot> roots)
        {
            foreach (TRoot document in roots)
            {
                ValidateRoot(document);
            }

            foreach (TRoot document in roots)
            {
                Container.AddOrUpdate(document.Id, id => document, (id, existing) => document);
            }
        }

        public async Task SaveManyAsync(List<TRoot> roots)
        {
            await Task.Run(() => SaveMany(roots));
        }
    }
}