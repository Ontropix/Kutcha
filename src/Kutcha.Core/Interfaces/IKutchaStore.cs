using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Kutcha.Core
{
    public interface IKutchaStore
    {
        void Drop();

        void Truncate();
    }

    /// <summary>
    /// Simple abstraction that represents typed collection of documents.
    /// Collection always consists only of one type of documents.
    /// </summary>
    public interface IKutchaStore<TRoot> : IKutchaStore, IKutchaReadOnlyStore<TRoot> where TRoot : class, IKutchaRoot, new()
    {
        void Insert(TRoot root);
        Task InsertAsync(TRoot root);
        void InsertMany(List<TRoot> roots);
        Task InsertManyAsync(List<TRoot> roots);
        
     
        void Replace(TRoot root);
        Task ReplaceAsync(TRoot root);

        //void UpdateField<TField>(Expression<Func<TRoot, TField>> field, TField value, Expression<Func<TRoot, bool>> @where);
        //void Update<TMember>(IList<string> ids, Expression<Func<TDocument, TMember>> expression, TMember value);


        void Save(TRoot root);
        Task SaveAsync(TRoot root);
        void SaveMany(List<TRoot> roots);
        Task SaveManyAsync(List<TRoot> roots);

        
        void DeleteById(string id);
        Task DeleteByIdAsync(string id);
        void DeleteByIds(params string[] ids);
        Task DeleteByIdsAsync(params string[] ids);
        void Delete(Expression<Func<TRoot, bool>> whereExpression);
        Task DeleteAsync(Expression<Func<TRoot, bool>> whereExpression);
    }
}