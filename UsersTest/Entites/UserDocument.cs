using System.ComponentModel.DataAnnotations;
using UsersTest.Interfaces;
using UserTestLibrary.Models.User;

namespace UsersTest.Entites
{
    public class UserDocument : IDbDocument
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public UserStatus Status { get; set; }
    }
}
