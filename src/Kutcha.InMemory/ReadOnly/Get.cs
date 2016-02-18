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
        public List<TRoot> All()
        {
            return Container.Values.ToList();
        }

        public async Task<List<TRoot>> AllAsync()
        {
            return await Task.FromResult(All());
        }

        public TRoot FindById(string id)
        {
            Argument.StringNotEmpty(id, "id");

            TRoot root;
            Container.TryGetValue(id, out root);
            return root;
        }

        public async Task<TRoot> FindByIdAsync(string id)
        {
            return await Task.FromResult(FindById(id));
        }

        public List<TRoot> FindByIds(params string[] ids)
        {
            Argument.ElementsNotEmpty(ids);
            return ids.Select(id => FindById(id)).Where(doc => doc != null).ToList();
        }

        public async Task<List<TRoot>> FindByIdsAsync(params string[] ids)
        {
            return await Task.FromResult(FindByIds(ids));
        }

        public List<TRoot> Find(Expression<Func<TRoot, bool>> whereExpression)
        {
            Argument.IsNotNull(whereExpression, "whereExpression");
            Func<TRoot, bool> whereFunc = whereExpression.Compile();
            return Container.Values.Where(document => whereFunc.Invoke(document)).ToList();
        }

        public async Task<List<TRoot>> FindAsync(Expression<Func<TRoot, bool>> whereExpression)
        {
            return await Task.FromResult(Find(whereExpression));
        }

        public async Task<List<TRoot>> FindAsync(Expression<Func<TRoot, bool>> whereExpression, int skip, int take)
        {
            return await Task.FromResult(Find(whereExpression).Skip(skip).Take(take).ToList());
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