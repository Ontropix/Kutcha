using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Kutcha.MongoDb
{
    internal partial class MongoKutchaStore<TRoot>
    {
        public List<TRoot> GetAll()
        {
            return Collection.Find(Filters.Empty).ToList();
        }

        public async Task<List<TRoot>> GetAllAsync()
        {
            return await Collection.Find(Filters.Empty).ToListAsync();
        }

        public TRoot GetById(string id)
        {
            Argument.StringNotEmpty(id, "id");
            return Collection.Find(root => root.Id == id).FirstOrDefault();
        }

        public async Task<TRoot> GetByIdAsync(string id)
        {
            Argument.StringNotEmpty(id, "id");
            return await Collection.Find(root => root.Id == id).FirstOrDefaultAsync();
        }

        public List<TRoot> GetByIds(ICollection<string> ids)
        {
            Argument.ElementsNotEmpty(ids);
            return Collection.Find(Filters.In(root => root.Id, ids)).ToList();
        }

        public async Task<List<TRoot>> GetByIdsAsync(ICollection<string> ids)
        {
            Argument.ElementsNotEmpty(ids);
            return await Collection.Find(Filters.In(root => root.Id, ids)).ToListAsync();
        }

        public List<TRoot> Where(Expression<Func<TRoot, bool>> whereExpression)
        {
            Argument.IsNotNull(whereExpression, "whereExpression");
            return Collection.Find(whereExpression).ToList();
        }

        public async Task<List<TRoot>> WhereAsync(Expression<Func<TRoot, bool>> whereExpression)
        {
            Argument.IsNotNull(whereExpression, "whereExpression");
            return await Collection.Find(whereExpression).ToListAsync();
        }

        public TRoot FindOne(Expression<Func<TRoot, bool>> whereExpression)
        {
            Argument.IsNotNull(whereExpression, "whereExpression");
            return AsyncHelpers.RunSync(() => FindOneAsync(whereExpression));
        }

        public async Task<TRoot> FindOneAsync(Expression<Func<TRoot, bool>> whereExpression)
        {
            Argument.IsNotNull(whereExpression, "whereExpression");
            return await Collection.Find(whereExpression).FirstOrDefaultAsync();
        }

        public async Task<List<TRoot>> SortBy(Expression<Func<TRoot, object>> sortExpression, int skip, int take)
        {
            Argument.IsNotNull(sortExpression, "sortExpression");
            return await Collection.Find(Filters.Empty).SortBy(sortExpression).Skip(skip).Limit(take).ToListAsync();
        }

        public async Task<List<TRoot>> SortByDescending(Expression<Func<TRoot, object>> sortExpression, int skip,
            int take)
        {
            Argument.IsNotNull(sortExpression, "sortExpression");
            return
                await Collection.Find(Filters.Empty).SortByDescending(sortExpression).Skip(skip).Limit(take).ToListAsync();
        }
    }
}