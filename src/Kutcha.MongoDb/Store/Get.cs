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

        public TRoot FindById(string id)
        {
            Argument.StringNotEmpty(id, "id");
            return Collection.Find(root => root.Id == id).FirstOrDefault();
        }

        public async Task<TRoot> FindByIdAsync(string id)
        {
            Argument.StringNotEmpty(id, "id");
            return await Collection.Find(root => root.Id == id).FirstOrDefaultAsync();
        }

        public List<TRoot> FindMany(Expression<Func<TRoot, bool>> filter)
        {
            Argument.IsNotNull(filter, "filter");
            return Collection.Find(filter).ToList();
        }

        public async Task<List<TRoot>> FindManyAsync(Expression<Func<TRoot, bool>> filter)
        {
            Argument.IsNotNull(filter, "filter");
            return await Collection.Find(filter).ToListAsync();
        }

        public TRoot FindOne(Expression<Func<TRoot, bool>> filter)
        {
            Argument.IsNotNull(filter, "filter");
            return AsyncHelpers.RunSync(() => FindOneAsync(filter));
        }

        public async Task<TRoot> FindOneAsync(Expression<Func<TRoot, bool>> filter)
        {
            Argument.IsNotNull(filter, "filter");
            return await Collection.Find(filter).FirstOrDefaultAsync();
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