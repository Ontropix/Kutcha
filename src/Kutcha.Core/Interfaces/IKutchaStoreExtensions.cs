using System;
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

        public static void Replace<TRoot>(this IKutchaStore<TRoot> store, Action<TRoot> constructor) where TRoot : class, IKutchaRoot, new()
        {
            var root = new TRoot();
            constructor(root);
            store.Replace(root);
        }

        public static async Task ReplaceAsync<TRoot>(this IKutchaStore<TRoot> store, Action<TRoot> constructor) where TRoot : class, IKutchaRoot, new()
        {
            var root = new TRoot();
            constructor(root);
            await store.ReplaceAsync(root);
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
            TRoot root = store.GetById(id);
            updater.Invoke(root);
            root.Id = id; //preventing wrong updates
            store.Replace(root);
        }

        public static async Task FindOneAndUpdateAsync<TRoot>(this IKutchaStore<TRoot> store, string id, Action<TRoot> updater) where TRoot : class, IKutchaRoot, new()
        {
            TRoot root = await store.GetByIdAsync(id);
            updater.Invoke(root);
            root.Id = id; //preventing wrong updates
            await store.ReplaceAsync(root);
        }
    }
}