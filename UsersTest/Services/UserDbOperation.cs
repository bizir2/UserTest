using UsersTest.Context;
using UsersTest.Entites;
using UsersTest.Interfaces;

namespace UsersTest.Services
{
    public class UsersDbOperation : DbOperation<UserDocument>, IUsersDbOperation
    {
        public UsersDbOperation(MySqlContext mySqlContext):base(mySqlContext) {}
    }
}
