using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Kutcha.Core;

namespace Kutcha.InMemory.ReadOnly
{
    internal partial class InMemoryKutchaReadOnlyStore<TRoot> : IKutchaReadOnlyStore<TRoot> where TRoot : class, IKutchaRoot, new()
    {
        protected static ConcurrentDictionary<string, TRoot> Container { get; private set; }

        public InMemoryKutchaReadOnlyStore()
        {
            Container = new ConcurrentDictionary<String, TRoot>();
        }
        
        public void Drop()
        {
            Container.Clear();
        }

        public void Truncate()
        {
            Container.Clear();
        }

        public Task CreateIndex(Expression<Func<TRoot, object>> field)
        {
            throw new NotSupportedException();
        }

        public Task CreateGeoIndex(Expression<Func<TRoot, object>> field)
        {
            throw new NotSupportedException();
        }

        public Task DropAllIndexes()
        {
            throw new NotSupportedException();
        }
    }
}