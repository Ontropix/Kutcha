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
        public async Task<List<TRoot>> SortBy(Expression<Func<TRoot, object>> sortExpression, int skip, int take)
        {
            Argument.IsNotNull(sortExpression, "sortExpression");
            Func<TRoot, object> sort = sortExpression.Compile();
            return await Task.FromResult(Container.Values.OrderBy(sort).ToList());
        }

        public async Task<List<TRoot>> SortByDescending(Expression<Func<TRoot, object>> sortExpression, int skip, int take)
        {
            Argument.IsNotNull(sortExpression, "sortExpression");
            Func<TRoot, object> sort = sortExpression.Compile();
            return await Task.FromResult(Container.Values.OrderByDescending(sort).ToList());
        }

        public async Task<List<TRoot>> SortBy(Expression<Func<TRoot, bool>> whereExpression, Expression<Func<TRoot, object>> sortExpression, int skip, int take)
        {
            Argument.IsNotNull(sortExpression, "sortExpression");
            Func<TRoot, bool> where = whereExpression.Compile();
            Func<TRoot, object> sort = sortExpression.Compile();

            List<TRoot> result = Container.Values.Where(where).OrderBy(sort).ToList();
            return await Task.FromResult(result);
        }

        public async Task<List<TRoot>> SortByDescending(Expression<Func<TRoot, bool>> whereExpression, Expression<Func<TRoot, object>> sortExpression, int skip, int take)
        {
            Argument.IsNotNull(whereExpression, "whereExpression");
            Argument.IsNotNull(sortExpression, "sortExpression");

            Func<TRoot, bool> where = whereExpression.Compile();
            Func<TRoot, object> sort = sortExpression.Compile();

            List<TRoot> result = Container.Values.Where(where).OrderByDescending(sort).ToList();
            return await Task.FromResult(result);
        }
    }
}