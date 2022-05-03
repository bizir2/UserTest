using System.Threading.Tasks;
using UsersTest.Interfaces;

namespace UsersTest.Services
{
    public class DbOrCache<TModel> : IDbOrCache<TModel>
        where TModel : class, IDbDocument
    {
        private readonly IСacheDbOperation<TModel> _cacheDbOperation;
        private readonly IDbOperation<TModel> _dbOperation;

        public DbOrCache(IСacheDbOperation<TModel> cacheDbOperation,
            IDbOperation<TModel> dbOperation)
        {
            _cacheDbOperation = cacheDbOperation;
            _dbOperation = dbOperation;
        }
        
        public Task<TModel?> ById(int id)
        {
            return _cacheDbOperation.Get(id);
        }

        public Task<TModel> Create(TModel user)
        {
            _cacheDbOperation.Set(user);
            return _dbOperation.Create(user);
        }

        public Task Remove(TModel model)
        {
            _cacheDbOperation.Remove(model.ID);
            return _dbOperation.Remove(model);
        }

        public void SetStatus(TModel model)
        {
            _cacheDbOperation.Set(model);
        }
        
        public async Task SetAll()
        {
            var updates = await _dbOperation.GetAll();
            _cacheDbOperation.SetAll(updates);
        }
    }
}
