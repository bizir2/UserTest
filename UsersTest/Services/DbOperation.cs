using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UsersTest.Context;
using UsersTest.Interfaces;

namespace UsersTest.Services
{
    public class DbOperation<TModel> : IDbOperation<TModel>
        where TModel : class, IDbDocument
    {
        private readonly MySqlContext _mySqlContext;
        private readonly DbSet<TModel> _set;
        public DbOperation(MySqlContext mySqlContext)
        {
            _mySqlContext = mySqlContext;
            _set = _mySqlContext.Set<TModel>();
        }
        
        public Task<TModel?> ById(int id)
        {
            return _set.FirstOrDefaultAsync(x => x.ID == id);
        }
        
        public async Task<TModel> Create(TModel model)
        {
            var result = _set.Add(model);
            await _mySqlContext.SaveChangesAsync();
            var u = _set.ToList();
            return result.Entity;
        }

        public async Task Remove(TModel model)
        {
            _set.Remove(model);
            await _mySqlContext.SaveChangesAsync();
        }
        
        public Task UpdateMany(TModel[] models)
        {
            _set.UpdateRange(models);
            return _mySqlContext.SaveChangesAsync();
        }
        
        public Task<TModel[]> GetAll()
        {
            return _set.ToArrayAsync();
        }
    }
}
