using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Kutcha.Core;

namespace Kutcha.InMemory.ReadOnly
{
    internal partial class InMemoryKutchaReadOnlyStore<TRoot> where TRoot : class, IKutchaRoot, new()
    {
        public List<TRoot> GetAll()
        {
            return Container.Values.ToList();
        }

        public async Task<List<TRoot>> GetAllAsync()
        {
            return await Task.FromResult(GetAll());
        }

        public TRoot GetById(string id)
        {
            Argument.StringNotEmpty(id, "id");

            TRoot root;
            Container.TryGetValue(id, out root);
            return root;
        }

        public async Task<TRoot> GetByIdAsync(string id)
        {
            return await Task.FromResult(GetById(id));
        }
        
        public List<TRoot> GetByIds(ICollection<string> ids)
        {
            throw new NotImplementedException();
        }

        public Task<List<TRoot>> GetByIdsAsync(ICollection<string> ids)
        {
            throw new NotImplementedException();
        }

        public List<TRoot> GetByIds(params string[] ids)
        {
            Argument.ElementsNotEmpty(ids);
            return ids.Select(id => GetById(id)).Where(doc => doc != null).ToList();
        }

        public async Task<List<TRoot>> GetByIdsAsync(params string[] ids)
        {
            return await Task.FromResult(GetByIds(ids));
        }

        public List<TRoot> Where(Expression<Func<TRoot, bool>> whereExpression)
        {
            Argument.IsNotNull(whereExpression, "whereExpression");
            Func<TRoot, bool> whereFunc = whereExpression.Compile();
            return Container.Values.Where(document => whereFunc.Invoke(document)).ToList();
        }

        public async Task<List<TRoot>> WhereAsync(Expression<Func<TRoot, bool>> whereExpression)
        {
            return await Task.FromResult(Where(whereExpression));
        }

        public async Task<List<TRoot>> FindAsync(Expression<Func<TRoot, bool>> whereExpression, int skip, int take)
        {
            return await Task.FromResult(Where(whereExpression).Skip(skip).Take(take).ToList());
        }

        public TRoot FindOne(Expression<Func<TRoot, bool>> whereExpression)
        {
            Argument.IsNotNull(whereExpression, "whereExpression");
            Func<TRoot, bool> whereFunc = whereExpression.Compile();
            return Container.Values.FirstOrDefault(whereFunc);
        }

        public async Task<TRoot> FindOneAsync(Expression<Func<TRoot, bool>> whereExpression)
        {
            return await Task.FromResult(FindOne(whereExpression));
        }

        public Task<List<TRoot>> ByLocationAsync(
            Expression<Func<TRoot, object>> field, double longitude, double latitude, double? maxDistance = null, double? minDistance = null)
        {
            throw new NotSupportedException();
        }
    }
}