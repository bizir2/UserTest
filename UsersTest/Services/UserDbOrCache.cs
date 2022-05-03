using UsersTest.Entites;
using UsersTest.Interfaces;

namespace UsersTest.Services
{
    public class UserDbOrCache : DbOrCache<UserDocument>, IUserDbOrCache
    {
        public UserDbOrCache(IСacheDbOperation<UserDocument> cacheDbOperation, IDbOperation<UserDocument> dbOperation) : base(cacheDbOperation, dbOperation) { }
    }
}
