using System;
using System.Linq.Expressions;  
using System.Threading.Tasks;

namespace Kutcha.Core
{
    public static class IKutchaStoreExtensions
    {
        public static void Insert<TRoot>(this IKutchaStore<TRoot> store, Action<TRoot> constructor) where TRoot : class, IKutchaRoot, new()
        {
            var root = new TRoot();
            constructor(root);
            store.Insert(root);
        }

        public static async Task InsertAsync<TRoot>(this IKutchaStore<TRoot> store, Action<TRoot> constructor) where TRoot : class, IKutchaRoot, new()
        {
            var root = new TRoot();
            constructor(root);
            await store.InsertAsync(root);
        }
        
        public static void Save<TRoot>(this IKutchaStore<TRoot> store, Action<TRoot> constructor) where TRoot : class, IKutchaRoot, new()
        {
            var root = new TRoot();
            constructor(root);
            store.Save(root);
        }

        public static async Task SaveAsync<TRoot>(this IKutchaStore<TRoot> store, Action<TRoot> constructor) where TRoot : class, IKutchaRoot, new()
        {
            var root = new TRoot();
            constructor(root);
            await store.SaveAsync(root);
        }

        public static void FindOneAndUpdate<TRoot>(this IKutchaStore<TRoot> store, string id, Action<TRoot> updater) where TRoot : class, IKutchaRoot, new()
        {
            TRoot root = store.FindById(id);

            if (root == null) throw new InvalidOperationException("No root was found"); 
            updater.Invoke(root);

            if (root.Id != id) throw new InvalidOperationException("Root ID cannot be changed");
            store.Save(root);
        }

        public static async Task FindOneAndUpdateAsync<TRoot>(this IKutchaStore<TRoot> store, string id, Action<TRoot> updater) where TRoot : class, IKutchaRoot, new()
        {
            TRoot root = await store.FindByIdAsync(id);

            if (root == null) throw new InvalidOperationException("No root was found");
            updater.Invoke(root);

            if (root.Id != id) throw new InvalidOperationException("Root ID cannot be changed");
            await store.SaveAsync(root);
        }

        public static void FindOneAndUpdate<TRoot>(this IKutchaStore<TRoot> store, Expression<Func<TRoot, bool>> filter, Action<TRoot> updater) where TRoot : class, IKutchaRoot, new()
        {
            TRoot root = store.FindOne(filter);
            
            if (root == null) throw new InvalidOperationException("No root was found");
            string id = root.Id;

            updater.Invoke(root);

            if (root.Id != id) throw new InvalidOperationException("Root ID cannot be changed");
            store.Save(root);
        }

        public static async Task FindOneAndUpdateAsync<TRoot>(this IKutchaStore<TRoot> store, Expression<Func<TRoot, bool>> filter, Action<TRoot> updater) where TRoot : class, IKutchaRoot, new()
        {
            TRoot root = await store.FindOneAsync(filter);

            if (root == null) throw new InvalidOperationException("No root was found");
            string id = root.Id;

            updater.Invoke(root);

            if (root.Id != id) throw new InvalidOperationException("Root ID cannot be changed");
            await store.SaveAsync(root);
        }
    }
}