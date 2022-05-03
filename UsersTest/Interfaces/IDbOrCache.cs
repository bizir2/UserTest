using System.Threading.Tasks;

namespace UsersTest.Interfaces
{
    public interface IDbOrCache<TModel>
    {
        public Task<TModel?> ById(int id);
        public Task<TModel> Create(TModel model);
        public Task Remove(TModel model);
        public void SetStatus(TModel model);
        public Task SetAll();
    }
}
