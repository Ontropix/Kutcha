using System;
using System.Collections.Concurrent;
using Kutcha.Core;

namespace Kutcha.InMemory
{
    public class InMemoryKutchaContext : IKutchaContext
    {
        protected readonly ConcurrentDictionary<Type, IKutchaStore> Stores;
        
        public InMemoryKutchaContext()
        {
            Stores = new ConcurrentDictionary<Type, IKutchaStore>();
        }

        public IKutchaStore<TRoot> GetStore<TRoot>() where TRoot : class, IKutchaRoot, new()
        {
            return CreateStore<TRoot>();
        }

        public IKutchaReadOnlyStore<TRoot> GetReadOnlyStore<TRoot>() where TRoot : class, IKutchaRoot, new()
        {
            return CreateStore<TRoot>();
        }

        protected virtual IKutchaStore<TRoot> CreateStore<TRoot>() where TRoot : class, IKutchaRoot, new()
        {
            Type rootType = typeof(TRoot);

            IKutchaStore store;
            if (!Stores.TryGetValue(rootType, out store))
            {
                Stores[rootType] = store = new InMemoryKutchaStore<TRoot>();
            }

            return (IKutchaStore<TRoot>)store;
        }

        public void ClearAllStorages()
        {
            foreach (IKutchaStore store in Stores.Values)
            {
                store.Truncate();
            }
        }
    }
}