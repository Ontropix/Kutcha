using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Kutcha.Core
{
    public interface IKutchaReadOnlyStore<TRoot> where TRoot : class, IKutchaRoot, new()
    {
        List<TRoot> GetAll();
        Task<List<TRoot>> GetAllAsync();

        TRoot FindById(string id);
        Task<TRoot> FindByIdAsync(string id);

        TRoot FindOne(Expression<Func<TRoot, bool>> filter);
        Task<TRoot> FindOneAsync(Expression<Func<TRoot, bool>> filter);

        List<TRoot> FindMany(Expression<Func<TRoot, bool>> filter);
        Task<List<TRoot>> FindManyAsync(Expression<Func<TRoot, bool>> filter);
        
        Task<List<TRoot>> SortBy(Expression<Func<TRoot, object>> sortExpression, int skip, int take);
        Task<List<TRoot>> SortByDescending(Expression<Func<TRoot, object>> sortExpression, int skip, int take);
    }
}