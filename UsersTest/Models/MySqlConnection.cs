using UsersTest.Interfaces;

namespace UsersTest.Models
{
    public class MySqlConnection : IMySqlConnection
    {
        public string ConnectionString { get; set; }
        public string Version { get; set; }
    }
}
