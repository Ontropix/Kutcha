using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Kutcha.Core
{
    /// <summary>
    /// Represents read-only collection of specified document.
    /// </summary>
    /// <typeparam name="TRoot">Class that implemented IDocument interface.</typeparam>
    public interface IKutchaReadOnlyStore<TRoot> where TRoot : class, IKutchaRoot, new()
    {
        List<TRoot> All();
        Task<List<TRoot>> AllAsync();

        /// <summary>
        /// Returns document by id. If document does not exists - method will return null.
        /// </summary>
        TRoot FindById(string id);
        Task<TRoot> FindByIdAsync(string id);

        
        /// <summary>
        /// Returns documents by theirs ids (skips unfound documents).
        /// </summary>
        List<TRoot> FindByIds(params string[] ids);
        Task<List<TRoot>> FindByIdsAsync(params string[] ids);


        List<TRoot> Find(Expression<Func<TRoot, bool>> whereExpression);
        Task<List<TRoot>> FindAsync(Expression<Func<TRoot, bool>> whereExpression);
        Task<List<TRoot>> FindAsync(Expression<Func<TRoot, bool>> whereExpression, int skip, int take);


        TRoot FindOne(Expression<Func<TRoot, bool>> whereExpression);
        Task<TRoot> FindOneAsync(Expression<Func<TRoot, bool>> whereExpression);

        Task<List<TRoot>> SortBy(Expression<Func<TRoot, object>> sortExpression, int skip, int take);
        Task<List<TRoot>> SortByDescending(Expression<Func<TRoot, object>> sortExpression, int skip, int take);
        Task<List<TRoot>> SortBy(Expression<Func<TRoot, bool>> whereExpression, Expression<Func<TRoot, object>> sortExpression, int skip, int take);
        Task<List<TRoot>> SortByDescending(Expression<Func<TRoot, bool>> whereExpression, Expression<Func<TRoot, object>> sortExpression, int skip, int take);

        Task<List<TRoot>> ByLocationAsync(Expression<Func<TRoot, object>> field, double longitude, double latitude, double? maxDistance = null, double? minDistance = null);

        Task CreateIndex(Expression<Func<TRoot, object>> field);
        Task CreateGeoIndex(Expression<Func<TRoot, object>> field);
        Task DropAllIndexes();
    }
}