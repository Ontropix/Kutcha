using System.Threading.Tasks;

namespace Kutcha.InMemory
{
    internal partial class InMemoryKutchaStore<TRoot>
    {
        public void Replace(TRoot root)
        {
            ValidateRoot(root);
            Container[root.Id] = root;
        }

        public async Task ReplaceAsync(TRoot root)
        {
            await Task.Run(() => Replace(root));
        }
    }
}