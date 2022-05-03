namespace UsersTest.Interfaces
{
    public interface IMySqlConnection
    {
        public string ConnectionString { get; set; }
        public string Version { get; set; }
    }
}