using System.Threading.Tasks;

namespace UsersTest.Interfaces
{
    public interface IСacheDbOperation<TModel>
        where TModel : IDbDocument
    {
        public void Set(TModel model);
        public Task<TModel?> Get(int id);
        public void Remove(int id);
        public TModel[] GetUpdatesAndClear();
        public void AddUpdates(TModel[] updates);
        public void SetAll(TModel[] updates);
    }
}
