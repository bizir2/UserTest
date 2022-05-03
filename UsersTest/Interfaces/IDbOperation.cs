using System.Threading.Tasks;

namespace UsersTest.Interfaces
{
    public interface IDbOperation<TModel>
        where TModel : class, IDbDocument
    {
        public Task<TModel?> ById(int id);
        public Task<TModel> Create(TModel model);
        public Task Remove(TModel model);
        public Task UpdateMany(TModel[] models);
        public Task<TModel[]> GetAll();

    }
}
