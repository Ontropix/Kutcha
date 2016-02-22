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

        public TRoot FindOne(Expression<Func<TRoot, bool>> filter)
        {
            Argument.IsNotNull(filter, "filter");
            Func<TRoot, bool> whereFunc = filter.Compile();
            return Container.Values.FirstOrDefault(whereFunc);
        }

        public async Task<TRoot> FindOneAsync(Expression<Func<TRoot, bool>> filter)
        {
            return await Task.FromResult(FindOne(filter));
        }

        public List<TRoot> FindMany(Expression<Func<TRoot, bool>> filter)
        {
            Argument.IsNotNull(filter, "filter");
            Func<TRoot, bool> whereFunc = filter.Compile();
            return Container.Values.Where(document => whereFunc.Invoke(document)).ToList();
        }

        public async Task<List<TRoot>> FindManyAsync(Expression<Func<TRoot, bool>> filter)
        {
            return await Task.FromResult(FindMany(filter));
        }
    }
}