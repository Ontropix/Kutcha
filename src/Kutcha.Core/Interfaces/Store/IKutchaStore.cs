using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Kutcha.Core
{
    public interface IKutchaStore
    {
        void Truncate();
    }
    
    public interface IKutchaStore<TRoot> : IKutchaStore, IKutchaReadOnlyStore<TRoot> where TRoot : class, IKutchaRoot, new()
    {
        void Insert(TRoot root);
        Task InsertAsync(TRoot root);

        void InsertMany(ICollection<TRoot> roots);
        Task InsertManyAsync(ICollection<TRoot> roots);
        
        void Replace(TRoot root);
        Task ReplaceAsync(TRoot root);

        void Save(TRoot root);
        Task SaveAsync(TRoot root);
        
        void DeleteById(string id);
        Task DeleteByIdAsync(string id);

        void DeleteMany(Expression<Func<TRoot, bool>> filter);
        Task DeleteManyAsync(Expression<Func<TRoot, bool>> filter);
    }
}