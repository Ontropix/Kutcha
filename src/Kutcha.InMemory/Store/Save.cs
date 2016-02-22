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
            Save(root);
            await Task.CompletedTask;
        }
    }
}