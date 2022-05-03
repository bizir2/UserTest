using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersTest.Interfaces;

namespace UsersTest.Services
{
    public class СacheDbOperation<TModel> : IСacheDbOperation<TModel>
        where TModel : class, IDbDocument
    {
        private Dictionary<int, TModel> _cache = new Dictionary<int, TModel>();
        private List<int> _updateIds = new List<int>();
        private object _objList = new();
        private object _objDictionary = new();
        
        public void Set(TModel model)
        {
            LockDictionary(() => _cache[model.ID] = model);
            if (!_updateIds.Contains(model.ID))
            {
                LockList(() => _updateIds.Add(model.ID));
            }
        }
        
        public void Remove(int id)
        {
            LockDictionary(() => _cache.Remove(id));
            if (!_updateIds.Contains(id))
            {
                LockList(() => _updateIds.Add(id));
            }
        }
        
        public Task<TModel?> Get(int id)
        {
            if(_cache.ContainsKey(id))
                return Task.FromResult(_cache[id])!;
            return Task.FromResult<TModel?>(null);
        }
        
        public TModel[] GetUpdatesAndClear()
        {
            TModel[] updates = null;
            LockList(() =>
            {
                updates = _updateIds.Select(x => _cache.ContainsKey(x) ? _cache[x] : null)
                    .Where(x => x != null)
                    .ToArray()!;
                _updateIds = new List<int>();   
                 
            });
            return updates;
        }

        public void SetAll(TModel[] updates)
        {
            LockDictionary(() =>
            {
                foreach (var u in updates)
                {
                    _cache[u.ID] = u;
                } 
            });
        }
        
        public void AddUpdates(TModel[] updates)
        {
            LockList(() => 
                _updateIds.AddRange(updates.Select(x => x.ID))
            );
        }

        private void LockList(Action action)
        {
            lock (_objList)
            {
                action();
            }
        }
        
        private void LockDictionary(Action action)
        {
            lock (_objDictionary)
            {
                action();
            }
        }
    }
}
