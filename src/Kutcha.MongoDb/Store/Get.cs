using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;

namespace Kutcha.MongoDb
{
    internal partial class MongoKutchaStore<TRoot>
    {
        public List<TRoot> All()
        {
            return AsyncHelpers.RunSync(() => AllAsync());
        }

        public async Task<List<TRoot>> AllAsync()
        {
            return await Collection.Find(EmptyFilter).ToListAsync();
        }

        public TRoot FindById(string id)
        {
            Argument.StringNotEmpty(id, "id");
            return AsyncHelpers.RunSync(() => FindByIdAsync(id));
        }

        public async Task<TRoot> FindByIdAsync(string id)
        {
            Argument.StringNotEmpty(id, "id");
            return await Collection.Find(MongoFilter.Eq(root => root.Id, id)).FirstOrDefaultAsync();
        }

        public List<TRoot> FindByIds(params string[] ids)
        {
            Argument.ElementsNotEmpty(ids);
            return AsyncHelpers.RunSync(() => FindByIdsAsync(ids));
        }

        public async Task<List<TRoot>> FindByIdsAsync(params string[] ids)
        {
            Argument.ElementsNotEmpty(ids);
            return await Collection.Find(MongoFilter.In(root => root.Id, ids)).ToListAsync();
        }

        public List<TRoot> Find(Expression<Func<TRoot, bool>> whereExpression)
        {
            Argument.IsNotNull(whereExpression, "whereExpression");
            return AsyncHelpers.RunSync(() => FindAsync(whereExpression));
        }

        public async Task<List<TRoot>> FindAsync(Expression<Func<TRoot, bool>> whereExpression)
        {
            Argument.IsNotNull(whereExpression, "whereExpression");
            return await Collection.Find(whereExpression).ToListAsync();
        }

        public async Task<List<TRoot>> FindAsync(Expression<Func<TRoot, bool>> whereExpression, int skip, int take)
        {
            Argument.IsNotNull(whereExpression, "whereExpression");
            return await Collection.Find(whereExpression).Skip(skip).Limit(take).ToListAsync();
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
            return await Collection.Find(EmptyFilter).SortBy(sortExpression).Skip(skip).Limit(take).ToListAsync();
        }

        public async Task<List<TRoot>> SortByDescending(Expression<Func<TRoot, object>> sortExpression, int skip, int take)
        {
            Argument.IsNotNull(sortExpression, "sortExpression");
            return await Collection.Find(EmptyFilter).SortByDescending(sortExpression).Skip(skip).Limit(take).ToListAsync();
        }

        public async Task<List<TRoot>> SortBy(Expression<Func<TRoot, bool>> whereExpression, Expression<Func<TRoot, object>> sortExpression, int skip, int take)
        {
            Argument.IsNotNull(whereExpression, "whereExpression");
            return await Collection.Find(whereExpression).SortBy(sortExpression).Skip(skip).Limit(take).ToListAsync();
        }

        public async Task<List<TRoot>> SortByDescending(Expression<Func<TRoot, bool>> whereExpression, Expression<Func<TRoot, object>> sortExpression, int skip, int take)
        {
            Argument.IsNotNull(whereExpression, "whereExpression");
            return await Collection.Find(whereExpression).SortByDescending(sortExpression).Skip(skip).Limit(take).ToListAsync();
        }
        
        public async Task<List<TRoot>> ByLocationAsync(Expression<Func<TRoot, object>> field, double longitude, double latitude, double? maxDistance = null, double? minDistance = null)
        {
            return await Collection.Find(MongoFilter.Near(field,
                GeoJson.Point(GeoJson.Geographic(longitude, latitude)) , maxDistance, minDistance)
            ).ToListAsync();
        }
    }
}