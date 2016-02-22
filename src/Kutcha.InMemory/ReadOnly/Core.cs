using System.Collections.Concurrent;
using Kutcha.Core;

namespace Kutcha.InMemory.ReadOnly
{
    internal partial class InMemoryKutchaReadOnlyStore<TRoot> : IKutchaReadOnlyStore<TRoot> where TRoot : class, IKutchaRoot, new()
    {
        protected static ConcurrentDictionary<string, TRoot> Container { get; private set; }

        public InMemoryKutchaReadOnlyStore()
        {
            Container = new ConcurrentDictionary<string, TRoot>();
        }
    }
}