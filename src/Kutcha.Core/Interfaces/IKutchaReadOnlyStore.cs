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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<TRoot> GetAll();
        Task<List<TRoot>> GetAllAsync();

        /// <summary>
        /// Returns document by id. If document does not exists - method will return null.
        /// </summary>
        TRoot GetById(string id);
        Task<TRoot> GetByIdAsync(string id);
        
        /// <summary>
        /// Returns documents by theirs ids (skips unfound documents).
        /// </summary>
        List<TRoot> GetByIds(ICollection<string> ids);
        Task<List<TRoot>> GetByIdsAsync(ICollection<string> ids);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        List<TRoot> Where(Expression<Func<TRoot, bool>> whereExpression);
        Task<List<TRoot>> WhereAsync(Expression<Func<TRoot, bool>> whereExpression);


        TRoot FindOne(Expression<Func<TRoot, bool>> whereExpression);
        Task<TRoot> FindOneAsync(Expression<Func<TRoot, bool>> whereExpression);

        Task<List<TRoot>> SortBy(Expression<Func<TRoot, object>> sortExpression, int skip, int take);
        Task<List<TRoot>> SortByDescending(Expression<Func<TRoot, object>> sortExpression, int skip, int take);
    }
}